using System.Collections;
using UnityEngine;

namespace Game
{
    namespace Play
    {
        public class GameMoves : MonoBehaviour
        {
            [SerializeField]
            private string currentPlayer;

            private GameObject[] players;

            private Transform[] platforms;

            private MovementPlayer[] playerMoves;

            private bool isNextMove;

            [SerializeField]
            private int a;

            private void Start()
            {
                GlobalUIEventManager.OnButtonRollDiceClick += RollDiceClick;
            }

            public void StartMove(GameObject[] players, Transform[] platforms)
            {
                playerMoves = new MovementPlayer[players.Length];

                for (int i = 0; i < players.Length; i++)
                {
                    playerMoves[i] = players[i].GetComponentInChildren<MovementPlayer>();

                    playerMoves[i].SetPlatforms(platforms);
                }

                this.platforms = platforms;

                StartCoroutine(Gameplay());
            }

            private void RollDiceClick()
            {

            }

            public void CompletePlayerMove()
            {
                isNextMove = true;
            }

            private IEnumerator Gameplay()
            {
                while (true)
                {
                    foreach(var player in playerMoves)
                    {
                        GlobalEventManager.OnChangedTrackingTarget?.Invoke(player.transform);

                        StartCoroutine(player.RollDice());

                        yield return new WaitUntil(() => isNextMove == true);

                        isNextMove = false;
                    }
                    a++;
                }
            }

            private void OnDisable()
            {
                GlobalUIEventManager.OnButtonRollDiceClick -= RollDiceClick;
            }
        }
    }
}