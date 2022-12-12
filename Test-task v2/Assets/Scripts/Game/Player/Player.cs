using UnityEngine;

namespace Game
{
    namespace Play
    {
        public abstract class Player : MonoBehaviour
        {
            protected int playerMoves;

            protected int playerReceivedBonus;

            protected int playerReceivedPenalty;

            protected abstract void PlayerMovingForward();

            protected abstract void PlayerMovingBack();

            protected abstract void Moving();

            protected abstract void SetPlayerStatistick();
        }
    }
}


