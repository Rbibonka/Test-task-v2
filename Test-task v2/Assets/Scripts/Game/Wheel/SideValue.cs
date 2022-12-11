using UnityEngine;

public class SideValue : MonoBehaviour, ISideValue
{
    [SerializeField]
    private int sideValue;

    public int GetSideValue
    {
        get
        {
            return sideValue;
        }
    }

}
