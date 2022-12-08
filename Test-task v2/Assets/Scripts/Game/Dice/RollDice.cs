using System.Collections;
using UnityEngine;

namespace Game
{
    namespace Play
    {
        public class RollDice : Dice
        {
            [Header("Dice parameters")]
            [Range(0, 30)]
            [SerializeField]
            private float impulseForce;

            [Range(-100, 100)]
            [SerializeField]
            private float minRangeValue;

            [Range(-100, 100)]
            [SerializeField]
            private float maxRangeValue;

            [Header("Game objects")]
            [SerializeField]
            private GameObject[] numbersPoints;

            [SerializeField]
            private MovementPlayer movementPlayer;

            private int diceNumber;

            private Rigidbody diceBody;

            private Vector3 direction;

            public int GetDiceNumber
            {
                get
                {
                    return diceNumber;
                }
            }

            private void Start()
            {
                currentState = CurrentState.idle;

                diceBody = GetComponent<Rigidbody>();
            }

            private void ThrowDice()
            {
                diceBody.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

                direction.x = Random.Range(minRangeValue, maxRangeValue);
                direction.y = Random.Range(minRangeValue, maxRangeValue);
                direction.z = Random.Range(minRangeValue, maxRangeValue);

                diceBody.AddTorque(direction);
            }

            public IEnumerator WaitLandedDice()
            {
                currentState = CurrentState.moving;

                ThrowDice();

                yield return new WaitUntil(() => currentState == CurrentState.landed);
            }

            private void FixedUpdate()
            {
                if (diceBody.velocity == Vector3.zero)
                {
                    if (currentState == CurrentState.moving)
                    {
                        currentState = CurrentState.landed;
                    }
                }

                if (currentState == CurrentState.landed)
                {
                    currentState = CurrentState.idle;
                }
            }

            protected override int CheckResultRoll()
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
    }
}

