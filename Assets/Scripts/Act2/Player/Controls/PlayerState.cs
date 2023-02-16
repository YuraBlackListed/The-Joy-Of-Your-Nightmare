using UnityEngine;

public enum PlayerStatus
{ 
    Normal,
    Hidden,
}
public enum PlayerMovementState
{ 
    Walking,
    Running,
    Crouching,
    Idle,
}

public class PlayerState : MonoBehaviour
{
    public static PlayerState instance;
    public PlayerStatus Status { get; private set; }
    public PlayerMovementState MovementStatus { get; private set; }
    public GameObject CurrentShelter { get; private set; }

    [SerializeField] private MovementScript Movement;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Status = PlayerStatus.Normal;
    }
    private void Update()
    {
        SetMovementState();
    }
    private void SetMovementState()
    {
        if(Movement.IsRunning)
        {
            MovementStatus = PlayerMovementState.Running;
            return;
        }
        if(Movement.IsCrouching)
        {
            MovementStatus = PlayerMovementState.Crouching;
            return;
        }
        if(Movement.IsMoving)
        {
            MovementStatus = PlayerMovementState.Walking;
            return;
        }

        MovementStatus = PlayerMovementState.Idle;
    }
    public static void Hide(GameObject shelter)
    {
        instance.CurrentShelter = shelter;

        instance.Status = PlayerStatus.Hidden;
    }
    public static void SetNormal()
    {
        instance.CurrentShelter = null;

        instance.Status = PlayerStatus.Normal;
    }
}
