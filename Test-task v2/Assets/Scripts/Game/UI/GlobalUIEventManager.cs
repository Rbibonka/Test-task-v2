using UnityEngine;
using System;

public class GlobalUIEventManager : MonoBehaviour
{
    public static Action OnButtonStartClick;
    public static Action OnButtonRollDiceClick;
    public static Action<string, int> OnChangePlayer;
    public static Action<int> OnChangeNumberFromWheel;
}
