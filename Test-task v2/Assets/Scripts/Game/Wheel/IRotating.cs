using UnityEngine;

namespace Game
{
    namespace Play
    {
        public interface IRotating
        {
            public void StartSpin();

            public Vector3 GetDistanceFromWheel { get; }

            public int GetQuantityMoves { get; }
        }
    }
}

