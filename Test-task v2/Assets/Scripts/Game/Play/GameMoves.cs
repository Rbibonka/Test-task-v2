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

            private int playersLeft;

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

            public void StartMove(GameObject[] players, Transform[] platforms)
            {
                movementPlayer = new MovementPlayer[players.Length];

                playersLeft = players.Length;

                for (int i = 0; i < players.Length; i++)
                {
                    movementPlayer[i] = players[i].GetComponentInChildren<MovementPlayer>();

                    movementPlayer[i].SetPlatforms();
                }

                StartCoroutine(Gameplay());
            }

            public void FinishSpinWheel()
            {
                currentStateWheel = spinWheel.GetCurrentStateWheel;
            }

            public void CompletePlayerMove()
            {
                endMove = true;
            }

            public void RepeatMove()
            {
                moveStatus = MoveStatus.repeatMove;
                endMove = true;
            }

            private IEnumerator Gameplay()
            {
                while (true)
                {
                    foreach(var player in movementPlayer)
                    {
                        do
                        {
                            moveStatus = MoveStatus.finishMove;

                            endMove = false;

                            currentStateWheel = SpinWheel.CurrentStateWheel.idle;

                            if (player.GetPlayerFinished)
                            {

                                break;
                            }
                            else
                            {
                                GlobalUIEventManager.OnChangePlayer?.Invoke(player.GetPlayerParameters.GetPlayerName, player.GetCurrentPlatform);

                                GlobalEventManager.OnChangedTrackingTarget?.Invoke(wheelPoint, spinWheel.GetDistanceFromWheel);

                                yield return new WaitUntil(() => currentStateWheel == SpinWheel.CurrentStateWheel.rotate);

                                spinWheel.StartSpin();

                                yield return new WaitUntil(() => currentStateWheel == SpinWheel.CurrentStateWheel.stopped);

                                GlobalUIEventManager.OnChangeNumberFromWheel?.Invoke(spinWheel.GetQuantityMoves);

                                GlobalEventManager.OnChangedTrackingTarget?.Invoke(player.transform, player.GetDistanceFromPlayer);

                                yield return new WaitForSeconds(2f);

                                player.Move(spinWheel.GetQuantityMoves);

                                yield return new WaitUntil(() => endMove == true);
                            }
                            
                        } while (moveStatus == MoveStatus.repeatMove);
                    }
                }
            }

            private void OnDisable()
            {
                GlobalUIEventManager.OnButtonRollDiceClick -= StartSpin;
            }
        }
    }
}