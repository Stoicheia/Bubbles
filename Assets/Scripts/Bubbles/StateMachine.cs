using Sirenix.Serialization;

namespace Bubbles
{
    public class StateMachine
    {
        [field: OdinSerialize] public State CurrentState { get; private set; }

        public State ReceiveItem(Item item)
        {
            State newState = CurrentState.ReceiveItemAndTransition(item);
            if (newState != null)
            {
                CurrentState = newState;
            }

            return newState;
        }
    }
}