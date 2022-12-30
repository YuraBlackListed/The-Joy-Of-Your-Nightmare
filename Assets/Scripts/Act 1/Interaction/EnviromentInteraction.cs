using UnityEngine;

namespace Act1
{
    public class EnviromentInteraction : MonoBehaviour
    {
        public KeyCode InteractButton;

        [SerializeField] private Transform MainCamera;

        [SerializeField] private LayerMask InteractableLayer;

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
                Use(interactRay.collider.gameObject);

                crosshair.Pointed();
            }
            else
            {
                crosshair.Unpointed();
            }
        }
        private void Use(GameObject target)
        {
            if(Input.GetKeyDown(InteractButton))
            {
                target.GetComponent<Interactable>().Interact();
            }
        }
        //This method \/ is currently inavailable, please dont use it
        /*private void Interact(GameObject interactable)
        {
            switch (interactable.tag)
            {
                //This done
                case "Window":
                    //interactable.GetComponent<WindowScript>().IsUsed = Input.GetKey(InteractButton);
                    break;
                //There must be 2 diff objects with colliders. One of them with tag "Window" and second one with "WindowExit" which is child of "Window" tagged one
                //Pretty questionable
                case "WindowExit":
                    //interactable.GetComponentInParent<WindowScript>().IsUsed = false;
                    break;
                //This done
                case "Lamp":
                    if (Input.GetKeyDown(InteractButton))
                    {
                        if (interactable.GetComponent<LampScript>().active)
                        {
                            interactable.GetComponent<LampScript>().active = false;
                        }
                        else
                        {
                            interactable.GetComponent<LampScript>().active = true;
                        }
                    }
                    break;
                    //Done
                case "Drawer":
                    if (Input.GetKeyDown(InteractButton))
                    {
                        if (interactable.GetComponent<DrawerScript>().active)
                        {
                            interactable.GetComponent<DrawerScript>().active = false;
                        }
                        else
                        {
                            interactable.GetComponent<DrawerScript>().active = true;
                        }
                    }
                    break;
                    //Dnoe
                case "FlashLight":
                    if (Input.GetKeyDown(InteractButton))
                    {
                        interactable.GetComponent<FlashLightPickScript>().active = false;
                    }
                    break;

            }
        }*/
    }
}
