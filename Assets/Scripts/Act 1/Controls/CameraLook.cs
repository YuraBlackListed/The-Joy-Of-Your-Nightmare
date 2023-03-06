using UnityEngine;

public class CameraLook : MonoBehaviour
{
   [SerializeField] private ControllsValue scriptableObj;
   [SerializeField] private GameObject playerCamera;
   [Range(0.1f, 1f)] public float smooth = 0.1f;
   public float sensitivity = 10f;
   public float maxYAngle = 80f;
   private Vector2 currentRotation;

   void Start()
   {
      sensitivity = scriptableObj.sensitivity;
   }
   void Update()
   {
      currentRotation.x += Input.GetAxis("Mouse X") * sensitivity / 10;
      currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity / 10;
      currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
      currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
      transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x,0);
      playerCamera.transform.rotation = Quaternion.Lerp(playerCamera.transform.rotation, transform.rotation, 1f * smooth);  
   }
}
