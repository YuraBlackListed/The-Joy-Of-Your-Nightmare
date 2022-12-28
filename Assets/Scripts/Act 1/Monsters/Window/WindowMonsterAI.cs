using UnityEngine;

public class WindowMonsterAI : MonoBehaviour
{
    public float WindowProgress { get; private set; }

    [SerializeField] private WindowScript Window;

    private float windowTimeLeft = 2f;
    private float chanceToDelay = 0.35f;
    private float windowCalmMod = 0.5f;

    private int randomRageMod;

    private bool windowDelayed = false;
    private bool doTimerCountdown = false;

    private void Start()
    {
        TryBlock();

        ResetRageModifier();

        InvokeRepeating(nameof(TryIncrease), 0f, 0.75f);
    }
    private void Update()
    {
        CheckProgress();

        TryCountdown();
    }
    private void TryIncrease()
    {
        if (!windowDelayed)
        {
            WindowProgress += RandomIncreasement();

            WindowProgress = Mathf.Clamp(WindowProgress, 0f, 100f);
        }
    }
    private void ResetRageModifier()
    {
        randomRageMod = Random.Range(1, 3);
    }
    private void TryBlock()
    {
        float chance = Random.value;

        if (chance <= chanceToDelay)
        {
            windowDelayed = true;
            Invoke(nameof(ResetWindow), Random.Range(1, 10));
        }
    }
    private void TryCountdown()
    {
        if (doTimerCountdown)
        {
            windowTimeLeft -= Time.deltaTime;
        }
    }
    private void CheckProgress()
    {
        if (WindowProgress >= 100f)
        {
            doTimerCountdown = true;
        }
    }
    private void TryDoWindowJumpscare()
    {
        if (windowTimeLeft <= 0f && !Window.IsClosed)
        {
            //Jumpscare here (needed)
        }
        else if (windowTimeLeft <= 0f && Window.IsClosed)
        {
            WindowProgress = 0f;

            ResetRageModifier();

            TryBlock();

            windowCalmMod -= 1f;
        }
    }
    private void ResetWindow()
    {
        windowDelayed = false;
    }
    private float RandomIncreasement()
    {
        return ((Time.deltaTime * Random.value * 10f) / windowCalmMod) * randomRageMod + Random.Range(0f, 6f);
    }

}
