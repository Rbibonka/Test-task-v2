using System.Collections;
using UnityEngine;

namespace Game
{
    namespace Play
    {
        [RequireComponent(typeof(GameStatistics))]
        public class GameMoves : MonoBehaviour, IGameMovable
        {
            [SerializeField]
            private float timeWaitCamera;

            [Header("Position")]
            [SerializeField]
            private Transform wheelPoint;

            [Header("Support script")]
            [SerializeField]
            private SpinWheel spinWheel;

            private MovementPlayer[] movementPlayer;

            private MovementPlayer[] finishers;

            private GameStatistics gameStatistics;

            private int quantityFinishers;

            private bool endMove;

            private SpinWheel.CurrentStateWheel currentStateWheel;

            public enum MoveStatus
            {
                finishMove,
                repeatMove
            }

            private MoveStatus moveStatus;

            private void OnEnable()
            {
                GlobalUIEventManager.OnButtonSpinWheelClick += StartSpin;
            }

            private void Start()
            {
                gameStatistics = GetComponent<GameStatistics>();
            }

            private void StartSpin()
            {
                currentStateWheel = SpinWheel.CurrentStateWheel.rotate;
            }

            public void StartMove(GameObject[] players)
            {
                SetParameters(players);

                StartCoroutine(Gameplay());
            }

            private void SetParameters(GameObject[] players)
            {
                movementPlayer = new MovementPlayer[players.Length];

                finishers = new MovementPlayer[players.Length];

                for (int i = 0; i < players.Length; i++)
                {
                    movementPlayer[i] = players[i].GetComponentInChildren<MovementPlayer>();

                    movementPlayer[i].SetPlatforms();
                }
            }

            public void FinishSpinWheel()
            {
                currentStateWheel = spinWheel.GetCurrentStateWheel;
            }

            public void CompletePlayerMove()
            {
                endMove = true;
            }

            public void RepeatPlayerMove()
            {
                moveStatus = MoveStatus.repeatMove;
                endMove = true;
            }

            private void WaitStartSpin(int numberPlayer)
            {
                GlobalUIEventManager.OnChangedPlayer?.Invoke(movementPlayer[numberPlayer].GetPlayerParameters.GetPlayerName, movementPlayer[numberPlayer].GetCurrentPlatform);

                GlobalEventManager.OnChangedTrackingTarget?.Invoke(wheelPoint, spinWheel.GetDistanceFromWheel);
            }

            private void PreparationPlayerMove(int numberPlayer)
            {
                GlobalUIEventManager.OnChangedNumberFromWheel?.Invoke(spinWheel.GetQuantityMoves);

                GlobalEventManager.OnChangedTrackingTarget?.Invoke(movementPlayer[numberPlayer].transform, movementPlayer[numberPlayer].GetDistanceFromPlayer);
            }

            private IEnumerator Gameplay()
            {
                while (true)
                {
                    for(int i = 0; i < movementPlayer.Length; i++)
                    {
                        do
                        {
                            moveStatus = MoveStatus.finishMove;

                            endMove = false;

                            currentStateWheel = SpinWheel.CurrentStateWheel.idle;

                            if (movementPlayer[i].GetPlayerFinished)
                            {
                                break;
                            }
                            else
                            {
                                WaitStartSpin(i);

                                yield return new WaitUntil(() => currentStateWheel == SpinWheel.CurrentStateWheel.rotate);

                                spinWheel.StartSpin();

                                yield return new WaitUntil(() => currentStateWheel == SpinWheel.CurrentStateWheel.stopped);

                                PreparationPlayerMove(i);

                                yield return new WaitForSeconds(timeWaitCamera);

                                movementPlayer[i].Move(spinWheel.GetQuantityMoves);

                                yield return new WaitUntil(() => endMove == true);

                                if (movementPlayer[i].GetPlayerFinished)
                                {
                                    finishers[quantityFinishers] = movementPlayer[i];

                                    quantityFinishers++;

                                    break;
                                }
                            }

                        } while (moveStatus == MoveStatus.repeatMove);
                    }

                    if (quantityFinishers >= movementPlayer.Length)
                    {
                        break;
                    }
                }

                gameStatistics.SetStatistics(finishers);

                GlobalEventManager.OnEndGame?.Invoke();
            }

            private void OnDisable()
            {
                GlobalUIEventManager.OnButtonSpinWheelClick -= StartSpin;
            }
        }
    }
}