using UnityEngine;

public class EnviromentInteraction : MonoBehaviour
{
    public KeyCode InteractButton;

    [SerializeField] private Transform MainCamera;

    void Update()
    {
        TryInteract();
    }

    private void TryInteract()
    {
        RaycastHit interactRay;

        if (Physics.Raycast(MainCamera.position, MainCamera.forward, out interactRay, 500f))
        {
            Interact(interactRay.collider.gameObject);
        }
    }
    private void Interact(GameObject interactable)
    {
        switch (interactable.tag)
        {
            case "Window":
                interactable.GetComponent<WindowScript>().IsUsed = Input.GetKey(InteractButton);
                break;
            case "Lamp":
                if(Input.GetKeyDown(InteractButton))
                {
                    if(interactable.GetComponent<LampScript>().Active)
                    {
                        interactable.GetComponent<LampScript>().Active = false;
                    }
                    else
                    {
                        interactable.GetComponent<LampScript>().Active = true;
                    }
                }
                break;
        }
    }
}
