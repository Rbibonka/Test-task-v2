using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    namespace Play
    {
        public class GameCanvas : MonoBehaviour
        {
            [SerializeField]
            private Text currentPlatform;

            [SerializeField]
            private Text numberFromWheel;

            [SerializeField]
            private Text currentPlayer;

            private void OnEnable()
            {
                GlobalUIEventManager.OnChangePlayer += SetCurrentPlayer;

                GlobalUIEventManager.OnChangeNumberFromWheel += SetNumberFromWheel;
            }

            private void SetCurrentPlayer(string currentPlayerName, int currentPlayerPlatform)
            {
                currentPlayer.text = currentPlayerName;
                currentPlatform.text = currentPlayerPlatform.ToString();
                numberFromWheel.text = "";
            }

            private void SetNumberFromWheel(int number)
            {
                numberFromWheel.text = number.ToString();
            }

            private void OnDisable()
            {
                GlobalUIEventManager.OnChangePlayer -= SetCurrentPlayer;

                GlobalUIEventManager.OnChangeNumberFromWheel -= SetNumberFromWheel;
            }
        }
    }
}