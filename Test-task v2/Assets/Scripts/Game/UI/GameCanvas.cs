using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    namespace Play
    {
        public class GameCanvas : MonoBehaviour
        {
            [Header("UI elements")]
            [SerializeField]
            private Text currentPlatform;

            [SerializeField]
            private Text numberFromWheel;

            [SerializeField]
            private Text currentPlayer;

            [SerializeField]
            private Button spinWheel;

            private void OnEnable()
            {
                GlobalUIEventManager.OnChangedPlayer += SetCurrentPlayer;

                GlobalUIEventManager.OnChangedNumberFromWheel += SetNumberFromWheel;
            }

            private void SetCurrentPlayer(string currentPlayerName, int currentPlayerPlatform)
            {
                currentPlayer.text = currentPlayerName;
                currentPlatform.text = currentPlayerPlatform.ToString();
                numberFromWheel.text = "";

                spinWheel.enabled = true;
            }

            private void SetNumberFromWheel(int number)
            {
                numberFromWheel.text = number.ToString();

                spinWheel.enabled = false;
            }

            private void OnDisable()
            {
                GlobalUIEventManager.OnChangedPlayer -= SetCurrentPlayer;

                GlobalUIEventManager.OnChangedNumberFromWheel -= SetNumberFromWheel;
            }
        }
    }
}