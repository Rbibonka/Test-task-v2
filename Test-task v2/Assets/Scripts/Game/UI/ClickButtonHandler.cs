using UnityEngine;

namespace Game
{
    namespace Play
    {
        public class ClickButtonHandler : MonoBehaviour
        {
            public void OnButtonStartClick()
            {
                GlobalUIEventManager.OnButtonStartClick?.Invoke();
            }

            public void OnButtonSpinWheelClick()
            {
                GlobalUIEventManager.OnButtonSpinWheelClick?.Invoke();
            }

            public void OnButtonPauseClick()
            {
                GlobalUIEventManager.OnButtonSpinWheelClick?.Invoke();
            }

            public void OnButtonBackClick()
            {
                GlobalUIEventManager.OnButtonBackClick?.Invoke();
            }
        }
    }
}

