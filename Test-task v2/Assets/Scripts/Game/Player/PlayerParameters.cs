using UnityEngine;

namespace Game
{
    namespace Play
    {
        public class PlayerParameters : Player
        {
            public string PlayerName
            {
                get
                {
                    return playerName;
                }
            }

            public void SettingParameters(int numberPlayer)
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
        }
    }
}