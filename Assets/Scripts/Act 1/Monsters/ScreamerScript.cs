using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScreamerScript : MonoBehaviour
{
    [SerializeField] private Animator doll;
    [SerializeField] private Animator player;

    [SerializeField] private SceneFade sceneFade;

    [SerializeField] private CameraLook lookScript;

    [SerializeField] private GameObject screammerDoll;

    [SerializeField]private Image deathScreen;

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

            screammerDoll.SetActive(true);

            lookScript.enabled = false;

            player.SetBool("Dead", true);

            AudioClip clip = AudioSystem.GetSound("Screamer1", AudioType.Monsters);

            AudioSystem.PlaySoundOnce("Screamer1", AudioType.Monsters, clip);

            deathScreen.DOFade(1f, 1.2f);
        }
        
    }
    private void MenuScene()
    {
        sceneFade.ActivateFade(0);
    }
    void Update()
    {
        if(played)
        {
            Invoke(nameof(MenuScene), 15f);
            Cursor.visible = true;
        }
    }
}
