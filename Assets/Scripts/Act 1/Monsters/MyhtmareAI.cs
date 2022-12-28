using UnityEngine;

public class MyhtmareAI : MonoBehaviour
{
    //DO NOT USE THIS SCRIPT IT TEMPORARLY BROKEN AND BEING SEPARETED TO OTHER SCRIPTS!!!!
    #region [Progresses]
    public float WindowProgress { get; private set; }
    public float ClosetProgress { get; private set; }
    #endregion

    #region [Delays]
    private bool windowDelayed = false;
    private bool closetDelayed = false;
    #endregion

    #region [Resets]
    private bool windowReset = false;
    private bool closetReset = false;
    #endregion

    #region [Timers]
    private float windowTimeLeft = 2f;
    private bool windowTimerIsActive = false;

    private float closetTimeLeft = 2f;
    private bool closetTimerIsActive = false;
    #endregion

    #region [Spots]
    [SerializeField] private WindowScript Window;
    //Needed closet here
    #endregion

    private float calmModifier = 1f;
    private float progressLimit = 100f;

    private void Start()
    {
        TryBlock();
    }
    private void Update()
    {
        TryIncreaseProgress();

        ClampValues();

        CheckProgress();

        TryReset();

        TryStartTimersCountdown();
    }
    private void CheckProgress()
    {
        if(WindowProgress >= progressLimit)
        {
            WindowJumpscare();
        }
        if(ClosetProgress >= progressLimit)
        {
            //ClosetJumpscare();
        }
    }
    #region [Jumpscares]
    private void WindowJumpscare()
    {
        windowTimerIsActive = true;

        Debug.Log("Close da fukin window");

        if(windowTimeLeft <= 0f && !Window.IsClosed)
        {
            //do jumpscare
            Debug.Log("Window jumscare");
        }
        else if (windowTimeLeft <= 0 && Window.IsClosed)
        {
            windowReset = true;
        }
    }
    private void ClosetJumpscare()
    {
        closetTimerIsActive = true;

        if (closetTimeLeft <= 0) // and Closet is NOT closed
        {
            //do jumscare
        }
        else if (closetTimeLeft <= 0) // and Closte IS closed
        {
            ClosetProgress = 0f;

            closetTimerIsActive = false;
            closetTimeLeft = 2f;

            calmModifier -= 2f;
        }
    }
    #endregion

    #region [Reset methods]
    private void TryReset()
    {
        if (windowReset)
        {
            WindowReset();
        }
        if (closetReset)
        {
            //do closet reset
        }
    }
    private void WindowReset()
    {
        windowReset = false;

        WindowProgress = 0f;

        windowTimerIsActive = false;
        windowTimeLeft = 2f;

        calmModifier -= 1.5f;
        calmModifier = Mathf.Clamp(calmModifier, 1f, 20f);
    }
    #endregion

    private void ClampValues()
    {
        WindowProgress = Mathf.Clamp(WindowProgress, 0f, 100f);
        ClosetProgress = Mathf.Clamp(ClosetProgress, 0f, 100f);
    }
    private void TryBlock()
    {
        float chanceToBlock = Random.value;

        if(chanceToBlock <= 0.15f)
        {
            ChangeMyhtmareSpots(true, true, true);

            Invoke(nameof(UnblockMyhtmare), 5f);

            return;
        }

        if(chanceToBlock <= 0.35f)
        {
            ChangeMyhtmareSpots(true, true, false);

            Invoke(nameof(UnblockMyhtmare), 5f);

            return;
        }

        if (chanceToBlock <= 0.55f)
        {
            ChangeMyhtmareSpots(true, false, false);

            Invoke(nameof(UnblockMyhtmare), 5f);

            return;         
        }

        if (chanceToBlock <= 0.75f)
        {
            ChangeMyhtmareSpots(false, true, false);

            Invoke(nameof(UnblockMyhtmare), 5f);

            return;
        }

        ChangeMyhtmareSpots(false, false, false);

        Invoke(nameof(UnblockMyhtmare), 5f);
    }
    private void TryStartTimersCountdown()
    {
        if(windowTimerIsActive)
        {
            windowTimeLeft -= Time.deltaTime;
        }
        if(closetTimerIsActive)
        {
            closetTimeLeft -= Time.deltaTime;
        }
    }
    private void ChangeMyhtmareSpots(bool window, bool closet, bool door)
    {
        windowDelayed = window;
        closetDelayed = closet;
    }
    private void UnblockMyhtmare()
    {
        ChangeMyhtmareSpots(false, false, false);
    }
    private void TryIncreaseProgress()
    {
        if (!windowDelayed)
        {
            WindowProgress += RandomIncreasement();
        }
        if (!closetDelayed)
        {
            ClosetProgress += RandomIncreasement();
        }
    }
    private float RandomIncreasement()
    {
        return (Time.deltaTime * Random.value * 10f + Random.Range(0f, 5.5f)) / calmModifier;
    }
}
