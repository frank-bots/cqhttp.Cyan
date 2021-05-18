namespace cqhttp.Cyan.Events.CQEvents {
    class UnknownEvent : Base.CQEvent {
        public override string post_type { get; }
        public string raw_event { get; private set; }
        public UnknownEvent (long time, string raw) :
            base (time) { this.raw_event = raw; }
    }
}