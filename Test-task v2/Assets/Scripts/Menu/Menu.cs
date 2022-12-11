using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    namespace Menu
    {
        public class Menu : MonoBehaviour
        {
            private void OnEnable()
            {
                MenuEventManager.StartLevelClick += StartLevel;

                MenuEventManager.ExitGameClick += ExitGame;
            }

            private void ExitGame()
            {
                Application.Quit();
            }

            private void StartLevel()
            {
                SceneManager.LoadScene(1, LoadSceneMode.Single);
            }

            private void OnDisable()
            {
                MenuEventManager.StartLevelClick -= StartLevel;
                
                MenuEventManager.ExitGameClick -= ExitGame;
            }
        }
    } 
}