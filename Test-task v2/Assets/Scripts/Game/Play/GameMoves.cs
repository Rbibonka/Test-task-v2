using System.Collections;
using UnityEngine;

namespace Game
{
    namespace Play
    {
        public class GameMoves : MonoBehaviour
        {
            [SerializeField]
            private Transform wheelPoint;

            [SerializeField]
            private SpinWheel spinWheel;

            private MovementPlayer[] movementPlayer;

            private int[] finishers;

            private int numberFinishers;

            private bool somePlayerFinished;

            private bool playerAdded;

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
                GlobalUIEventManager.OnButtonRollDiceClick += StartSpin;
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

                finishers = new int[players.Length];

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

            private IEnumerator Gameplay()
            {
                while (true)
                {
                    for(int i = 0; i < movementPlayer.Length; i++)
                    {
                        do
                        {
                            if (movementPlayer[i].GetPlayerFinished)
                            {
                                break;
                            }
                            else
                            {
                                moveStatus = MoveStatus.finishMove;

                                endMove = false;

                                currentStateWheel = SpinWheel.CurrentStateWheel.idle;

                                GlobalUIEventManager.OnChangePlayer?.Invoke(movementPlayer[i].GetPlayerParameters.GetPlayerName, movementPlayer[i].GetCurrentPlatform);

                                GlobalEventManager.OnChangedTrackingTarget?.Invoke(wheelPoint, spinWheel.GetDistanceFromWheel);

                                yield return new WaitUntil(() => currentStateWheel == SpinWheel.CurrentStateWheel.rotate);

                                spinWheel.StartSpin();

                                yield return new WaitUntil(() => currentStateWheel == SpinWheel.CurrentStateWheel.stopped);

                                GlobalUIEventManager.OnChangeNumberFromWheel?.Invoke(spinWheel.GetQuantityMoves);

                                GlobalEventManager.OnChangedTrackingTarget?.Invoke(movementPlayer[i].transform, movementPlayer[i].GetDistanceFromPlayer);

                                yield return new WaitForSeconds(2f);

                                movementPlayer[i].Move(spinWheel.GetQuantityMoves);

                                yield return new WaitUntil(() => endMove == true);
                            }

                        } while (moveStatus == MoveStatus.repeatMove);
                    }
                }
            }

            private void AddedFinisher()
            {

            }

            private void OnDisable()
            {
                GlobalUIEventManager.OnButtonRollDiceClick -= StartSpin;
            }
        }
    }
}