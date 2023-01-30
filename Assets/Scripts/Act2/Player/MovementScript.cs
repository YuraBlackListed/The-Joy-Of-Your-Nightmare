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
    [SerializeField] private Transform LastPosition;

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

    private float verticalInput = 0;
    private float horizontalInput = 0;
    private float speedMod = 1.5f;
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

        IsCrouching = Input.GetKey(CrouchKey);

        TryCrouch();

        if(Input.GetKey(SprintKey) && !IsCrouching)
        {
            SprintMod = 4f;
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
