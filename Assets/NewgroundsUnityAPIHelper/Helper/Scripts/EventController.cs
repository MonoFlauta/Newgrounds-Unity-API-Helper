using System;
using io.newgrounds;

namespace NewgroundsUnityAPIHelper.Helper.Scripts
{
    internal class EventController
    {
        private readonly core _core;

        public EventController(core core)
        {
            _core = core;
        }

        public void LogEvent(string eventName, Action onLogEvent)
        {
            var request = new io.newgrounds.components.Event.logEvent { event_name = eventName };
            request.callWith(_core, _ => onLogEvent());
        }
    }
}