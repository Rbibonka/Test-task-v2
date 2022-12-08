using UnityEngine;

namespace Game
{
    namespace Play
    {
        public abstract class Dice : MonoBehaviour
        {
            protected int currentNumber;

            public enum CurrentState
            {
                idle,
                moving,
                landed
            }

            protected CurrentState currentState;

            protected abstract int CheckResultRoll();
        }
    }
}


