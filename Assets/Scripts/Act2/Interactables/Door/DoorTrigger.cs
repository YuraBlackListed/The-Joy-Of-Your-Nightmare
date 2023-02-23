using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private DoorScript MyDoorScript;

    private void OnTriggerEnter(Collider incomingObject)
    {    
        if(incomingObject.gameObject.tag == "Runner")
        {
            CheckIfOpened();
        }
    }
    private void CheckIfOpened()
    {
        if(MyDoorScript.IsOpened)
        {
            return;
        }

        MyDoorScript.Interact();
    }
}
