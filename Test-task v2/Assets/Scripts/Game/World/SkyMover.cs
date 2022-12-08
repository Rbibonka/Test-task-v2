using UnityEngine;

public class SkyMover : MonoBehaviour
{
    [Header("Parameters")]
    [Range(0, 1)]
    [SerializeField]
    private float speed;

    [Header("Game objects")]
    [SerializeField]
    private Transform[] clouds;

    [SerializeField]
    private Transform startPosition;

    [SerializeField]
    private Transform finishPosition;

    private void Update()
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].Translate(Vector3.right * speed * Time.deltaTime);

            if (clouds[i].position.x >= finishPosition.position.x)
            {
                clouds[i].position = new Vector3(startPosition.position.x, clouds[i].position.y, clouds[i].position.z);
            }
        }
    }
}
