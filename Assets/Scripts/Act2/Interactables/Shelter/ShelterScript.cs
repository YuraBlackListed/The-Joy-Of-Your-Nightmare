using UnityEngine;

public class ShelterScript : Interactable
{
    public int SafetyLevel = 4;
    public bool PlayerIsInside { get; private set; }

    public Transform OutPosition;

    [SerializeField] private GameObject Player;

    [SerializeField] private Transform InPosition;

    private bool canInteract = true;

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
            Player.transform.position = InPosition.position;
            Player.transform.rotation = InPosition.rotation;
        }

        SafetyLevel = Mathf.Clamp(SafetyLevel, 0, 4);
    }
    private void GetIn()
    {
        canInteract = false;
        PlayerIsInside = true;

        PlayerState.Hide(gameObject);

        var movement = Player.GetComponent<MovementScript>();

        movement.enabled = false;

        Player.transform.position = InPosition.position;
        Player.transform.rotation = InPosition.rotation;

        Invoke(nameof(ResetInteraction), shelterCooldown);
    }
    private void GetOut()
    {
        canInteract = false;
        PlayerIsInside = false;

        PlayerState.SetNormal();

        var movement = Player.GetComponent<MovementScript>();

        movement.enabled = true;

        Player.transform.position = OutPosition.position;
        Player.transform.rotation = OutPosition.rotation;

        Invoke(nameof(ResetInteraction), shelterCooldown);
    }
    private void ResetInteraction()
    {
        canInteract = true;
    }
}
