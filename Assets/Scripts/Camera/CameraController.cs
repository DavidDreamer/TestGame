using UnityEngine;

public class CameraController : MonoBehaviour
{
    [field: SerializeField]
    private CameraControllerConfig Config { get; set; }

    [field: SerializeField]
    public GameObject Target { get; set; }

    private void LateUpdate()
    {
        Quaternion targetRotation = Quaternion.Euler(Config.Angle, Target.transform.eulerAngles.y, 0);
        float rotationDelta = Config.RotationSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationDelta);

        Vector3 direction = (rotation * Vector3.back).normalized;
        Vector3 targetPosition = Target.transform.position + Config.Offset;
        Vector3 position = targetPosition + direction * Config.DistanceToTarget;

        transform.SetPositionAndRotation(position, rotation);
    }
}
