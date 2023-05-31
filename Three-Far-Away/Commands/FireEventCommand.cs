using Three_Far_Away.Infrastructure;

namespace Three_Far_Away.Commands
{
    public class FireEventCommand : CommandBase
    {
        private string _eventName;
        public FireEventCommand(string eventName)
        {
            _eventName = eventName;
        }

        public override void Execute(object parameter)
        {
            EventBus.FireEvent(_eventName);
        }
    }
}
