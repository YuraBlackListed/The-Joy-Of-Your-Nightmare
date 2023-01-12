using UnityEngine;

public class RunnerLogic : MonoBehaviour
{
    [SerializeField] private FieldOfView FOV;

    [SerializeField] private RunnerMovement MonsterMovement;

    private bool startCountdown = false;

    private float timeBeforeLosingPlayer = 9f;

    private void Update()
    {
        TryTurnOnTimer();

        TryDoCountdown();

        CheckTimer();
    }
    private void CheckTimer()
    {
        if(timeBeforeLosingPlayer <= 0f)
        {
            MonsterMovement.ResetTarget();

            ResetTimer();
        }
    }
    private void TryTurnOnTimer()
    {
        if (!FOV.canSeePlayer)
        {
            startCountdown = true;
        }
        else
        {
            MonsterMovement.StartChase();

            ResetTimer();
        }
    }
    private void ResetTimer()
    {
        startCountdown = false;

        timeBeforeLosingPlayer = 9f;
    }
    private void TryDoCountdown()
    {
        if(startCountdown)
        {
            timeBeforeLosingPlayer -= Time.deltaTime;
        }
    }
}
