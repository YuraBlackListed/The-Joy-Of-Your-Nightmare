using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AkshayDhotre.GraphicSettingsMenu
{
    public class CameraController : MonoBehaviour
    {
        public Transform player;

        private void Update()
        {
            transform.LookAt(player.transform.position);
        }
    }
}
