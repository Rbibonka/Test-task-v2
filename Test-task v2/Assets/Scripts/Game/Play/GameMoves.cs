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

            private MovementPlayer[] playerMoves;

            private bool nextMove;

            private bool finishMove;

            private SpinWheel.CurrentStateWheel currentStateWheel;

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
                playerMoves = new MovementPlayer[players.Length];

                for (int i = 0; i < players.Length; i++)
                {
                    playerMoves[i] = players[i].GetComponentInChildren<MovementPlayer>();

                    playerMoves[i].SetPlatforms();
                }

                StartCoroutine(Gameplay());
            }

            public void FinishScrollingWheel()
            {
                currentStateWheel = spinWheel.GetCurrentStateWheel;
            }

            public void CompletePlayerMove()
            {
                nextMove = true;
            }

            public void RepeatMove()
            {
                finishMove = true;
            }

            private IEnumerator Gameplay()
            {
                while (true)
                {
                    foreach(var player in playerMoves)
                    {
                        nextMove = false;

                        currentStateWheel = SpinWheel.CurrentStateWheel.idle;

                        GlobalEventManager.OnChangedTrackingTarget?.Invoke(wheelPoint, spinWheel.GetDistanceFromWheel);

                        yield return new WaitUntil(() => currentStateWheel == SpinWheel.CurrentStateWheel.rotate);

                        spinWheel.StartSpin();

                        yield return new WaitUntil(() => currentStateWheel == SpinWheel.CurrentStateWheel.stopped);

                        GlobalEventManager.OnChangedTrackingTarget?.Invoke(player.transform, player.GetDistanceFromPlayer);

                        player.Move(spinWheel.GetQuantityMoves);

                        yield return new WaitUntil(() => nextMove == true);
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