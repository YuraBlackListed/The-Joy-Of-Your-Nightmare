using UnityEngine;

public class DoorScript : Interactable
{
    public bool IsOpened = false;

    public Vector3 OpenRotation;
    public Vector3 ClosedRotation;

    public bool IsLocked = false;

    public string KeyName;

    public override void Interact()
    {
        if(IsLocked)
        {
            return;
        }

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
        if(Inventory.ContainsItem(KeyName, ItemType.Key))
        {
            IsLocked = false;
        }

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
