using UnityEngine;

public class ClickButtonHandler : MonoBehaviour
{
    public void OnButtonStartClick()
    {
        GlobalUIEventManager.OnButtonStartClick?.Invoke();
    }

    public void OnButtonRollDiceClick()
    {
        GlobalUIEventManager.OnButtonRollDiceClick?.Invoke();
    }
}
