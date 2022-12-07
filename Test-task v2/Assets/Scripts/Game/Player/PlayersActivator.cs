using UnityEngine;

namespace Game
{
    namespace Play
    {
        public class PlayersActivator : MonoBehaviour
        {
            private GameObject[] activePlayers;

            public GameObject[] GetPlayers
            {
                get
                {
                    return activePlayers;
                }
            }

            public void SpawnPlayers(GameObject[] players, int quantityPlayers)
            {
                activePlayers = new GameObject[quantityPlayers];

                for (int i = 0; i < quantityPlayers; i++)
                {
                    players[i].SetActive(true);

                    players[i].GetComponentInChildren<PlayerParameters>().SettingParameters(i);

                    activePlayers[i] = players[i];
                }

                GlobalEventManager.OnPlayerActivated?.Invoke();
            }
        }
    }
}