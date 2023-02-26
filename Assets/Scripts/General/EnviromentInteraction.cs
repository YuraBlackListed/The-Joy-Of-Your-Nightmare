using UnityEngine;

    public class EnviromentInteraction : MonoBehaviour
    {
        public KeyCode InteractButton;

        [SerializeField] private float Distance;

        [SerializeField] private Transform MainCamera;

        [SerializeField] private LayerMask InteractableLayer;

        [SerializeField] private Crosshair crosshair;

        [SerializeField] private Animator uiHint;

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
            Debug.DrawRay(MainCamera.position, MainCamera.forward, Color.green, Distance);
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

                    uiHint.SetBool("Pointed", true);
                    crosshair.Pointed();
                }
            }
            else
            {
                uiHint.SetBool("Pointed", false);
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