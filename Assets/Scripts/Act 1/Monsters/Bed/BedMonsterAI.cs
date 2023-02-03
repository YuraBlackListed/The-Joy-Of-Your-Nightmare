using UnityEngine;

public class BedMonsterAI : MonsterAI
{
    [SerializeField] private BedStages Stages;

    private void Start()
    {
        TryBlock();

        ResetRageModifier();
    }
    private void  Update()
    {
        TryIncrease();

        Progress = Mathf.Clamp(Progress, 0f, ProgressLimit);

        CheckProgress();

        TryCountdownTimer();

        CheckTimer();
    }
    public void GetOut()
    {
        AudioClip clip = AudioSystem.GetSound("BedMonsterScared", AudioType.Monsters);

        AudioSystem.PlaySoundOnce("BedMonster", AudioType.Monsters, clip);

        Stages.DoBedSoundReset = true;

        ResetMonster();
    }
    private void TryBlock()
    {
        float chance = Random.value;

        if(chance >= delayChance)
        {
            isDelayed = true;
            Invoke(nameof(ResetBed), Random.Range(1, 15));
        }
    }
    private void ResetBed()
    {
        isDelayed = false;
    }
    private void ResetRageModifier()
    {
        randomRageMod = Random.Range(1, 6);
    }
    private void TryIncrease()
    {
        if(!isDelayed)
        {
            Progress += RandomIncreasement();
        }
    }
    private void CheckProgress()
    {
        if(Progress >= 200f)
        {
            doTimerCountdown = true;
        }
    }
    private void TryCountdownTimer()
    {
        if(doTimerCountdown)
        {
            timeLeft -= Time.deltaTime;
        }
    }
    private void CheckTimer()
    {
        if(timeLeft <= 0 && canAttack)
        {
            //do jumpscare
        }
    }
    private void ResetMonster()
    {
        Progress = 0f;

        doTimerCountdown = false;

        timeLeft = timerTimeLimit;

        ResetRageModifier();

        TryBlock();
    }
    private float RandomIncreasement()
    {
        return ((Time.deltaTime * Random.value * 10f + Random.Range(0f, 15.5f)) / calmMod) * randomRageMod;
    }
}
