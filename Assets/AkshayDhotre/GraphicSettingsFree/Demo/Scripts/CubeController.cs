using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AkshayDhotre.GraphicSettingsMenu
{
    public class CubeController : MonoBehaviour
    {
        public float moveSpeed;

        private Camera cam;

        private MenuToggler menuToggler;
        private void Start()
        {
            cam = Camera.main;
            menuToggler = FindObjectOfType<MenuToggler>();
        }

        private void Update()
        {
            if (menuToggler != null)
            {
                if (menuToggler.menuCanvasComponent.enabled)
                {
                    return;

                }
            }

            Vector3 cameraForward = cam.transform.forward;
            cameraForward.y = 0;
            Vector3 cameraRight = cam.transform.right;
            cameraRight.y = 0;



            transform.position += cameraForward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            transform.position += cameraRight * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

            
        }
    }
}

