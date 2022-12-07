using UnityEngine;

namespace Game
{
    namespace Play
    {
        public abstract class Player : MonoBehaviour
        {
            protected int numberMotion;

            [SerializeField]
            protected int diceNumber;

            protected int playerId;

            [SerializeField]
            protected int currentPlatform;

            [SerializeField]
            protected int finishPlatform;
        }
    }
}


