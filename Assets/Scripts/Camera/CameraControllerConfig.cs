using UnityEngine;

[CreateAssetMenu(fileName = "CameraControllerConfig", menuName = "Scriptable Objects/CameraControllerConfig")]
public class CameraControllerConfig : ScriptableObject
{
    [field: SerializeField]
    public float DistanceToTarget { get; private set; }

    [field: SerializeField]
    public Vector3 Offset { get; private set; }

    [field: SerializeField]
    public float Angle { get; private set; }

    [field: SerializeField]
    public float RotationSpeed { get; private set; }
}
