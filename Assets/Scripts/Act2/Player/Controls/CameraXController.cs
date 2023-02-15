using UnityEngine;

public class CameraXController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [Range(0.1f, 1f)] public float smooth = 0.1f;
    public float sensitivity = 10f;
    private Vector2 currentRotation;

    void Update()
    {
        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        transform.rotation = Quaternion.Euler(0f, currentRotation.x, 0);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
