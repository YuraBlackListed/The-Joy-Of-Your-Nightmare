using UnityEngine;

public class DoorScript : Interactable
{
    public bool IsOpened { get; private set; } = false;

    [SerializeField] private Vector3 OpenRotation;
    [SerializeField] private Vector3 ClosedRotation;

    public override void Interact()
    {
        if (IsOpened) 
         {
            Close();
         }
         else
         {
            Open();
         }
    }
    public void Open()
    {
        IsOpened = true;
    }
    public void Close()
    {
        IsOpened = false;
    }
    private void Update()
    {
        if (IsOpened)
        {
            transform.localRotation = Quaternion.Euler(OpenRotation);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(ClosedRotation);
        }
    }
}
