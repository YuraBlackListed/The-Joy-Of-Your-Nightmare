using UnityEngine;

public class WindowScript : MonoBehaviour
{
    public bool IsUsed;
    public bool IsClosed = false;
    private bool openPlayed = false;
    private bool closePlayed = true;
    
    [SerializeField] private AudioSource CurtainOpenSound;
    [SerializeField] private AudioSource CurtainCloseSound;
    
    [SerializeField] private Animator leftCurtain;
    [SerializeField] private Animator rightCurtain;

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
            CurtainOpenSound.Play();
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
            CurtainCloseSound.Play();
        }
        leftCurtain.SetBool("Opened", false);
        rightCurtain.SetBool("Opened", false);
    }
}
