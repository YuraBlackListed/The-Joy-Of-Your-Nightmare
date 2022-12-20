using UnityEngine;

public class ScreamerScript : MonoBehaviour
{
    [SerializeField] private Animator doll;
    [SerializeField] private Animator player;
    [SerializeField] private SceneFade sceneFade;
    [SerializeField] private AudioSource screamer;
    void Update()
    {
        
    }
    public void Scream()
    {
        doll.SetBool("Dead", true);
        player.SetBool("Dead", true);
        screamer.Play();
    }
    public void Fade()
    {
        sceneFade.ActivateFade(0);
    }
}
