using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    namespace Play
    {
        [RequireComponent(typeof(Rigidbody))]
        public class SpinWheel : Wheel
        {
            [Range(0,100)]
            [SerializeField]
            private float rotateSpeed;

            [Range(0, 10)]
            [SerializeField]
            private float defaultSpeed;

            [SerializeField]
            private Vector3 distanceFromWheel;

            [SerializeField]
            private GameObject[] numberPoints;

            [SerializeField]
            private GameMoves gameMoves;

            private float maxRotateVelocity = 6f;

            private Rigidbody wheelBody;

            public enum CurrentStateWheel
            {
                idle,
                rotate,
                slowing,
                stopped
            }

            private CurrentStateWheel currentStateWheel;

            public Vector3 GetDistanceFromWheel
            {
                get
                {
                    return distanceFromWheel;
                }
            }

            public CurrentStateWheel GetCurrentStateWheel
            {
                get
                {
                    return currentStateWheel;
                }
            }

            public int GetQuantityMoves
            {
                get
                {
                    return quantityMoves;
                }
            }

            private void Start()
            {
                wheelBody = GetComponent<Rigidbody>();
            }

            public void StartSpin()
            {
                wheelBody.angularDrag = Random.Range(0.2f, 1f);

                rotateSpeed = Random.Range(-100, 100);

                currentStateWheel = CurrentStateWheel.rotate;
            }

            private void Update()
            {
                if (currentStateWheel == CurrentStateWheel.idle)
                {
                    wheelBody.AddTorque(0f, 0f, defaultSpeed);
                }
                else if (currentStateWheel == CurrentStateWheel.rotate)
                {
                    if (Mathf.Abs(wheelBody.angularVelocity.z) < Mathf.Abs(maxRotateVelocity))
                    {
                        wheelBody.AddTorque(0f, 0f, rotateSpeed);
                    }
                    else
                    {
                        currentStateWheel = CurrentStateWheel.slowing;
                    }
                }
                else if (wheelBody.angularVelocity == Vector3.zero)
                {
                    quantityMoves = CheckResultSpin();

                    currentStateWheel = CurrentStateWheel.stopped;

                    gameMoves.FinishScrollingWheel();

                    currentStateWheel = CurrentStateWheel.idle;
                }
            }

            protected override int CheckResultSpin()
            {
                GameObject maxPoint;

                maxPoint = numberPoints[0];

                for (int i = 1; i < numberPoints.Length; i++)
                {
                    if (numberPoints[i].transform.position.y > maxPoint.transform.position.y)
                    {
                        maxPoint = numberPoints[i];
                    }
                }

                return maxPoint.GetComponent<SideValue>().GetSideValue;
            }
        }
    }
}
