using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    namespace Play
    {
        public class SwitcherView : MonoBehaviour
        {
            [SerializeField]
            private Canvas gameCanvas;

            private void Awake()
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