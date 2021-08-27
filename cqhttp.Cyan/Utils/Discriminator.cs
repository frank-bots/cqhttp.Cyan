using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// borrowed from https://gist.github.com/StevenLiekens/82ddcf1823ee91cf6d5edfcdb1f6a591
namespace cqhttp.Cyan.Utils {
    ///
    public delegate object CustomObjectCreator (Type discriminatedType);
    ///
    public delegate void JsonPreprocessor (string discriminator, JObject jsonObject);

    /// <summary>
    ///     Extend this class to configure a type with a discriminator field.
    /// </summary>
    public abstract class DiscriminatorOptions {
        /// <summary>Gets the base type, which is typically (but not necessarily) abstract.</summary>
        public abstract Type BaseType { get; }
        /// <summary>Gets the fallback type, which is typically (but not necessarily) abstract.</summary>
        public abstract Type FallbackType { get; }

        /// <summary>Gets the name of the discriminator field.</summary>
        public abstract string DiscriminatorFieldName { get; }

        /// <summary>Returns true if the discriminator should be serialized to the CLR type; otherwise false.</summary>
        public abstract bool SerializeDiscriminator { get; }

        /// <summary>Gets the mappings from discriminator values to CLR types.</summary>
        public abstract IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes ();

        /// <summary>Callback that creates an object which will then be populated by the serializer.</summary>
        public CustomObjectCreator Activator { get; protected set; } = null;

        /// <summary>Callback that can optionally mutate the JObject before it is converted.</summary>
        public JsonPreprocessor Preprocessor { get; protected set; } = null;
    }
    ///
    public sealed class DiscriminatedJsonConverter : JsonConverter {
        private readonly DiscriminatorOptions _discriminatorOptions;
        private readonly List<(string TypeName, Type Type)> _discriminatorTypes;
        ///
        public DiscriminatedJsonConverter (Type concreteDiscriminatorOptionsType)
            : this ((DiscriminatorOptions) Activator.CreateInstance (concreteDiscriminatorOptionsType)) {
        }
        ///
        public DiscriminatedJsonConverter (DiscriminatorOptions discriminatorOptions) {
            _discriminatorOptions = discriminatorOptions ?? throw new ArgumentNullException (nameof (discriminatorOptions));
            _discriminatorTypes = _discriminatorOptions.GetDiscriminatedTypes ().ToList ();
        }
        ///
        public override bool CanConvert (Type objectType) => _discriminatorOptions.BaseType.IsAssignableFrom (objectType);
        ///
        public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.Null) {
                return null;
            }

            var json = JObject.Load (reader);

            if (!json.ContainsKey("raw_event")) {
                json.Add("raw_event", json);
            }

            var discriminatorField = json.Property (_discriminatorOptions.DiscriminatorFieldName);
            if (discriminatorField is null) {
                Log.Error ($"找不到属性{_discriminatorOptions.DiscriminatorFieldName}，这可能是协议端的bug");
                throw new JsonSerializationException ($"Could not find discriminator field with name '{_discriminatorOptions.DiscriminatorFieldName}'.");
            }

            var discriminatorFieldValue = discriminatorField.Value.ToString ();

            var found = _discriminatorTypes.FirstOrDefault (tuple => tuple.TypeName == discriminatorFieldValue).Type;
            if (found == null) {
                Log.Warn ($"未能解析 ({_discriminatorOptions.DiscriminatorFieldName}=>{discriminatorFieldValue})");
                Log.Debug (json.ToString ());
                found = _discriminatorOptions.FallbackType ?? objectType;
            }

            _discriminatorOptions.Preprocessor?.Invoke (discriminatorFieldValue, json);
            if (!_discriminatorOptions.SerializeDiscriminator) {
                discriminatorField.Remove ();
            }
            if (found != objectType && found.CustomAttributes.Any (attribute => attribute.AttributeType == typeof (JsonConverterAttribute))) {
                return serializer.Deserialize (json.CreateReader (), found);
            }

            var value = _discriminatorOptions.Activator?.Invoke (found) ?? Activator.CreateInstance (found);
            serializer.Populate (json.CreateReader (), value);
            return value;
        }
        ///
        public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotSupportedException ("DiscriminatedJsonConverter should only be used while deserializing.");
        }
    }
}