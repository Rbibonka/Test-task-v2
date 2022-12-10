using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private int[] h = { 0, 2, 3, 4, 5 };

    private void Start()
    {
        foreach (var i in h)
        {
            Debug.Log(i);
        }
    }
}
