using System;

namespace Bubbles
{
    [Serializable]
    public abstract class State
    {
        public abstract State ReceiveItemAndTransition(Item item);
        public abstract State RemoveItemAndTransition(Item item);
    }
}