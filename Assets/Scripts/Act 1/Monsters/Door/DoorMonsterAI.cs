using UnityEngine;

public class DoorMonsterAI : MonsterAI
{
    [SerializeField] private LampScript Lamp;
    [SerializeField] private DoorStages DoorStages;
    [SerializeField] private ScreamerScript screamer;
    [SerializeField] private LevelScriptableObject levelScrObj;

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
    private void TryIncrease()
    {
        if(!isDelayed)
        {
            Progress += RandomIncreasement();
        }
    }
    private void ResetRageModifier()
    {
        randomRageMod = Random.Range(1, 3);
    }
    private void TryBlock()
    {
        float chance = Random.value;

        if(chance <= delayChance)
        {
            isDelayed = true;
            Invoke(nameof(ResetDoor), Random.Range(1, 10));
        }
    }
    private void TryCountdown()
    {
        if(doTimerCountdown)
        {
            timeLeft -= Time.deltaTime;
        }
    }
    private void CheckProgress()
    {
        if(Progress >= 85f)
        {
            doTimerCountdown = true;

            if(canAttack) Invoke(nameof(EnterRoom), roomEnterDelay);
        }
    }
    private void EnterRoom()
    {
        if (Lamp.active && timeLeft > 0f)
        {
            screamer.Scream();
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
    private void ResetDoor()
    {
        isDelayed = false;
    }
    private float RandomIncreasement()
    {
        return ((Time.deltaTime * Random.value * 10f + Random.Range(0f, 5.5f)) / calmMod * levelScrObj.EnemiesLevel) * randomRageMod;
    }
}
