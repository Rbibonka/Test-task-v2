using UnityEngine;

namespace Game
{
    namespace Play
    {
        public class GameStatistics : Statistics
        {
            private MovementPlayer[] players;

            public MovementPlayer[] GetPlayers
            {
                get
                {
                    return players;
                }
            }

            public void SetStatistics(MovementPlayer[] finishedPlayers)
            {
                players = finishedPlayers;
            }
        }
    }
}