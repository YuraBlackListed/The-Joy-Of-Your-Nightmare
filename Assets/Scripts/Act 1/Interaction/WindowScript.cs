using UnityEngine;

public class WindowScript : Interactable
{
    public bool IsUsed;
    public bool IsClosed = false;
    private bool openPlayed = false;
    private bool closePlayed = true;
        
    [SerializeField] private Animator leftCurtain;
    [SerializeField] private Animator rightCurtain;

    public override void Interact()
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

            AudioClip clip = AudioSystem.GetSound("CurtainClose", AudioType.Furniture);

            AudioSystem.PlaySoundOnce("Curtain", AudioType.Furniture, clip);
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

            AudioClip clip = AudioSystem.GetSound("CurtainOpen", AudioType.Furniture);

            AudioSystem.PlaySoundOnce("Curtain", AudioType.Furniture, clip);
        }
        leftCurtain.SetBool("Opened", false);
        rightCurtain.SetBool("Opened", false);
    }
}
