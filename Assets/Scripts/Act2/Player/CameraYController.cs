using UnityEngine;

public class CameraYController : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    [Range(0.1f, 1f)] public float smooth = 0.1f;
    public float sensitivity = 10f;
    public float maxYAngle = 80f;
    private Vector2 currentRotation;
    void Update()
    {
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
        transform.localRotation = Quaternion.Euler(currentRotation.y, 0f, 0);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
