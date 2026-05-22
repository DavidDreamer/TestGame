using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [field: SerializeField]
    private Collider[] Colliders { get; set; }

    [field: SerializeField]
    private Rigidbody[] Rigidbodies { get; set; }

    private void OnValidate()
    {
        Colliders = GetComponentsInChildren<Collider>(true);
        Rigidbodies = GetComponentsInChildren<Rigidbody>(true);
    }

    public void Enable(bool value)
    {
        foreach (var collider in Colliders)
        {
            collider.enabled = value;
        }

        foreach (var body in Rigidbodies)
        {
            body.isKinematic = !value;
        }
    }
}
