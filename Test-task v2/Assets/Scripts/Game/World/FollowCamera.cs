using UnityEngine;

namespace Game
{
    namespace Play
    {
        public class FollowCamera : MonoBehaviour
        {
            [Header("Camera parameters")]
            [SerializeField]
            private Vector3 distanceFromTarget;

            [Range(0, 5)]
            [SerializeField]
            private float cameraSpeed;

            [Range(0, 5)]
            [SerializeField]
            private float cameraRotateSpeed;

            [Header("Game object")]
            [SerializeField]
            private Transform centerTerrainPoint;

            private Transform trackingTarget;
            
            public enum CameraState
            {
                idle,
                terrainTracking,
                playersTracking
            }

            private CameraState cameraState;

            private void OnEnable()
            {
                GlobalEventManager.OnChangedTrackingTarget += TrackingPlayers;

                GlobalEventManager.OnStartGame += StartTrackingTerrain;
            }

            private void StartTrackingTerrain()
            {
                cameraState = CameraState.terrainTracking;
            }

            private void TrackingPlayers(Transform target, Vector3 distance)
            {
                distanceFromTarget = distance;

                trackingTarget = target;

                cameraState = CameraState.playersTracking;
            }

            private void LateUpdate()
            {
                if (cameraState == CameraState.terrainTracking)
                {
                    transform.position = Vector3.MoveTowards(transform.position, centerTerrainPoint.position, 
                        cameraSpeed * Time.deltaTime);
                }
                else if (cameraState == CameraState.playersTracking)
                {
                    CameraTrackingPlayer();
                }
            }

            private void CameraTrackingPlayer()
            {
                var positionToGo = trackingTarget.position + distanceFromTarget;

                var smoothPosition = Vector3.Lerp(transform.position, positionToGo, cameraSpeed * Time.deltaTime);

                transform.position = smoothPosition;

                var lookAt = Quaternion.LookRotation(trackingTarget.position - transform.position);

                transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, cameraRotateSpeed * Time.deltaTime);
            }

            private void OnDisable()
            {
                GlobalEventManager.OnChangedTrackingTarget -= TrackingPlayers;

                GlobalEventManager.OnStartGame -= StartTrackingTerrain;
            }
        }
    }
}