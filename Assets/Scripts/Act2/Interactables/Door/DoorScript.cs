using UnityEngine;

public class DoorScript : Lockable
{
    public bool IsOpened = false;
    public bool CanOpen = true;

    public Vector3 OpenRotation;
    public Vector3 ClosedRotation;

    public override void Interact()
    {
        if(!CanOpen)
        {
            return;
        }

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

        CanOpen = false;

        Invoke(nameof(ResetOpening), 1.5f);
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
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(OpenRotation), Time.deltaTime * 3.5f);
        }
        else
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(ClosedRotation), Time.deltaTime * 3.5f);
        }
    }
    private void ResetOpening()
    {
        CanOpen = true;
    }
}
