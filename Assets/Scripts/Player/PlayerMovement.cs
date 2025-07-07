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
    private int positionIndex;
    private float interpolateAmount;
    private bool isMoving;
    [SerializeField] private bool isGrounded;
    private Rigidbody rb;

    [SerializeField] private SwipeDetection swipeDetection;

    TouchControls inputActions;
    private Vector2 inputVector;

    private void OnEnable()
    {
        swipeDetection.OnSwipe += StartMovement;
    }

    private void OnDisable()
    {
        swipeDetection.OnSwipe -= StartMovement;
    }

    private void Awake()
    {
        inputActions = new TouchControls();
        inputActions.Keyboard.Enable();
        inputActions.Keyboard.Movement.performed += Move;
    }

    private void OnDestroy()
    {
        inputActions.Keyboard.Disable();
    }

    private void Start()
    {
        positionIndex = 1;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GroundCheck();
        AddFallingForce();
        GravityControl();
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

            yield return new WaitForEndOfFrame();
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
