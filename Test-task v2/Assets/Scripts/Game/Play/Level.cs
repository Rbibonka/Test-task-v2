using UnityEngine;

namespace Game
{
    namespace Play
    {
        public class Level : MonoBehaviour
        {
            [SerializeField]
            private LevelParameters levelParameters;

            [SerializeField]
            private PathCreator pathCreator;

            [SerializeField]
            private PlayersActivator playersActivator;

            [SerializeField]
            private GameMoves gameMoves;

            private void Start()
            {
                GlobalEventManager.OnPlayerActivated += StartGame;

                GlobalEventManager.OnStartGame += BuildPath;

                GlobalEventManager.OnPathBuilt += SpawnPlayer;
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
                gameMoves.StartMove(playersActivator.GetPlayers, levelParameters.GetPlatfromPoints);
            }

            private void OnDisable()
            {
                GlobalEventManager.OnPlayerActivated -= StartGame;

                GlobalEventManager.OnStartGame -= BuildPath;

                GlobalEventManager.OnPathBuilt -= SpawnPlayer;
            }
        }
    }
}