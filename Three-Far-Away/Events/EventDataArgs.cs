using System;

namespace Three_Far_Away.Events
{
    public class EventDataArgs : EventArgs
    {
        public object Data { get; }

        public EventDataArgs(object data)
        {
            Data = data;
        }
    }
}
