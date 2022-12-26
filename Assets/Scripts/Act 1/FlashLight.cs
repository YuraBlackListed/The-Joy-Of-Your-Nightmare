using UnityEngine;
using System.Collections.Generic;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject torchModel;

    [SerializeField] private LayerMask BedMonsterLayer;
    
    public KeyCode F = KeyCode.F;
    
    [Range(0.01f, 0.1f)] public float speed = 0.1f;
    
    private bool active = false;
    public bool picked = false;
    private bool played = false;
    
    [SerializeField] private List<GameObject> positions;
    
    private void Start()
    {
        int randomId = Random.Range(0, positions.Count - 1);
        
        GameObject spawned = Instantiate(torchModel, positions[randomId].transform.position, Quaternion.identity);
        spawned.transform.Rotate (0f, 0f, -86.418f);
        spawned.transform.parent = positions[randomId].transform;
        FlashLightPickScript lighter = spawned.GetComponent<FlashLightPickScript>();
        lighter.flashlight = this;
    }
    private void Update()
    {
        if (picked)
        {
            if (!played)
            {
                PlayPickUpSound();
            }

            MoveFlashlight();

            UseFlashlight();

            if(active)
            {
                LightLogic();
            }
        }
    }
    private void LightLogic()
    {
        RaycastHit flashlight;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out flashlight, 1.5f, BedMonsterLayer))
        {
            GameObject monster = flashlight.collider.gameObject;

            if(monster.tag == "BedMonster")
            {
                var AI = monster.GetComponent<BedMonsterAI>();

                AI.GetOut();
            }
        }
    }
    private void PlayPickUpSound()
    {
        played = true;

        AudioClip clip = AudioSystem.GetSound("FlashLightPickUp", AudioType.Tools);

        AudioSystem.PlaySoundOnce("FlashlightPickable", AudioType.Tools, clip);
    }
    private void MoveFlashlight()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        transform.rotation = Quaternion.Lerp(transform.rotation, cam.transform.rotation, 1 * speed);
        transform.position = cam.transform.position;
    }
    private void UseFlashlight()
    {
        if (Input.GetKeyDown(F))
        {
            if (active)
            {
                AudioClip clip = AudioSystem.GetSound("FlashLightTurnOn", AudioType.Tools);

                AudioSystem.PlaySoundOnce("Flashlight", AudioType.Tools, clip);

                transform.GetChild(0).gameObject.SetActive(false);
                active = false;
            }
            else
            {
                AudioClip clip = AudioSystem.GetSound("FlashLightTurnOff", AudioType.Tools);

                AudioSystem.PlaySoundOnce("Flashlight", AudioType.Tools, clip);

                transform.GetChild(0).gameObject.SetActive(true);
                active = true;
            }
        }
    }
}
