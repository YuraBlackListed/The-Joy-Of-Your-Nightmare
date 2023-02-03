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

        TryCountdown();

        CheckTimer();
    }
    public void GetOut()
    {
        AudioClip clip = AudioSystem.GetSound("BedMonsterScared", AudioType.Monsters);

        AudioSystem.PlaySoundOnce("BedMonster", AudioType.Monsters, clip);

        Stages.DoBedSoundReset = true;

        ResetProgress();
    }
    private void CheckTimer()
    {
        if(timeLeft <= 0 && canAttack)
        {
            //do jumpscare
        }
    }
    private void ResetProgress()
    {
        Progress = 0f;

        doTimerCountdown = false;

        SetNewLimit(Random.Range(50f, 250f));

        timeLeft = timerTimeLimit;

        ResetRageModifier();

        TryBlock();
    }
    protected override float RandomIncreasement()
    {
        return ((Time.deltaTime * Random.value * 10f + Random.Range(0f, 15.5f)) / calmMod) * randomRageMod;
    }
}
