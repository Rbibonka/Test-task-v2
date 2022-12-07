using UnityEngine;
using System;

namespace Game 
{ 
    namespace Play
    {
        public class GlobalEventManager
        {
            public static Action<Transform> OnChangedTrackingTarget;
            public static Action OnRollDice;
            public static Action OnPlayerActivated;
            public static Action OnPathBuilt;
            public static Action OnStartGame;
        }
    }
}