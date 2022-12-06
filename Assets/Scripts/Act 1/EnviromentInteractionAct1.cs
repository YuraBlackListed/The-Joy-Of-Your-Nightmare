using UnityEngine;

public class EnviromentInteractionAct1 : MonoBehaviour
{
    public KeyCode InteractButton;

    [SerializeField] private Transform MainCamera;

    [SerializeField] private LayerMask InteractableLayer;

    [SerializeField] private WindowScript Window;

    void Update()
    {
        TryInteract();
    }

    private void TryInteract()
    {
        RaycastHit interactRay;

        if (Physics.Raycast(MainCamera.position, MainCamera.forward, out interactRay, 500f, InteractableLayer))
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
            //There must be 2 diff objects with colliders. One of them with tag "Window" and second one with "WindowExit" which is child of "Window" tagged one
            case "WindowExit":
                interactable.GetComponentInParent<WindowScript>().IsUsed = false;
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
    private void HandleSpecials()
    {
        Window.IsUsed = false;
    }
}
