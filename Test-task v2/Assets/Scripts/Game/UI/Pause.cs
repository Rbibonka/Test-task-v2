using UnityEngine;

namespace Game
{
    namespace Play
    {
        public class Pause : MonoBehaviour
        {
            private void OnEnable()
            {
                GlobalUIEventManager.OnButtonPauseClick += ChangeStatePause;

                GlobalUIEventManager.OnButtonContinueClick += ChangeStatePause;

                GlobalUIEventManager.OnButtonBackClick += OpenMenu;
            }

            private void ChangeStatePause()
            {
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
                else
                {
                    Time.timeScale = 0;
                }
            }

            private void OpenMenu()
            {
                Time.timeScale = 1;
            }

            private void OnDisable()
            {
                GlobalUIEventManager.OnButtonPauseClick -= ChangeStatePause;

                GlobalUIEventManager.OnButtonContinueClick -= ChangeStatePause;

                GlobalUIEventManager.OnButtonBackClick -= OpenMenu;
            }
        }
    }
}
