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

            private void OnEnable()
            {
                GlobalEventManager.OnPathBuilt += ActivateGameCanvas;
            }

            private void ActivateGameCanvas()
            {
                gameCanvas.enabled = true;
            }

            private void OnDisable()
            {
                GlobalEventManager.OnPathBuilt -= ActivateGameCanvas;
            }
        }
    }
}