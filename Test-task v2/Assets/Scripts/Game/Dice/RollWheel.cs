using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollWheel : MonoBehaviour
{
    [SerializeField]
    private Rigidbody wheelBody;

    private float x = 2500;

    private bool a;

    [Header("Game objects")]
    [SerializeField]
    private GameObject[] numbersPoints;

    private void Start()
    {
        StartCoroutine(i());
    }

    private void FixedUpdate()
    {
        wheelBody.AddTorque(x, 0, 0);
    }

    private IEnumerator i()
    {
        yield return new WaitForSeconds(4f);

        x = 0;

        yield return new WaitUntil(() => wheelBody.angularVelocity.x <= 0f);

        Debug.Log(CheckResultRoll());
    }

    private int CheckResultRoll()
    {
        GameObject maxPoint;

        maxPoint = numbersPoints[0];

        for (int i = 1; i < numbersPoints.Length; i++)
        {
            if (numbersPoints[i].transform.position.y > maxPoint.transform.position.y)
            {
                maxPoint = numbersPoints[i];
            }
        }

        return maxPoint.GetComponent<SideValue>().GetSideValue;
    }
}
