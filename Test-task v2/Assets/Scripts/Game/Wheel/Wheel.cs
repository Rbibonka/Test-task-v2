using UnityEngine;

namespace Game
{
    namespace Play
    {
        public abstract class Wheel : MonoBehaviour
        {
            protected int quantityMoves;

            protected abstract int CheckResultSpin();
        }
    }
}


