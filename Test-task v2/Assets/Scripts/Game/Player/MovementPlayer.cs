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
                movingBack,
                landed
            }

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

                yield return new WaitUntil(() => isRollDice == true);

                diceNumber = Random.Range(1, 7);

                CalculatingPath();
            }

            private void CalculatingPath()
            {
                Debug.Log(diceNumber);

                finishPlatform = currentPlatform + diceNumber;

                playerState = PlayerState.moving;
            }

            private void Back()
            {
                currentPlatform--;

                finishPlatform = currentPlatform - 3;

                playerState = PlayerState.movingBack;
            }

            private void Update()
            {
                if (playerState == PlayerState.moving)
                {
                    PlayerMovingForward();
                }
                else if (playerState == PlayerState.movingBack)
                {
                    PlayerMovingBack();
                }
            }

            private void PlayerMovingBack()
            {
                if (currentPlatform == finishPlatform)
                {
                    playerState = PlayerState.landed;

                    gameMoves.CompletePlayerMove();
                }
                else
                {
                    movePoint.position = Vector3.MoveTowards(movePoint.position, platforms[currentPlatform].position,
                        playerSpeed * Time.deltaTime);

                    if (Vector3.Distance(movePoint.position, platforms[currentPlatform].position) < minDistance)
                    {
                        currentPlatform--;
                    }
                }
            }

            private void PlayerMovingForward()
            {
                if (currentPlatform == finishPlatform)
                {
                    playerState = PlayerState.landed;
                }
                else
                {
                    movePoint.position = Vector3.MoveTowards(movePoint.position, platforms[currentPlatform].position,
                        playerSpeed * Time.deltaTime);

                    if (Vector3.Distance(movePoint.position, platforms[currentPlatform].position) < minDistance)
                    {
                        currentPlatform++;
                    }
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
                        // Debug.Log("пизедц");
                        
                        Back();
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