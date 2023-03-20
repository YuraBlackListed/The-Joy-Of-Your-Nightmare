using UnityEngine;

public class WindowMonsterAI : MonsterAI
{
    [SerializeField] private WindowScript Window;
    [SerializeField] private KillerInfo info;
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
    protected override void TryDoJumpscare()
    {
        TryDoWindowJumpscare();
    }
    private void TryDoWindowJumpscare()
    {
        if (timeLeft <= 0f && !Window.IsClosed)
        {
            animator.SetBool("Fall", true);
            info.killerID = 1;
        }
        else if (timeLeft <= 0f && Window.IsClosed)
        {
            print(timeLeft);
            Progress = 0f;
            doTimerCountdown = false;

            SetNewLimit(Random.Range(50f, 200f));
            
            ResetRageModifier();

            TryBlock();

            timeLeft = timerTimeLimit;

            calmMod -= 1f;
        }
    }
    protected override float RandomIncreasement()
    {
        return ((Time.deltaTime * Random.value * 10f) / calmMod * levelScrObj.EnemiesLevel) * randomRageMod + Random.Range(0f, 6f);
    }
}