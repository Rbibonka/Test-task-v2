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

            [SerializeField]
            private Canvas pauseCanvas;

            private void OnEnable()
            {
                GlobalEventManager.OnPathBuilt += StartGame;

                GlobalEventManager.OnEndGame += EndGame;

                GlobalUIEventManager.OnButtonPauseClick += PauseGame;

                GlobalUIEventManager.OnButtonContinueClick += ContinueGame;
            }

            private void ContinueGame()
            {
                PausePanelStateChanges();
            }

            private void PausePanelStateChanges()
            {
                gameCanvas.enabled = !gameCanvas.enabled;

                pauseCanvas.enabled = !pauseCanvas.enabled;
            }

            private void PauseGame()
            {
                PausePanelStateChanges();
            }

            private void EndGame()
            {
                gameCanvas.enabled = false;

                endGameCanvas.enabled = true;
            }

            private void StartGame()
            {
                gameCanvas.enabled = true;
            }

            private void OnDisable()
            {
                GlobalEventManager.OnPathBuilt -= StartGame;

                GlobalEventManager.OnEndGame -= EndGame;

                GlobalUIEventManager.OnButtonPauseClick -= PauseGame;

                GlobalUIEventManager.OnButtonContinueClick -= ContinueGame;

            }
        }
    }
}