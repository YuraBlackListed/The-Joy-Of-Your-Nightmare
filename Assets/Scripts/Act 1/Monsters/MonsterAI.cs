using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float Progress;
    public float ProgressLimit;

    [SerializeField] protected float delayChance = 0;
    [SerializeField] protected float calmMod = 0f;
    [SerializeField] protected float timerTimeLimit = 0f;

    [SerializeField] protected internal bool canAttack = false;

    protected float timeLeft = 0f;
    protected float randomRageMod = 0f;

    protected bool isDelayed = false;
    protected bool doTimerCountDown = false;

    public virtual void SetNewLimit(float limit)
    {
        ProgressLimit = limit;
    }
}
