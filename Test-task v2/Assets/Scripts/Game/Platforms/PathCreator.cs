using System.Collections;
using UnityEngine;
using TMPro;

namespace Game
{
    namespace Play
    {
        public class PathCreator : MonoBehaviour, IBuilder
        {
            [Header("Game objects")]
            [SerializeField]
            private GameObject simplePlatform;

            [SerializeField]
            private GameObject[] specialPlatform;

            [SerializeField]
            private Transform parent;

            [Header("Path parameters")]
            [SerializeField]
            private int specialPlatformsQuantity;

            [Range(0, 1)]
            [SerializeField]
            private float timeSpawnPlatform;

            private Transform[] spawnPoitns;

            private int firstPossibleSpecialPlatfrom = 4;

            private int[] specialPlfatforms;

            private bool isMatches;

            private bool uniqueValue;

            public void StartBuildPlatforms(int specialPlatformsValue, Transform[] platforms)
            {
                specialPlatformsQuantity = specialPlatformsValue;

                spawnPoitns = platforms;

                if (specialPlatformsQuantity < spawnPoitns.Length - firstPossibleSpecialPlatfrom)
                {
                    NumberingSpecialPlatforms();

                    StartCoroutine(BuildPath());
                }
                else
                {
                    Debug.Log("Специальных платформ больше чем платформ всего!");
                }
            }

            private bool ComparsionPlatforms(int numberPlatform)
            {
                isMatches = false;

                foreach (int item in specialPlfatforms)
                {
                    if (numberPlatform == item)
                    {
                        isMatches = true;

                        break;
                    }
                }

                return isMatches;
            }

            private IEnumerator BuildPath()
            {
                for (int i = 1; i < spawnPoitns.Length; i++)
                {
                    if (ComparsionPlatforms(i))
                    {
                        int randomValue = Random.Range(0, specialPlatform.Length);

                        SpawnPlatform(specialPlatform[randomValue], i);
                    }
                    else if (!ComparsionPlatforms(i))
                    {
                        SpawnPlatform(simplePlatform, i);
                    }

                    yield return new WaitForSeconds(timeSpawnPlatform);
                }

                GlobalEventManager.OnPathBuilt?.Invoke();
            }

            private void SpawnPlatform(GameObject platform, int numberSpawnPointPlatform)
            {
                var platformClone = Instantiate(platform, spawnPoitns[numberSpawnPointPlatform].position, 
                    Quaternion.identity, parent.transform);

                var platformTextMesh = platformClone.GetComponentInChildren<TextMeshPro>();

                platformTextMesh.color = Random.ColorHSV();

                platformTextMesh.text = numberSpawnPointPlatform.ToString();
            }

            private void NumberingSpecialPlatforms()
            {
                int maxQuantitySpecialPlatforms = spawnPoitns.Length - 1;

                specialPlfatforms = new int[specialPlatformsQuantity];

                for (int i = 0; i < specialPlfatforms.Length; i++)
                {
                    while (true)
                    {
                        uniqueValue = true;

                        int randomValue = Random.Range(firstPossibleSpecialPlatfrom, maxQuantitySpecialPlatforms);

                        for (int j = 0; j < specialPlfatforms.Length; j++)
                        {
                            if (randomValue == specialPlfatforms[j])
                            {
                                uniqueValue = false;
                                break;
                            }
                        }

                        if (uniqueValue)
                        {
                            specialPlfatforms[i] = randomValue;
                            break;
                        }
                    }
                }
            }
        }
    }
}