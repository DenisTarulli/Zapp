using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float interpolationSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField, Range((float)-79.81, (float)-9.81)] private float gravityScale;
    [SerializeField] private float fallingForce;
    [SerializeField] private float fallingForceMultiplier;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private BoxCollider groundCollider;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float downForce;
    [SerializeField] private GameObject trail;
    private int positionIndex;
    private float interpolateAmount;
    private bool isMoving;
    [SerializeField] private bool isGrounded;
    private Rigidbody rb;

    private TouchControls inputActions;
    private Vector2 inputVector;
    private PowerUp powerUp;

    private void Awake()
    {
        inputActions = new TouchControls();
        inputActions.Keyboard.Enable();
        inputActions.Keyboard.Movement.performed += Move;
        inputActions.Keyboard.PowerUp.performed += TriggerPowerUp;
    }

    private void OnDestroy()
    {
        inputActions.Keyboard.Disable();
    }

    private void Start()
    {
        powerUp = GetComponent<PowerUp>();
        trail = transform.GetChild(1).gameObject;
        positionIndex = 1;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GroundCheck();
        AddFallingForce();
        GravityControl();
        PositionFixer();
        HandleParticles();
    }

    private void TriggerPowerUp(InputAction.CallbackContext context)
    {
        powerUp.ActivatePowerUp();
    }

    private void HandleParticles()
    {
        if (isGrounded)
            trail.SetActive(true);
        else
            trail.SetActive(false);
    }

    private void PositionFixer()
    {
        if (!isMoving && isGrounded)
            transform.position = new(positionIndex - 1f, 0.5f, 0f);
    }

    private void Move(InputAction.CallbackContext context)
    {
        inputVector = inputActions.Keyboard.Movement.ReadValue<Vector2>();

        if (inputVector.x < 0f)
            StartMovement(-1f);
        else if (inputVector.x > 0f)
            StartMovement(1f);

        if (inputVector.y > 0f)
            Jump();
        else if (inputVector.y < 0f)
            DownForce();
    }

    public void SwipeMoveTrigger(Vector2 delta)
    {
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x < 0)
                StartMovement(-1f);
            else
                StartMovement(1f);
        }
        else
        {
            if (delta.y < 0)
                DownForce();
            else
                Jump();
        }
    }

    private bool GroundCheck()
    {
        float extraHeight = 0.1f;
        isGrounded = Physics.Raycast(boxCollider.bounds.center, Vector3.down, boxCollider.bounds.extents.y + extraHeight, groundMask);
        return isGrounded;
    }

    private void StartMovement(float dir)
    {
        float newIndex = positionIndex + dir;

        if (!isMoving && newIndex <= 2 && newIndex >= 0)
        {
            StartCoroutine(MoveTo(dir));
        }

        if (dir > 1)
        {
            Jump();
        }

        if (dir < -1)
        {
            DownForce();
        }
    }

    private IEnumerator MoveTo(float direction)
    {
        isMoving = true;

        Vector3 currentPosition = transform.position;
        Vector3 newPosition = new(transform.position.x + direction, transform.position.y, transform.position.z);

        while (interpolateAmount < 1f)
        {
            interpolateAmount += Time.deltaTime * interpolationSpeed;
            transform.position = Vector3.Lerp(currentPosition, newPosition, interpolateAmount);

            yield return null;
        }

        positionIndex += (int)direction;
        transform.position = new Vector3(positionIndex - 1, transform.position.y, transform.position.z); 
        interpolateAmount = 0f;
        isMoving = false;
    }

    private void Jump()
    {
        if (isGrounded == false || rb.velocity.y > 0f) return;

        float jumpForce = Mathf.Sqrt(jumpHeight * Physics.gravity.y * -2f) * rb.mass;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void DownForce()
    {
        if (isGrounded) return;

        rb.AddForce(downForce * Vector3.down, ForceMode.Impulse);
    }

    private void AddFallingForce()
    {
        if (rb.velocity.y < 0f)
            rb.AddForce(fallingForce * fallingForceMultiplier * Time.deltaTime * Vector3.down, ForceMode.Force);        
    }

    private void GravityControl()
    {
        Physics.gravity = new(0f, gravityScale, 0f);
    }
}
