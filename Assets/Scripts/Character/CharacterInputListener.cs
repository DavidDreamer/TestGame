using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputListener : MonoBehaviour
{
    [field: SerializeField]
    private Character Character { get; set; }

    [field: SerializeField]
    private CharacterController CharacterController { get; set; }

    [field: SerializeField]
    private CharacterConfig Config { get; set; }

    [field: SerializeField]
    private InputAction MoveAction { get; set; }

    [field: SerializeField]
    private InputAction RotationAction { get; set; }

    [field: SerializeField]
    private InputAction Ability1Action { get; set; }

    [field: SerializeField]
    private InputAction Ability2Action { get; set; }

    private void OnEnable()
    {
        MoveAction.Enable();
        RotationAction.Enable();
        Ability1Action.Enable();
        Ability2Action.Enable();
    }

    private void OnDisable()
    {
        MoveAction.Disable();
        RotationAction.Disable();
        Ability1Action.Disable();
        Ability2Action.Disable();
    }

    void Update()
    {
        EvaluateRotation();
        EvaluatePosition();

        if (Ability1Action.WasPressedThisFrame())
        {
            Character.Abilities[0].Activate();
        }

        if (Ability2Action.WasPressedThisFrame())
        {
            Character.Abilities[1].Activate();
        }

        void EvaluateRotation()
        {
            var input = RotationAction.ReadValue<Vector2>();
            float rotationDelta = input.x * Config.RotationSpeed;
            float rotationY = transform.eulerAngles.y + rotationDelta;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotationY, transform.eulerAngles.z);
        }

        void EvaluatePosition()
        {
            var input = MoveAction.ReadValue<Vector2>();
            var move = new Vector3(input.x, 0, input.y);

            Vector3 directionWorldSpace = transform.TransformDirection(move);
            Vector3 positionDelta = directionWorldSpace * Config.MovementSpeed * Time.deltaTime;

            CharacterController.Move(positionDelta);
        }
    }
}
