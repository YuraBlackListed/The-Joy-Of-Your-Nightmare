using UnityEngine;

public class WindowScript : Interactable
{
    public bool IsClosed = false;
    private bool openPlayed = false;
    private bool closePlayed = true;
        
    [SerializeField] private Animator leftCurtain;
    [SerializeField] private Animator rightCurtain;

    private void Start()
    {
        Open();
    }
    public override void Interact()
    {
        IsClosed = true;

        Close();
    }
    public override void StopInteract()
    {
        IsClosed = false;

        Open();
    }
    public void Open()
    {
        if(!openPlayed)
        {
            openPlayed = true;
            closePlayed = false;

            var clip = AudioSystem.GetSound("CurtainOpen", AudioType.Furniture);

            AudioSystem.PlaySoundOnce("Curtain", AudioType.Furniture, clip);
        }
        leftCurtain.SetBool("Opened", true);
        rightCurtain.SetBool("Opened", true);
    }

    public void Close()
    {
        if(!closePlayed)
        {
            closePlayed = true;
            openPlayed = false;

            var clip = AudioSystem.GetSound("CurtainClose", AudioType.Furniture);

            AudioSystem.PlaySoundOnce("Curtain", AudioType.Furniture, clip);
        }
        leftCurtain.SetBool("Opened", false);
        rightCurtain.SetBool("Opened", false);
    }
}
