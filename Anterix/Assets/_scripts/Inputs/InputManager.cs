using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputSystem_Actions inputActions;
    // [SerializeField] private string actionMapName = "GameInputs"; // or "GameInputs" if it's under that map
    // [SerializeField] private string actionName = "Click";

    [SerializeField] private LayerMask interactableLayer;

    private InputAction clickAction;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;

        inputActions = new InputSystem_Actions();

        inputActions.GameInputs.Click.performed += OnClick;
        
        // Find the click action inside the input action asset
        var actionMap = inputActions.GameInputs;
        clickAction = actionMap.Click;
    }

    private void OnEnable()
    {
        inputActions.GameInputs.Enable();
    }

    private void OnDisable()
    {
        inputActions.GameInputs.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector2 worldPos = mainCamera.ScreenToWorldPoint(screenPos);

        Collider2D hit = Physics2D.OverlapPoint(worldPos, interactableLayer);

        if (hit != null)
        {
            TriggerAction _trigger = hit.GetComponent<TriggerAction>();

            if (_trigger)
            {
                _trigger.PreformAction();
            }
        }
        else
        {
            Debug.Log("Clicked on nothing interactable.");
        }
    }
}

