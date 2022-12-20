using UnityEngine;

public class EnviromentInteractionAct1 : MonoBehaviour
{
    public KeyCode InteractButton;

    [SerializeField] private Transform MainCamera;

    [SerializeField] private LayerMask InteractableLayer;

    [SerializeField] private WindowScript Window;
    
    [SerializeField] private Crosshair crosshair;
    
    

    void Update()
    {
        TryInteract();
    }

    private void TryInteract()
    {
        RaycastHit interactRay;

        if (Physics.Raycast(MainCamera.position, MainCamera.forward, out interactRay, 1.5f, InteractableLayer))
        {
            Interact(interactRay.collider.gameObject);
            crosshair.Pointed();
        }
        else
        {
            crosshair.Unpointed();
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
                    if(interactable.GetComponent<LampScript>().active)
                    {
                        interactable.GetComponent<LampScript>().active = false;
                    }
                    else
                    {
                        interactable.GetComponent<LampScript>().active = true;
                    }
                }
                break;
            case "Drawer":
                if(Input.GetKeyDown(InteractButton))
                {
                    if(interactable.GetComponent<DrawerScript>().active)
                    {
                        interactable.GetComponent<DrawerScript>().active = false;
                    }
                    else
                    {
                        interactable.GetComponent<DrawerScript>().active = true;
                    }
                }
                break;
            case "FlashLight":
                if (Input.GetKeyDown(InteractButton))
                {
                    interactable.GetComponent<FlashLightPickScript>().active = false;
                }
                break;
            
        }
    }
}
