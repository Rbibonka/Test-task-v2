using UnityEngine;

namespace Game
{
    namespace Play
    {
        public class LevelParameters : MonoBehaviour
        {
            [Header("Configuration options")]
            [SerializeField]
            private int quantityPlayers;

            [SerializeField]
            private int quantitySpecialPlatforms;

            [SerializeField]
            private int penaltyNumberPlatforms;

            [Header("Game objects")]
            [SerializeField]
            private Transform[] platformPoints;

            [SerializeField]
            private GameObject[] players;

            public int GetPenaltyNumberPlatforms
            {
                get
                {
                    return penaltyNumberPlatforms;
                }
            }

            public GameObject[] GetPlayers
            {
                get
                {
                    return players;
                }
            }

            public Transform[] GetPlatfromPoints
            {
                get
                {
                    return platformPoints;
                }
            }

            public int GetPlayersQuantity
            {
                get
                {
                    return quantityPlayers;
                }
            }

            public int GetPlatformQuantity
            {
                get
                {
                    return platformPoints.Length;
                }
            }

            public int GetQuantitySpecialPlatforms
            {
                get
                {
                    return quantitySpecialPlatforms;
                }
            }

            public void ChangeQuantityPalyers(int value)
            {
                quantityPlayers = value;
            }

            public void ChangeQuantitySpecialPlatforms(int value)
            {
                quantitySpecialPlatforms = value;
            }
        }
    }
}