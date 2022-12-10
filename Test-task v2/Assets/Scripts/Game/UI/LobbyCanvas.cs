using UnityEngine;
using UnityEngine.UI;
using System;

namespace Game
{
    namespace Play
    {
        public class LobbyCanvas : MonoBehaviour
        {
            [Header("UI elements")]
            [SerializeField]
            private InputField quantityPlayersInptFd;

            [SerializeField]
            private InputField quantitySpecialPlatformsInptFd;

            [SerializeField]
            private Animator panelAnimator;

            [Header("Settings script")]
            [SerializeField]
            private LevelParameters levelParameters;

            private int firstPossibleSpecialPlatfrom = 4;

            private int quantityPlatforms;
            private int quantityPlayers;
            private int quantitySpecialPlatforms;

            private void OnEnable()
            {
                GlobalUIEventManager.OnButtonStartClick += CompleteSetup;
            }

            private void Start()
            {
                quantityPlatforms = levelParameters.GetPlatformQuantity;
            }

            private void CompleteSetup()
            {
                try
                {
                    quantityPlayers = int.Parse(quantityPlayersInptFd.text);

                    quantitySpecialPlatforms = int.Parse(quantitySpecialPlatformsInptFd.text);

                    if (quantityPlayersInptFd.text[0] == '0')
                    {
                        Debug.Log("Игроков не может быть 0!");
                    }
                    else
                    {
                        if (quantityPlayers > 0 && quantitySpecialPlatforms >= 0)
                        {
                            if (quantitySpecialPlatforms < quantityPlatforms - firstPossibleSpecialPlatfrom)
                            {
                                panelAnimator.SetTrigger("Close");

                                levelParameters.ChangeQuantityPalyers(quantityPlayers);

                                levelParameters.ChangeQuantitySpecialPlatforms(quantitySpecialPlatforms);

                                GlobalEventManager.OnStartGame?.Invoke();
                            }
                            else
                            {
                                Debug.Log("Специальных камней больше чем камней всего!");
                            }
                        }
                        else
                        {
                            Debug.Log("Игроков или препятствий не может быть меньше 0!");
                        }
                    }
                }
                catch(FormatException)
                {
                    Debug.Log("Введены неизвестные символы!");
                }
            }

            private void OnDisable()
            {
                GlobalUIEventManager.OnButtonStartClick -= CompleteSetup;
            }
        }
    }
}

