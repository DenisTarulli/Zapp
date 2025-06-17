using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion

    private TouchControls playerControls;

    private void Awake()
    {
        playerControls = new TouchControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Touch.PrimaryContact.started += StartTouchPrimary;
        playerControls.Touch.PrimaryContact.canceled += EndTouchPrimary;
    }

    private void OnDisable()
    {
        playerControls.Touch.PrimaryContact.started -= StartTouchPrimary;
        playerControls.Touch.PrimaryContact.canceled -= EndTouchPrimary;
        playerControls.Disable();
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        Vector2 position = playerControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        float time = (float)context.startTime;

        OnStartTouch?.Invoke(position, time);
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        Vector2 position = playerControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        float time = (float)context.time;

        OnEndTouch?.Invoke(position, time);
    }

    public Vector2 PrimaryPosition()
    {
        return playerControls.Touch.PrimaryPosition.ReadValue<Vector2>();
    }
}
