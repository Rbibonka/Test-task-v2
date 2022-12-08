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
            [Header("Parameters player")]
            [Range(0, 3)]
            [SerializeField]
            private float playerSpeed;

            [SerializeField]
            private int currentPlatform;

            [SerializeField]
            private int finishPlatform;

            private Vector3 distanceFromPlayer;

            [Header("Game object")]
            [SerializeField]
            private Transform movePoint;

            [Header("Supprot scripts")]
            [SerializeField]
            private GameMoves gameMoves;

            [SerializeField]
            private LevelParameters levelParameters;

            private Transform[] platforms;

            private float minDistance = 0.001f;

            private void Start()
            {
                distanceFromPlayer = new Vector3(0.7f, 0.5f, 0f);
            }

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

            public Vector3 GetDistanceFromPlayer
            {
                get
                {
                    return distanceFromPlayer;
                }
            }

            public void SetPlatforms()
            {
                platforms = levelParameters.GetPlatfromPoints;
            }

            public void Move(int quantityMoves)
            {
                finishPlatform = currentPlatform + quantityMoves;

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

            private void OnTriggerStay(Collider other)
            {
                if (playerState == PlayerState.landed)
                {
                    if (other.TryGetComponent(out DefaultPlatform defaultPlatform))
                    {
                        playerState = PlayerState.idle;

                        gameMoves.CompletePlayerMove();

                        //gameMoves.RepeatMove();
                    }
                    else if (other.TryGetComponent(out PenaltyPlatform penaltyPlatform))
                    {
                        playerState = PlayerState.idle;

                        StartCoroutine(Delay());
                    }
                    else if (other.TryGetComponent(out BonusPlatform bonusPlatform))
                    {
                        //gameMoves.RepeatMove();
                    }
                }
            }
        }
    }
}