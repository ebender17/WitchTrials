using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

/// <summary>
/// Made a scriptable object so it can be acessed from anywhere in project. 
/// </summary>
[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IMenusActions, GameInput.IDialogueActions
{
    //Auto-generated C# class with input actions
    private GameInput _gameInput;

    //Assign deletgate{} to events to initialize them with an empty delegate
    // so we can skip the null check when we use them 

    //Gameplay
    public event UnityAction<Vector2> moveEvent = delegate { };
    public event UnityAction jumpEvent = delegate { };
    public event UnityAction jumpCanceledEvent = delegate { };
    public event UnityAction attackEvent = delegate { };
    public event UnityAction attackCanceledEvent = delegate { };
    public event UnityAction interactEvent = delegate { };
    public event UnityAction dashEvent = delegate { };
    public event UnityAction dashCanceledEvent = delegate { };
    public event UnityAction<Vector2, bool> dashDirectionEvent = delegate { };
    public event UnityAction openMenuEvent = delegate { };

    //Menus 
    public event UnityAction menuConfirmEvent = delegate { };

    //Dialogue 
    public event UnityAction advanceDialogueEvent = delegate { };

    private void OnEnable()
    {
        if(_gameInput == null)
        {
            _gameInput = new GameInput();
            _gameInput.Gameplay.SetCallbacks(this);
            _gameInput.Menus.SetCallbacks(this);
            _gameInput.Dialogue.SetCallbacks(this);
        }

        EnableGameplayInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }
    public void OnAdvanceDialogue(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            advanceDialogueEvent();
    }

    public void OnConfirm(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            menuConfirmEvent();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            dashEvent.Invoke();
        if (context.phase == InputActionPhase.Canceled)
            dashCanceledEvent.Invoke();
    }

    public void OnDashDirection(InputAction.CallbackContext context)
    {
        dashDirectionEvent.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            interactEvent.Invoke();

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            jumpEvent.Invoke();
        if (context.canceled)
            jumpCanceledEvent.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnPrimaryAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            attackEvent.Invoke();
        if (context.phase == InputActionPhase.Canceled)
            attackCanceledEvent.Invoke();
    }

    public void OnOpenMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            openMenuEvent.Invoke(); 
    }

    private bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse"; 
    public void EnableGameplayInput()
    {
        _gameInput.Menus.Disable();
        _gameInput.Dialogue.Disable();

        _gameInput.Gameplay.Enable();
    }

    public void EnableMenuInput()
    {
        _gameInput.Gameplay.Disable();
        _gameInput.Dialogue.Disable();

        _gameInput.Menus.Enable();
    }
    public void EnableDialogueInput()
    {
        _gameInput.Menus.Disable();
        _gameInput.Gameplay.Disable();

        _gameInput.Dialogue.Enable();
    }

    public void DisableAllInput()
    {
        _gameInput.Menus.Disable();
        _gameInput.Dialogue.Disable();
        _gameInput.Gameplay.Disable();
    }

}
