using UnityEngine;
using System;

namespace Game
{
    namespace Play
    {
        public class GlobalUIEventManager : MonoBehaviour
        {
            public static Action OnButtonStartClick;
            public static Action OnButtonSpinWheelClick;
            public static Action OnButtonPauseClick;
            public static Action OnButtonContinueClick;
            public static Action OnButtonBackClick;
            public static Action<string, int> OnChangedPlayer;
            public static Action<int> OnChangedNumberFromWheel;
        }
    }
}
