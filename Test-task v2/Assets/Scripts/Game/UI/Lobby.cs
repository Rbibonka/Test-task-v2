using UnityEngine;
using UnityEngine.UI;
using System;

namespace Game
{
    namespace Play
    {
        public class Lobby : MonoBehaviour
        {
            [Header("UI")]
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

            private void Start()
            {
                GlobalUIEventManager.OnButtonStartClick += CompleteSetup;

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
                        Debug.Log("������� �� ����� ���� 0!");
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
                                Debug.Log("����������� ������ ������ ��� ������ �����!");
                            }
                        }
                        else
                        {
                            Debug.Log("������� ��� ����������� �� ����� ���� ������ 0!");
                        }
                    }
                }
                catch(FormatException)
                {
                    Debug.Log("������� ����������� �������!");
                }
            }

            private void OnDisable()
            {
                GlobalUIEventManager.OnButtonStartClick -= CompleteSetup;
            }
        }
    }
}

