using UnityEngine;

public class MovementScript : MonoBehaviour
{
    //Adjust all of values like you need

    public bool IsMoving = false;

    [Header ("Controls")]
    public KeyCode ForwardButton;
    public KeyCode BackwardButton;
    public KeyCode LeftButton;
    public KeyCode RightButton;
    public KeyCode CrouchKey;
    public KeyCode SprintKey;

    [SerializeField] private Transform MainCamera;
    [Tooltip("This is empty gameobject, player is not parent, just object on scene")]
    [SerializeField] private Transform LastPosition;

    [SerializeField] private Animator CrouchAnimator;
    [SerializeField] private Animator CharacterAnimator;

    [Header("Crouch values")]
    [SerializeField] private float CrouchSpeedDecreasement;
    public bool IsCrouching;

    [Header ("Speed values")]
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float SprintMod;
    public bool IsRunning = false;

    [Header("Ground values")]
    [SerializeField] private float PlayerHeight;
    [SerializeField] private float GroundDrag; 
    [SerializeField] private LayerMask GroundLayer;

    private float verticalInput = 0;
    private float horizontalInput = 0;
    private float speedMod = 2f;
    private float normalSpeed;
    private float normalMaxSpeed;
    private float crouchSpeed;

    //Recommended mass = 17.5, recommended Drag = 3
    private Rigidbody rb;

    private Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        normalSpeed = MoveSpeed;
        normalMaxSpeed = MaxSpeed;

        InvokeRepeating(nameof(ResetLastPosition), 2f, 2f);
    }
    private void Update()
    {
        GetInput();

        MovePlayer();

        SpeedControl();
    }
    private void ResetLastPosition()
    {
        if(Vector3.Distance(transform.position, LastPosition.position) > 5.5f)
        {
            LastPosition.position = transform.position;
        }
    }
    private void GetInput()
    {
        TryDecreaseValues();

        IsCrouching = Input.GetKey(CrouchKey) && !IsRunning;

        TryCrouch();

        if(Input.GetKey(SprintKey) && !IsCrouching && IsMoving)
        {
            IsRunning = true;

            SprintMod = 1.3f;
        }
        else
        {
            IsRunning = false;

            SprintMod = 1f;
        }

        TryChangeSpeedValues();

        ClampValues();

        if (Input.GetKey(ForwardButton))
        {
            verticalInput += Time.deltaTime * speedMod;
        }
        else if(Input.GetKey(BackwardButton))
        {
            verticalInput -= Time.deltaTime * speedMod;
        }

        if(Input.GetKey(LeftButton))
        {
            horizontalInput -= Time.deltaTime * speedMod;
        }
        else if (Input.GetKey(RightButton))
        {
            horizontalInput += Time.deltaTime * speedMod;
        }

        ClampValues();
    }
    private void TryDecreaseValues()
    {
        if(!Input.GetKey(ForwardButton) || !Input.GetKey(BackwardButton))
        {
            verticalInput = 0f;
        }
        if (!Input.GetKey(RightButton) || !Input.GetKey(LeftButton))
        {
            horizontalInput = 0f;
        }
    }
    private void TryCrouch()
    {
        if(IsCrouching)
        {
            CrouchAnimator.SetBool("Crouching", true);

            crouchSpeed = CrouchSpeedDecreasement;
        }
        else if (!IsCrouching)
        {
            CrouchAnimator.SetBool("Crouching", false);

            crouchSpeed = 1f;
        }
    }
    private void TryChangeSpeedValues()
    {
        MoveSpeed = (normalSpeed * SprintMod) / crouchSpeed;
        MaxSpeed = (normalMaxSpeed * SprintMod) / crouchSpeed;
    }
    private void ClampValues()
    {
        verticalInput = Mathf.Clamp(verticalInput, -1f, 1f);
        horizontalInput = Mathf.Clamp(horizontalInput, -1f, 1f);
    }
    private void MovePlayer()
    {
        if(Mathf.Approximately(verticalInput, 0f) && Mathf.Approximately(horizontalInput, 0f))
        {
            IsMoving = false;
        }
        else
        {
            IsMoving = true;
        }

        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * MoveSpeed, ForceMode.Impulse);
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > MaxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * MaxSpeed;

            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
