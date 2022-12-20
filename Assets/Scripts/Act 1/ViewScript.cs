using UnityEngine;

public class ViewScript : MonoBehaviour
{
    [SerializeField] private Transform MainCamera;

    [SerializeField] private LayerMask GhostLayer;

    private void Update()
    {
        RaycastHit view;

        if (Physics.Raycast(MainCamera.position, MainCamera.forward, out view, 10000f, GhostLayer))
        {
            CheckView(view.collider.gameObject);
        }
    }
    private void CheckView(GameObject lookingAtObject)
    {
        switch (lookingAtObject.tag)
        {
            case "Ghost":
                lookingAtObject.GetComponent<GhostAI>().PlayerLookingAtMe = true;
                break;
                //WARNING!! Make some colliders AROUND collider with tag "Ghost", if not it always will send you back in time (bug)
            case "GhostExit":
                lookingAtObject.GetComponentInParent<GhostAI>().PlayerLookingAtMe = false;
                break;
        }
    }
}
