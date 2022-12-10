using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideValue : MonoBehaviour
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
