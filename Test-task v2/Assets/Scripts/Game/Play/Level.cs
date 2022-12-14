using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    namespace Play
    {
        public class Level : MonoBehaviour
        {
            [Header("Support scripts")]
            [SerializeField]
            private LevelParameters levelParameters;

            [SerializeField]
            private PathCreator pathCreator;

            [SerializeField]
            private PlayersActivator playersActivator;

            [SerializeField]
            private GameMoves gameMoves;

            private void OnEnable()
            {
                GlobalEventManager.OnPlayerActivated += StartGame;

                GlobalEventManager.OnStartGame += BuildPath;

                GlobalEventManager.OnPathBuilt += SpawnPlayer;

                GlobalUIEventManager.OnButtonBackClick += OpenMenu;
            }

            private void OpenMenu()
            {
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }

            private void BuildPath()
            {
                pathCreator.StartBuildPlatforms(levelParameters.GetQuantitySpecialPlatforms, levelParameters.GetPlatfromPoints);
            }

            private void SpawnPlayer()
            {
                playersActivator.SpawnPlayers(levelParameters.GetPlayers, levelParameters.GetPlayersQuantity);
            }

            private void StartGame()
            {
                gameMoves.StartMove(playersActivator.GetPlayers);
            }

            private void OnDisable()
            {
                GlobalEventManager.OnPlayerActivated -= StartGame;

                GlobalEventManager.OnStartGame -= BuildPath;

                GlobalEventManager.OnPathBuilt -= SpawnPlayer;

                GlobalUIEventManager.OnButtonBackClick -= OpenMenu;
            }
        }
    }
}