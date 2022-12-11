using UnityEngine;

namespace Game
{
    namespace Menu
    {
        public class MenuButtonHandler : MonoBehaviour
        {
            public void StartLevelClick()
            {
                MenuEventManager.StartLevelClick?.Invoke();
            }

            public void ExitGameClick()
            {
                MenuEventManager.ExitGameClick?.Invoke();
            }
        }
    }
}

