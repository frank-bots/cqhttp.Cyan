namespace cqhttp.Cyan.Events.CQEvents {
    class UnknownEvent : Base.CQEvent {
        public Newtonsoft.Json.Linq.JObject raw_event { get; set; }
    }
}