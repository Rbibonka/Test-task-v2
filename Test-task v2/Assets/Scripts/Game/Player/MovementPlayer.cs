using System.Collections;
using UnityEngine;

namespace Game
{
    namespace Play
    {
        [RequireComponent(typeof(Rigidbody))]
        [RequireComponent(typeof(MeshCollider))]
        public class MovementPlayer : PlayerParameters
        {
            [Range(0, 3)]
            [SerializeField]
            private float playerSpeed;

            [SerializeField]
            private Transform movePoint;

            [SerializeField]
            private GameMoves gameMoves;

            private Transform[] platforms;

            private float minDistance = 0.001f;

            [SerializeField]
            private bool isRollDice = false;

            public enum PlayerState
            {
                idle,
                moving,
                landed
            }

            public enum MoveDirection 
            { 
                forward,
                back
            }

            private MoveDirection moveDirection;

            [SerializeField]
            private PlayerState playerState;

            private void Start()
            {
                GlobalUIEventManager.OnButtonRollDiceClick += RollDiceClick;
            }

            public void SetPlatforms(Transform[] platforms)
            {
                this.platforms = platforms;
            }

            private void RollDiceClick()
            {
                isRollDice = true;
            }

            public IEnumerator RollDice()
            {
                isRollDice = false;

                //yield return new WaitForSeconds(0.01f);

                yield return new WaitUntil(() => isRollDice == true);

                diceNumber = Random.Range(1, 7);

                CalculatingPath(diceNumber);
            }

            private void CalculatingPath(int additionalNumber)
            {
                Debug.Log(diceNumber);

                finishPlatform = currentPlatform + additionalNumber;

                if (finishPlatform < currentPlatform)
                {
                    moveDirection = MoveDirection.back;
                }
                else if (finishPlatform > currentPlatform)
                {
                    moveDirection = MoveDirection.forward;
                }

                playerState = PlayerState.moving;
            }

            private void Update()
            {
                if (playerState == PlayerState.moving)
                {
                    if (Vector3.Distance(movePoint.position, platforms[finishPlatform].position) < minDistance)
                    {
                        playerState = PlayerState.landed;
                    }
                    else
                    {
                        movePoint.position = Vector3.MoveTowards(movePoint.position, platforms[currentPlatform].position, playerSpeed * Time.deltaTime);

                        if (moveDirection == MoveDirection.forward)
                        {
                            PlayerMovingForward();
                        }
                        else if (moveDirection == MoveDirection.back)
                        {
                            PlayerMovingBack();
                        }
                    }
                }
            }

            private void PlayerMovingForward()
            {
                if (Vector3.Distance(movePoint.position, platforms[currentPlatform].position) < minDistance)
                {
                    if (currentPlatform < finishPlatform)
                    {
                        currentPlatform++;
                    }
                }
            }

            private void PlayerMovingBack()
            {
                if (Vector3.Distance(movePoint.position, platforms[currentPlatform].position) < minDistance)
                {
                    currentPlatform--;
                }
            }

            private void OnTriggerStay(Collider other)
            {
                if (playerState == PlayerState.landed)
                {
                    if (other.TryGetComponent(out DefaultPlatform defaultPlatform))
                    {
                        playerState = PlayerState.idle;

                        gameMoves.CompletePlayerMove();
                    }
                    else if (other.TryGetComponent(out PenaltyPlatform penaltyPlatform))
                    {
                        playerState = PlayerState.idle;

                        CalculatingPath(-3);
                    }
                    else if (other.TryGetComponent(out BonusPlatform bonusPlatform))
                    {
                        StartCoroutine(RollDice());
                    }
                }
            }
            private void OnDisable()
            {
                GlobalUIEventManager.OnButtonRollDiceClick -= RollDiceClick;
            }
        }
    }
}