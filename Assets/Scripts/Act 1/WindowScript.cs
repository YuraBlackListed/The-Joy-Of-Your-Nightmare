using UnityEngine;

public class WindowScript : MonoBehaviour
{
    public bool IsUsed;
    public bool IsClosed = false;
    private bool openPlayed = false;
    private bool closePlayed = true;
        
    [SerializeField] private Animator leftCurtain;
    [SerializeField] private Animator rightCurtain;

    private AudioClip curtainSound;

    private void Start()
    {
        curtainSound = AudioSystem.GetSound("Curtain", AudioType.Tools);
    }
    private void Update()
    {
        if (IsUsed) Close();
        else Open();
    }

    public void Open()
    {
        IsClosed = false;
        if(!openPlayed)
        {
            openPlayed = true;
            closePlayed = false;
            AudioSystem.PlaySoundOnce("Curtain", AudioType.Tools, curtainSound);
        }
        leftCurtain.SetBool("Opened", true);
        rightCurtain.SetBool("Opened", true);
    }

    public void Close()
    {
        IsClosed = true;
        if(!closePlayed)
        {
            closePlayed = true;
            openPlayed = false;
            AudioSystem.PlaySoundOnce("Curtain", AudioType.Tools, curtainSound);
        }
        leftCurtain.SetBool("Opened", false);
        rightCurtain.SetBool("Opened", false);
    }
}
