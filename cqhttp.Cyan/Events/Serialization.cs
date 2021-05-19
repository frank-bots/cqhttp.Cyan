using System;
using System.Linq;
using System.Collections.Generic;
using cqhttp.Cyan.Events.CQEvents;
using cqhttp.Cyan.Utils;
using System.Reflection;

namespace cqhttp.Cyan.Events {
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    class DiscriminatorValueAttribute : Attribute {
        public string _value { get; private set; }
        public DiscriminatorValueAttribute (string value) => _value = value;
    }
    class BaseEventDiscriminatorOptions : DiscriminatorOptions {
        public override Type BaseType => typeof (Events.CQEvents.Base.CQEvent);
        public override Type FallbackType => typeof (UnknownEvent);
        public override string DiscriminatorFieldName => throw new NotImplementedException ();
        public override bool SerializeDiscriminator => true;
        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes () {
            foreach (var t in GetSubtypes (BaseType)) {
                var attr = t.GetCustomAttribute<DiscriminatorValueAttribute> (false);
                if (attr != null) yield return (attr._value, t.AsType ());
            }
        }
        public TypeInfo[] GetSubtypes (Type type) {
            return type.Assembly.GetTypes ().AsParallel ().Where (
                (t) => t.IsSubclassOf (type)
            ).Select (t => t.GetTypeInfo ()).ToArray ();
        }
    }
    class EventDiscriminatorOptions : BaseEventDiscriminatorOptions {
        public override string DiscriminatorFieldName => "post_type";
        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes () {
            yield return ("request", typeof (CQEvents.Base.RequestEvent));
            yield return ("message", typeof (CQEvents.Base.MessageEvent));
            yield return ("notice", typeof (CQEvents.Base.NoticeEvent));
            yield return ("meta_event", typeof (CQEvents.MetaEvents.MetaEvent));
        }
    }
    class RequestEventDiscriminatorOptions : BaseEventDiscriminatorOptions {
        public override Type BaseType => typeof (Events.CQEvents.Base.RequestEvent);
        public override string DiscriminatorFieldName => "request_type";
    }
    class NoticeEventDiscriminatorOptions : BaseEventDiscriminatorOptions {
        public override Type BaseType => typeof (Events.CQEvents.Base.NoticeEvent);
        public override string DiscriminatorFieldName => "notice_type";
    }
    class MessageEventDiscriminatorOptions : BaseEventDiscriminatorOptions {
        public override Type BaseType => typeof (Events.CQEvents.Base.MessageEvent);
        public override string DiscriminatorFieldName => "message_type";
    }
    class MetaEventDiscriminatorOptions : BaseEventDiscriminatorOptions {
        public override Type BaseType => typeof (CQEvents.MetaEvents.MetaEvent);
        public override string DiscriminatorFieldName => "meta_event_type";
    }
    class NotifyNoticeEventDiscriminatorOptions : BaseEventDiscriminatorOptions {
        public override Type BaseType => typeof (CQEvents.NotifyEvent);
        public override string DiscriminatorFieldName => "sub_type";
    }
}