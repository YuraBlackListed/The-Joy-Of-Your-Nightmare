using UnityEngine;

    public class EnviromentInteraction : MonoBehaviour
    {
        public KeyCode InteractButton;

        [SerializeField] private float Distance;

        [SerializeField] private Transform MainCamera;

        [SerializeField] private LayerMask InteractableLayer;

        [SerializeField] private Crosshair crosshair;

        private Interactable lastInteractable;

        private void Update()
        {
            UpdateInteraction();

            if (Input.GetKeyDown(InteractButton))
            {
                TryUse();
            }
            else if(Input.GetKeyUp(InteractButton))
            {
                lastInteractable?.StopInteract();
            }
        }
        private void UpdateInteraction()
        {
            RaycastHit interactRay;

            if (Physics.Raycast(MainCamera.position, MainCamera.forward, out interactRay, Distance, InteractableLayer))
            {
                GameObject hittedGameobject = interactRay.collider.gameObject;

                var useable = hittedGameobject.GetComponent<Interactable>();

                if(useable)
                {
                    lastInteractable = useable;

                    lastInteractable.StartHover();

                    crosshair.Pointed();
                }
            }
            else
            {
                crosshair.Unpointed();

                if(lastInteractable)
                {
                    lastInteractable.StopInteract();

                    lastInteractable = null;
                }
            }
        }
        private void TryUse()
        {
            lastInteractable?.Interact();
        }
    }