using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputController : MonoBehaviour
{
    [field: SerializeField]
    private InputAction MoveAction { get; set; }

    [field: SerializeField]
    private InputAction RotationAction { get; set; }

    [field: SerializeField]
    private InputAction Ability1Action { get; set; }

    [field: SerializeField]
    private InputAction Ability2Action { get; set; }

    private Character Character { get; set; }

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

    public void Bind(Character character)
    {
        Character = character;
    }

    public void Tick()
    {
        EvaluateRotation();
        EvaluatePosition();

        if (Ability1Action.WasPressedThisFrame())
        {
            Character.Abilities[0].Activate(Character);
        }

        if (Ability2Action.WasPressedThisFrame())
        {
            Character.Abilities[1].Activate(Character);
        }

        void EvaluateRotation()
        {
            var input = RotationAction.ReadValue<Vector2>();
            float rotationDelta = input.x * Character.Config.RotationSpeed;
            float rotationY = Character.transform.eulerAngles.y + rotationDelta;
            Character.transform.eulerAngles = new Vector3(Character.transform.eulerAngles.x, rotationY, Character.transform.eulerAngles.z);
        }

        void EvaluatePosition()
        {
            var input = MoveAction.ReadValue<Vector2>();
            var move = new Vector3(input.x, 0, input.y);

            Vector3 directionWorldSpace = Character.transform.TransformDirection(move);
            Vector3 positionDelta = directionWorldSpace * Character.Config.MovementSpeed * Time.deltaTime;

            Character.CharacterController.Move(positionDelta);
        }
    }
}
