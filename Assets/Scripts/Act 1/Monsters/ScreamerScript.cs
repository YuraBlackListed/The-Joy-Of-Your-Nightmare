using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScreamerScript : MonoBehaviour
{
    [SerializeField] private Animator doll;
    [SerializeField] private Animator player;

    [SerializeField] private SceneFade sceneFade;

    [SerializeField] private CameraLook lookScript;

    [SerializeField] private GameObject screammerDoll;
    [SerializeField] private GameObject crosshair;

    [SerializeField] private List<GameObject> monsters;

    [SerializeField] private Image deathScreen;

    [SerializeField] private string MyAudioSourceName;
     

    bool played = false;

    public void Fall()
    {
        doll.SetBool("Fall", true);
    }
    public void Scream()
    {
        if(!played)
        {
            played = true;
            for(int i = 0; i < monsters.Count; i++)
            {
                monsters[i].SetActive(false);
            }
            crosshair.SetActive(false);
            screammerDoll.SetActive(true);

            lookScript.enabled = false;

            player.SetBool("Dead", true);

            AudioClip clip = AudioSystem.GetSound("Screamer1", AudioType.Monsters);

            AudioSystem.PlaySoundOnce(MyAudioSourceName, AudioType.Monsters, clip);

            Invoke(nameof(ShutOff), 0.7f);
        }
        
    }
    
    void ShutOff()
    {
        deathScreen.DOFade(1f, 0.2f);
    }
    private void MenuScene()
    {
        sceneFade.ActivateFade(2);
        Cursor.visible = true;
    }
    void Update()
    {
        if(played)
        {
            Invoke(nameof(MenuScene), 4f);
        }
    }
}
