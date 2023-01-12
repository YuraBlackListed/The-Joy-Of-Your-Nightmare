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

    [SerializeField] private Animator CrouchAnimator;
    //[SerializeField] private Animator CharacterAnimator //for future

    [Header("Crouch values")]
    [SerializeField] private float CrouchSpeedDecreasement;
    [SerializeField] private bool IsCrouching;

    [Header ("Speed values")]
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float SprintMod;

    [Header("Ground values")]
    [SerializeField] private float PlayerHeight;
    [SerializeField] private float GroundDrag; 
    [SerializeField] private LayerMask GroundLayer;

    private bool isGrounded;

    private float verticalInput = 0;
    private float horizontalInput = 0;
    private float speedMod = 1.5f;
    private float rbDrag;
    private float normalSpeed;
    private float normalMaxSpeed;

    //Recommended mass = 17.5, recommended Drag = 3
    private Rigidbody rb;

    private Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rbDrag = rb.drag;

        normalSpeed = MoveSpeed;
        normalMaxSpeed = MaxSpeed;
    }
    private void Update()
    {
        CheckGround();

        GetInput();

        MovePlayer();

        SpeedControl();
    }
    private void GetInput()
    {
        TryDecreaseValues();

        IsCrouching = Input.GetKey(CrouchKey);

        TryCrouch();

        if(Input.GetKey(SprintKey) && !IsCrouching)
        {
            SprintMod = 2.5f;
        }
        else
        {
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
    private void TryCrouch()
    {
        if(IsCrouching)
        {
            CrouchAnimator.SetBool("Crouching", true);

            CrouchSpeedDecreasement = 2.5f;
        }
        else if (!IsCrouching)
        {
            CrouchAnimator.SetBool("Crouching", false);

            CrouchSpeedDecreasement = 1f;
        }
    }
    private void TryChangeSpeedValues()
    {
        MoveSpeed = (normalSpeed * SprintMod) / CrouchSpeedDecreasement;
        MaxSpeed = (normalMaxSpeed * SprintMod) / CrouchSpeedDecreasement;
    }
    private void ClampValues()
    {
        verticalInput = Mathf.Clamp(verticalInput, -1f, 1f);
        horizontalInput = Mathf.Clamp(horizontalInput, -1f, 1f);
    }
    private void TryDecreaseValues()
    {
        if(!Input.GetKey(ForwardButton) || !Input.GetKey(BackwardButton))
        {
            verticalInput = Mathf.Lerp(verticalInput, 0f, Time.deltaTime * speedMod * 1000000f);
        }

        if(!Input.GetKey(RightButton) || !Input.GetKey(LeftButton))
        {
            horizontalInput = Mathf.Lerp(horizontalInput, 0f, Time.deltaTime * speedMod * 1000000f);
        }
    }
    private void MovePlayer()
    {
        moveDirection = MainCamera.forward * verticalInput + MainCamera.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * MoveSpeed, ForceMode.Impulse);
    }
    private void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.1f, GroundLayer);

        if(isGrounded)
        {
            rb.drag = Mathf.Lerp(rb.drag, rbDrag, Time.deltaTime * 5f);
        }
        else
        {
            rb.drag = Mathf.Lerp(rb.drag, 0f, Time.deltaTime * 5f);
        }
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
