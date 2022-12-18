using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool IsOpened { get; private set; } = false;

    [SerializeField] private Vector3 OpenRotation;
    [SerializeField] private Vector3 ClosedRotation;

    [SerializeField] private Animator DoorAnimator;

    public void TryInteract()
    {
        if(IsOpened)
        {
            Interact("Close");
        }
        else
        {
            Interact("Open");
        }
    }
    private void Interact(string triggerName)
    {
        DoorAnimator.SetTrigger(triggerName);

        IsOpened = !IsOpened;
    }
}
