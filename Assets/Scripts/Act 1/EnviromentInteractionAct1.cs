using UnityEngine;

public class EnviromentInteractionAct1 : MonoBehaviour
{
    public KeyCode InteractButton;

    [SerializeField] private Transform MainCamera;

    [SerializeField] private LayerMask InteractableLayer;

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
            //You guys can do your interactables here
        }
    }
}
