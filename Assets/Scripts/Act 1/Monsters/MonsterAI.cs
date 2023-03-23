using UnityEngine;

/*PATER NOSTER, qui es in caelis, 
 * sanctificetur nomen tuum. 
 * Adveniat regnum tuum. Fiat voluntas tua, 
 * sicut in caelo et in terra. 
 * Panem nostrum quotidianum da nobis hodie, 
 * et dimitte nobis debita nostra sicut et nos dimittimus debitoribus nostris. 
 * Et ne nos inducas in tentationem, sed libera nos a malo. Amen.*/

public class MonsterAI : MonoBehaviour
{
    public float Progress;
    public float ProgressLimit;

    [SerializeField] protected float delayChance = 0;
    [SerializeField] protected float calmMod = 0f;
    [SerializeField] protected float timerTimeLimit = 2f;

    [SerializeField] protected internal bool canAttack = false;

    protected float timeLeft = 2f;
    protected float randomRageMod = 0f;

    protected bool isDelayed = false;
    protected bool doTimerCountdown = false;

    protected virtual void TryIncrease()
    {
        if (!isDelayed && !doTimerCountdown)
        {
            Progress += RandomIncreasement();

            Progress = Mathf.Clamp(Progress, 0f, ProgressLimit);
        }
    }
    protected virtual void SetNewLimit(float limit)
    {
        ProgressLimit = limit;
    }
    protected virtual void ResetMonster()
    {
        isDelayed = false;
    }
    protected virtual void TryBlock()
    {
        float chance = Random.value;

        if (chance <= delayChance)
        {
            isDelayed = true;
            Invoke(nameof(ResetMonster), Random.Range(1, 10));
        }
    }
    protected virtual void CheckProgress()
    {
        if (Progress >= ProgressLimit - 2f)
        {
            doTimerCountdown = true;

            if (canAttack) TryDoJumpscare();
        }
    }
    protected virtual void TryDoJumpscare()
    {

    }
    protected virtual void TryCountdown()
    {
        if (doTimerCountdown)
        {
            timeLeft -= Time.deltaTime;
        }
    }
    protected void ResetRageModifier()
    {
        timeLeft = 2f;
        randomRageMod = Random.Range(1, 3);
    }
    protected virtual float RandomIncreasement()
    {
        return 0f;
    }
}
