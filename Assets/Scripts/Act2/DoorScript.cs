using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool IsOpened { get; private set; } = false;

    [SerializeField] private Vector3 OpenRotation;
    [SerializeField] private Vector3 ClosedRotation;


    private void Update()
    {
        if (IsOpened)
        {
            transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, OpenRotation, Time.deltaTime * 5f));
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, ClosedRotation, Time.deltaTime * 5f));
        }
    }
    public void Interact()
    {
        IsOpened = !IsOpened;
    }
}
