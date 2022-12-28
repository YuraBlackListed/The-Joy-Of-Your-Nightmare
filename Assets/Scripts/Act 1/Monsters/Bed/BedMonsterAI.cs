using UnityEngine;

public class BedMonsterAI : MonoBehaviour
{
    public float BedProgress { get; private set; }

    [SerializeField] private BedStages Stages;

    private float timeLeft = 5f;
    private float chanceToDelay = 0.55f;
    private float bedCalmMod = 10f;

    private int randomRageMod;

    private bool bedDelayed = false;
    private bool doTimerCountdown = false;

    private void Start()
    {
        TryBlock();

        ResetRageModifier();
    }
    private void  Update()
    {
        TryIncrease();

        BedProgress = Mathf.Clamp(BedProgress, 0f, 200f);

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

        if(chance >= chanceToDelay)
        {
            bedDelayed = true;
            Invoke(nameof(ResetBed), Random.Range(1, 15));
        }
    }
    private void ResetBed()
    {
        bedDelayed = false;
    }
    private void ResetRageModifier()
    {
        randomRageMod = Random.Range(1, 6);
    }
    private void TryIncrease()
    {
        if(!bedDelayed)
        {
            BedProgress += RandomIncreasement();
        }
    }
    private void CheckProgress()
    {
        if(BedProgress >= 200f)
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
        if(timeLeft <= 0)
        {
            //do jumpscare
        }
    }
    private void ResetMonster()
    {
        BedProgress = 0f;

        doTimerCountdown = false;

        timeLeft = 5f;

        ResetRageModifier();

        TryBlock();
    }
    private float RandomIncreasement()
    {
        return ((Time.deltaTime * Random.value * 10f + Random.Range(0f, 15.5f)) / bedCalmMod) * randomRageMod;
    }
}
