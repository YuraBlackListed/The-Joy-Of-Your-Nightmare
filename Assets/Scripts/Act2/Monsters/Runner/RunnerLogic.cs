using UnityEngine;

public class RunnerLogic : MonoBehaviour
{
    [SerializeField] private FieldOfView FOV;

    [SerializeField] private RunnerMovement MonsterMovement;

    private bool stopAndThink = false;
    private bool startedChase = false;

    private float timeBeforeLosingPlayer = 9f;
    private float timeBeforeStartingChaseLeft = 2.5f;
    private float timeBeforeStartaingChaseDefault = 2.5f;
    private float timePenalty = 0.5f;
    private float timeBeingCalm = 3.5f;

    private void Update()
    {
        TryTurnOnExposureTimer();

        TryTurnOnChaseTimer();

        TryDoCountdowns();

        CheckChaseTimer();

        CheckStartTimer();
    }
    private void CheckChaseTimer()
    {
        if(timeBeforeLosingPlayer <= 0f)
        {
            MonsterMovement.ResetTarget();

            ResetLogic();
        }
    }
    private void CheckStartTimer()
    {
        if(timeBeforeStartingChaseLeft <= 0f)
        {
            startedChase = true;

            MonsterMovement.StartChase();
        }
    }
    private void TryTurnOnExposureTimer()
    {
        if (FOV.canSeePlayer)
        {
            stopAndThink = true;

            MonsterMovement.StopAndThink();
        }
        else if (!startedChase && !FOV.canSeePlayer && stopAndThink)
        {
            MonsterMovement.Invoke("ContinuePatrolling", timeBeingCalm);

            timeBeingCalm -= 0.1f;

            timeBeingCalm = Mathf.Clamp(timeBeingCalm, 1f, 3.5f);

            stopAndThink = false;
        }
    }
    private void TryTurnOnChaseTimer()
    {
        if(FOV.canSeePlayer && startedChase)
        {
            timeBeforeLosingPlayer = 9f;
        }
    }
    private void ResetLogic()
    {
        startedChase = false;

        timeBeforeLosingPlayer = 9f;

        timeBeingCalm -= 0.25f;

        timeBeingCalm = Mathf.Clamp(timeBeingCalm, 1f, 3.5f);

        timeBeforeStartingChaseLeft = timeBeforeStartaingChaseDefault;
        timeBeforeStartingChaseLeft -= timePenalty;
        timeBeforeStartaingChaseDefault -= timePenalty;
        timeBeforeStartingChaseLeft = Mathf.Clamp(timeBeforeStartingChaseLeft, 1f, 2.5f);
    }
    private void TryDoCountdowns()
    {
        if(startedChase)
        {
            timeBeforeLosingPlayer -= Time.deltaTime;
        }

        if (stopAndThink)
        {
            timeBeforeStartingChaseLeft -= Time.deltaTime;
        }
    }
}
