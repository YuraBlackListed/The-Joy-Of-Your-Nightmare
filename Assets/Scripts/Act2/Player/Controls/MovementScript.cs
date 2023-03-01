using UnityEngine;

public class MovementScript : MonoBehaviour
{
    //Adjust all of values like you need

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
    [SerializeField] private float SprintModifier;
    public bool IsRunning = false;

    [Header("Ground values")]
    [SerializeField] private float PlayerHeight;
    [SerializeField] private float GroundDrag; 
    [SerializeField] private LayerMask GroundLayer;

    [Space]
    [SerializeField] private PlayerSteps Steps;

    private float sprintMod = 1f;
    private float verticalInput = 0;
    private float horizontalInput = 0;
    private float speedMod = 2f;
    private float normalSpeed;
    private float normalMaxSpeed;
    private float crouchSpeed;

    //Recommended mass = 17.5, recommended Drag = 3
    private Rigidbody rb;

    private Vector3 moveDirection;

    public bool IsMoving()
    {
        return (Input.GetKey(ForwardButton) | Input.GetKey(BackwardButton) | Input.GetKey(LeftButton) | Input.GetKey(RightButton)) && PlayerState.instance.Status is not PlayerStatus.Hidden;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        normalSpeed = MoveSpeed;
        normalMaxSpeed = MaxSpeed;

        InvokeRepeating(nameof(ResetLastPosition), 2f, 2f);
    }
    private void FixedUpdate()
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

        if(Input.GetKey(SprintKey) && !IsCrouching)
        {
            IsRunning = true;

            sprintMod = SprintModifier;
        }
        else
        {
            IsRunning = false;

            sprintMod = 1f;
        }

        TryChangeSpeedValues();

        ClampValues();

        if (Input.GetKey(ForwardButton))
        {
            verticalInput += Time.deltaTime * speedMod;
            Steps.MakeStepSound();
        }
        else if(Input.GetKey(BackwardButton))
        {
            verticalInput -= Time.deltaTime * speedMod;
            Steps.MakeStepSound();
        }

        if (Input.GetKey(LeftButton))
        {
            horizontalInput -= Time.deltaTime * speedMod;
            Steps.MakeStepSound();
        }
        else if (Input.GetKey(RightButton))
        {
            horizontalInput += Time.deltaTime * speedMod;
            Steps.MakeStepSound();
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
        MoveSpeed = (normalSpeed * sprintMod) / crouchSpeed;
        MaxSpeed = (normalMaxSpeed * sprintMod) / crouchSpeed;
    }
    private void ClampValues()
    {
        verticalInput = Mathf.Clamp(verticalInput, -1f, 1f);
        horizontalInput = Mathf.Clamp(horizontalInput, -1f, 1f);
    }
    private void MovePlayer()
    {
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
