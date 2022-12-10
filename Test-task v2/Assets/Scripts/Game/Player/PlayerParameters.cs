using UnityEngine;

namespace Game
{
    namespace Play
    {
        public class PlayerParameters : Statistics
        {
            private int playerId;

            private string playerName;

            public string GetPlayerName
            {
                get
                {
                    return playerName;
                }
            }

            public int GetQuantityReceivedBonus
            {
                get
                {
                    return quantityPlayerReceivedBonus;
                }
            }

            public int GetQuantityPlayerReceivedBonus
            {
                get
                {
                    return quantityPlayerReceivedPenalty;
                }
            }

            public int GetQuantityMoves
            {
                get
                {
                    return quantityPlayerMoves;
                }
            }

            public void SetPlayerName(int numberPlayer)
            {
                var randomColor = Random.ColorHSV();

                GetComponentInChildren<Renderer>().material.color = randomColor;

                var playerText = GetComponentInChildren<TextMesh>();

                numberPlayer++;

                playerId = numberPlayer;

                playerText.text = $"Player {playerId}";
                
                playerName = playerText.text;

                playerText.color = randomColor;
            }

            public void SetPlayerStatistics(int quantityReceivedBonus, int quantityReceivedPenalty, int quantityMoves)
            {
                quantityPlayerReceivedBonus = quantityReceivedBonus;

                quantityPlayerReceivedPenalty = quantityReceivedPenalty;

                quantityPlayerMoves = quantityMoves;
            }
        }
    }
}