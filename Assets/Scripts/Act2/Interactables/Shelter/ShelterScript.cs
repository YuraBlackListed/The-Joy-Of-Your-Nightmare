using UnityEngine;

public class ShelterScript : Interactable
{
    public int SafetyLevel = 4;
    public bool PlayerIsInside { get; private set; }

    public Transform OutPosition;

    [SerializeField] private GameObject Player;

    [SerializeField] private Transform InPosition;

    private bool canInteract = true;
    private bool hasExited = true;

    private float shelterCooldown = 1.5f; // Set it like get in/out animation lenght

    public void TryGetPlayerOut()
    {
        if(PlayerIsInside)
        {
            //Play animation of closet opening

            GetOut();
        }
        else
        {
            SafetyLevel++;

            //Play animation of closet opening
        }
    }
    public override void Interact()
    {
        if (PlayerIsInside && canInteract)
        {
            GetOut();
        }
        else if (!PlayerIsInside && canInteract)
        {
            GetIn();
        }
    }
    private void Update()
    {
        if(PlayerIsInside)
        {
            Player.transform.position = Vector3.Lerp(Player.transform.position, InPosition.position, Time.deltaTime * 5f);
            Player.transform.rotation = Quaternion.Slerp(Player.transform.rotation, InPosition.rotation, Time.deltaTime * 3.5f);
        }
        else if(!PlayerIsInside && !hasExited)
        {
            Player.transform.position = Vector3.Lerp(Player.transform.position, OutPosition.position, Time.deltaTime * 5f);
            Player.transform.rotation = Quaternion.Slerp(Player.transform.rotation, OutPosition.rotation, Time.deltaTime * 3.5f);
        }

        SafetyLevel = Mathf.Clamp(SafetyLevel, 0, 4);
    }
    private void GetIn()
    {
        Player.GetComponent<CapsuleCollider>().enabled = false;
        Player.GetComponent<Rigidbody>().useGravity = false;

        canInteract = false;
        PlayerIsInside = true;

        PlayerState.Hide(gameObject);

        var movement = Player.GetComponent<MovementScript>();

        movement.enabled = false;

        hasExited = false;

        Invoke(nameof(ResetInteraction), shelterCooldown);
    }
    private void GetOut()
    {
        canInteract = false;
        PlayerIsInside = false;

        PlayerState.SetNormal();

        var movement = Player.GetComponent<MovementScript>();

        movement.enabled = true;

        Invoke(nameof(ResetExit), 1f);

        Invoke(nameof(ResetInteraction), shelterCooldown);
    }
    private void ResetExit()
    {
        Player.GetComponent<CapsuleCollider>().enabled = true;
        Player.GetComponent<Rigidbody>().useGravity = true;

        hasExited = true;
    }
    private void ResetInteraction()
    {
        canInteract = true;
    }
}
