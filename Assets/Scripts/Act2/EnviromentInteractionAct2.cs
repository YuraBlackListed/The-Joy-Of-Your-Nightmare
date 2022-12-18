using UnityEngine;

public class EnviromentInteractionAct2 : MonoBehaviour
{
    public KeyCode InteractButton;

    [SerializeField] private Transform MainCamera;

    [SerializeField] private LayerMask InteractableLayer;

    void Update()
    {
        if(Input.GetKeyDown(InteractButton))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        RaycastHit interactRay;

        if (Physics.Raycast(MainCamera.position, MainCamera.forward, out interactRay, 100f, InteractableLayer))
        {
            Interact(interactRay.collider.gameObject);
        }
    }
    private void Interact(GameObject interactable)
    {
        switch (interactable.tag)
        {
            case "Shelter":
                interactable.GetComponent<ShelterScript>().Interact();
                break;
            case "Door":
                interactable.GetComponent<DoorScript>().TryInteract();
                break;
            //You guys can do your interactables here
        }
    }
}
