using System.Collections;
using UnityEngine;

namespace Game
{
    namespace Play
    {
        [RequireComponent(typeof(Rigidbody))]
        [RequireComponent(typeof(MeshCollider))]
        [RequireComponent(typeof(PlayerParameters))]
        public class MovementPlayer : Player
        {
            [Header("Parameters player")]
            [Range(0, 3)]
            [SerializeField]
            private float playerSpeed;

            [SerializeField]
            private int currentPlatform;

            [SerializeField]
            private int finishPlatform;

            [SerializeField]
            private Vector3 distanceFromPlayer;

            [Header("Game object")]
            [SerializeField]
            private Transform movePoint;

            [Header("Supprot scripts")]
            [SerializeField]
            private GameMoves gameMoves;

            [SerializeField]
            private LevelParameters levelParameters;

            private PlayerParameters playerParameters;

            private Transform[] platforms;

            private float minDistance = 0.001f;

            private bool playerFinished;

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

            private PlayerState playerState;

            public int GetCurrentPlatform
            {
                get
                {
                    return currentPlatform;
                }
            }

            public PlayerParameters GetPlayerParameters
            {
                get
                {
                    return playerParameters;
                }
            }

            public bool GetPlayerFinished
            {
                get
                {
                    return playerFinished;
                }
            }

            public Vector3 GetDistanceFromPlayer
            {
                get
                {
                    return distanceFromPlayer;
                }
            }

            private void OnEnable()
            {
                playerParameters = GetComponent<PlayerParameters>();
            }

            public void SetPlatforms()
            {
                platforms = levelParameters.GetPlatfromPoints;
            }

            public void Move(int quantityMoves)
            {
                finishPlatform = currentPlatform + quantityMoves;

                if (finishPlatform > platforms.Length)
                {
                    finishPlatform = platforms.Length - 1;
                }

                if (quantityMoves < 0)
                {
                    moveDirection = MoveDirection.back;
                }
                else
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

            protected override void PlayerMovingForward()
            {
                if (Vector3.Distance(movePoint.position, platforms[currentPlatform].position) < minDistance)
                {
                    if (currentPlatform < finishPlatform)
                    {
                        currentPlatform++;
                    }
                    else if (currentPlatform >= platforms.Length - 1)
                    {
                        SetPlayerStatistick();

                        playerFinished = true;
                    }
                }
            }

            protected override void PlayerMovingBack()
            {
                if (Vector3.Distance(movePoint.position, platforms[currentPlatform].position) < minDistance)
                {
                    if (currentPlatform > finishPlatform)
                    {
                        currentPlatform--;
                    }
                }
            }

            private IEnumerator Delay()
            {
                yield return new WaitForSeconds(1f);

                Move(levelParameters.GetPenaltyNumberPlatforms);
            }

            private void SetPlayerStatistick()
            {
                playerParameters.SetPlayerStatistics(playerReceivedBonus, playerReceivedPenalty, playerMoves);
            }

            private void OnTriggerStay(Collider other)
            {
                if (playerState == PlayerState.landed)
                {
                    playerState = PlayerState.idle;

                    if (other.TryGetComponent(out DefaultPlatform defaultPlatform))
                    {
                        playerMoves++;

                        gameMoves.CompletePlayerMove();
                    }
                    else if (other.TryGetComponent(out PenaltyPlatform penaltyPlatform))
                    {
                        playerReceivedPenalty++;

                        StartCoroutine(Delay());
                    }
                    else if (other.TryGetComponent(out BonusPlatform bonusPlatform))
                    {
                        playerReceivedBonus++;

                        playerMoves++;

                        gameMoves.RepeatMove();
                    }
                }
            }
        }
    }
}