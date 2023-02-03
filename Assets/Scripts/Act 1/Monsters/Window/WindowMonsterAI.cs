using UnityEngine;

public class WindowMonsterAI : MonsterAI
{
    [SerializeField] private WindowScript Window;
    [SerializeField] private Animator animator;
    [SerializeField] private LevelScriptableObject levelScrObj;

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
        if (!isDelayed)
        {
            Progress += RandomIncreasement();

            Progress = Mathf.Clamp(Progress, 0f, ProgressLimit);
        }
    }
    private void ResetRageModifier()
    {
        randomRageMod = Random.Range(1, 3);
    }
    private void TryBlock()
    {
        float chance = Random.value;

        if (chance <= delayChance)
        {
            isDelayed = true;
            Invoke(nameof(ResetWindow), Random.Range(1, 10));
        }
    }
    private void TryCountdown()
    {
        if (doTimerCountdown)
        {
            timeLeft -= Time.deltaTime;
        }
    }
    private void CheckProgress()
    {
        if (Progress >= ProgressLimit)
        {
            doTimerCountdown = true;

            if(canAttack) TryDoWindowJumpscare();
        }
    }
    private void TryDoWindowJumpscare()
    {
        if (timeLeft <= 0f && !Window.IsClosed)
        {
            animator.SetBool("Fall", true);
        }
        else if (timeLeft <= 0f && Window.IsClosed)
        {
            Progress = 0f;
            doTimerCountdown = false;
            
            ResetRageModifier();

            TryBlock();

            timeLeft = timerTimeLimit;

            calmMod -= 1f;
        }
    }
    private void ResetWindow()
    {
        isDelayed = false;
    }
    private float RandomIncreasement()
    {
        return ((Time.deltaTime * Random.value * 10f) / calmMod * levelScrObj.EnemiesLevel) * randomRageMod + Random.Range(0f, 6f);
    }

}