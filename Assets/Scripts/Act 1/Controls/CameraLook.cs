using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    [Range(0.1f, 1f)] public float smooth = 0.1f;
    public float sensitivity = 10f;
     public float maxYAngle = 80f;
     private Vector2 currentRotation;
     void Update()
     {
        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
        transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x,0);
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera.transform.rotation = Quaternion.Lerp(playerCamera.transform.rotation, transform.rotation, 1f * smooth);
        
     }

}
