using UnityEngine;

namespace Game
{
    namespace Play
    {
        public class SwitcherCanvas : MonoBehaviour
        {
            [Header("UI Objects")]
            [SerializeField]
            private Canvas gameCanvas;

            [SerializeField]
            private Canvas endGameCanvas;

            private void OnEnable()
            {
                GlobalEventManager.OnPathBuilt += ActivateGameCanvas;

                GlobalEventManager.OnEndGame += EndGame;
            }

            private void EndGame()
            {
                gameCanvas.enabled = false;

                endGameCanvas.enabled = true;
            }

            private void ActivateGameCanvas()
            {
                gameCanvas.enabled = true;
            }

            private void OnDisable()
            {
                GlobalEventManager.OnPathBuilt -= ActivateGameCanvas;

                GlobalEventManager.OnEndGame -= EndGame;
            }
        }
    }
}