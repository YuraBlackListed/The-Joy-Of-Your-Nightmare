using UnityEngine;

public class DoorMonsterAI : MonsterAI
{
    [SerializeField] private LampScript Lamp;
    [SerializeField] private KillerInfo info;
    [SerializeField] private DoorStages DoorStages;
    [SerializeField] private ScreamerScript screamer;

    private float roomEnterDelay = 2.25f;

    private void Start()
    {
        TryBlock();

        ResetRageModifier();

        InvokeRepeating(nameof(TryIncrease), 0f, 1f);
    }
    private void Update()
    {
        Progress = Mathf.Clamp(Progress, 0f, ProgressLimit);

        CheckProgress();

        TryCountdown();
    }
    protected override void TryBlock()
    {
        float chance = Random.value;

        if(chance <= delayChance)
        {
            isDelayed = true;
            Invoke(nameof(ResetMonster), Random.Range(1, 10));
        }
    }
    protected override void CheckProgress()
    {
        //print(Progress);
        if(Progress >= ProgressLimit)
        {
            doTimerCountdown = true;

            if(canAttack) Invoke(nameof(EnterRoom), roomEnterDelay);
        }
    }
    private void EnterRoom()
    {
        if (Lamp.active && timeLeft < 0f)
        {
            screamer.Scream();
            info.killerID = 2;
        }
        else if (timeLeft <= 0f && !Lamp.active)
        {
            Progress = 0f;

            ResetRageModifier();

            TryBlock();

            calmMod -= 1f;

            doTimerCountdown = false;

            timeLeft = timerTimeLimit;

            DoorStages.DoResetDoorSounds = true;
        }
    }
    protected override float RandomIncreasement()
    {
        return ((Time.deltaTime * Random.value * 10f + Random.Range(0f, 5.5f)) / calmMod) * randomRageMod;
    }
}
