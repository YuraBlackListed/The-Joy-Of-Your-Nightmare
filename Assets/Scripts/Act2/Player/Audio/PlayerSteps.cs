using UnityEngine;

public class PlayerSteps : MonoBehaviour
{
    [SerializeField] private LayerMask MonsterLayer;

    [SerializeField] private MovementScript PlayerMovement;

    public float soundRangeMod = 1f;

    private void Update()
    {
        SetRangeMod();
    }
    private void SetRangeMod()
    {
        switch(PlayerState.instance.MovementStatus)
        {
            case PlayerMovementState.Idle:
                soundRangeMod = 0f;
                break;
            case PlayerMovementState.Walking:
                soundRangeMod = 1f;
                break;
            case PlayerMovementState.Running:
                soundRangeMod = 2.25f;
                break;
            case PlayerMovementState.Crouching:
                soundRangeMod = 0.35f;
                break;
        }
    }
    public void MakeStepSound()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 4f * soundRangeMod, MonsterLayer);

        foreach(var collider in colliders)
        {
            var runnerAi = collider.gameObject.GetComponent<RunnerLogic>();

            runnerAi.IncreaseExposure();

            return;
        }
    }
}
