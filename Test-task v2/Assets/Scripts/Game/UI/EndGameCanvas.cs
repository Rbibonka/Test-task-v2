using UnityEngine;
using UnityEngine.UI;


namespace Game
{
    namespace Play
    {
        public class EndGameCanvas : MonoBehaviour
        {
            [SerializeField]
            private GameStatistics gameStatistics;

            [Header("Rows")]
            [SerializeField]
            private Text[] playerPlace;

            [SerializeField]
            private Text[] playerName;

            [SerializeField]
            private Text[] quantityMoves;
            
            [SerializeField]
            private Text[] quantityBonuses;

            [SerializeField]
            private Text[] quantityPenalties;

            private void OnEnable()
            {
                GlobalEventManager.OnEndGame += FillTable;
            }

            private void FillTable()
            {
                for (int i = 0; i < gameStatistics.GetPlayers.Length; i++)
                {
                    playerPlace[i].text = $"{i + 1}";

                    playerName[i].text = gameStatistics.GetPlayers[i].GetPlayerParameters.GetPlayerName;

                    quantityMoves[i].text = gameStatistics.GetPlayers[i].GetPlayerParameters.GetQuantityMoves.ToString();

                    quantityBonuses[i].text = gameStatistics.GetPlayers[i].GetPlayerParameters.GetQuantityReceivedBonus.ToString();

                    quantityPenalties[i].text = gameStatistics.GetPlayers[i].GetPlayerParameters.GetQuantityReceivedPenalty.ToString();
                }
            }

            private void OnDisable()
            {
                GlobalEventManager.OnEndGame -= FillTable;
            }
        }
    }
}
