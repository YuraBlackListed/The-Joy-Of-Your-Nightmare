using UnityEngine;

public class DoorMonsterAI : MonoBehaviour
{
    public float DoorProgress { get; private set; }

    [SerializeField] private LampScript Lamp;
    [SerializeField] private DoorStages DoorStages;

    private float inRoomTimeLeft = 6.5f;
    private float chanceToDelay = 0.35f;
    private float roomEnterDelay = 2.25f;
    //The more it less it gets, the faster the progress increasing (this >= 0)
    private float doorCalmMod = 5f;

    private int randomRageMod;

    private bool doorDelayed = false;
    private bool doTimerCountdown = false;

    private void Start()
    {
        TryBlock();

        ResetRageModifier();
    }
    private void Update()
    {
        TryIncrease();

        DoorProgress = Mathf.Clamp(DoorProgress, 0f, 100f);

        CheckProgress();

        TryCountdown();
    }
    private void TryIncrease()
    {
        if(!doorDelayed)
        {
            DoorProgress += RandomIncreasement();
        }
    }
    private void ResetRageModifier()
    {
        randomRageMod = Random.Range(1, 3);
    }
    private void TryBlock()
    {
        float chance = Random.value;

        if(chance <= chanceToDelay)
        {
            doorDelayed = true;
            Invoke(nameof(ResetDoor), Random.Range(1, 10));
        }
    }
    private void TryCountdown()
    {
        if(doTimerCountdown)
        {
            inRoomTimeLeft -= Time.deltaTime;
        }
    }
    private void CheckProgress()
    {
        if(DoorProgress >= 100f)
        {
            doTimerCountdown = true;

            Invoke(nameof(EnterRoom), roomEnterDelay - 0.5f);
        }
    }
    private void EnterRoom()
    {
        if (Lamp.active && inRoomTimeLeft > 0f)
        {
            //Jumpscare here (needed)
        }
        else if (inRoomTimeLeft <= 0f && !Lamp.active);
        {
            DoorProgress = 0f;

            ResetRageModifier();

            TryBlock();

            doorCalmMod -= 1f;

            DoorStages.DoResetDoorSounds = true;
        }
    }
    private void ResetDoor()
    {
        doorDelayed = false;
    }
    private float RandomIncreasement()
    {
        return ((Time.deltaTime * Random.value * 10f + Random.Range(0f, 5.5f)) / doorCalmMod) * randomRageMod;
    }
}
