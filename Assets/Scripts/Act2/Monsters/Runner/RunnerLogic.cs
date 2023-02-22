using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class RunnerLogic : MonoBehaviour
{
    [SerializeField] private FieldOfView FOV;

    [SerializeField] private RunnerMovement MonsterMovement;

    [SerializeField] private GameObject Player;

    [SerializeField] private LayerMask UsableLayer;

    [SerializeField] private float MaxPlayerSoundExposure;

    [SerializeField] private bool startedChase = false;

    [SerializeField] private float ChaseLength;

    private bool stopAndThink = false;
    private bool heardPlayer = false;

    private float timeBeforeLosingPlayer;
    private float timeBeforeStartingChaseLeft = 2.5f;
    private float timeBeforeStartaingChaseDefault = 2.5f;
    private float timePenalty = 0.5f;
    private float timeBeingCalm = 3.5f;
    private float playerSoundExposure = 0;

    private GameObject shelter;

    private void Start()
    {
        timeBeforeLosingPlayer = ChaseLength;
    }
    private void Update()
    {
        TryTurnOnExposureTimer();

        TryTurnOnChaseTimer();

        TryDoCountdowns();

        CheckStartTimer();

        CheckChaseTimer();

        if (startedChase)
        {
            TryCheckShelter();
        }
    }
    private void CheckChaseTimer()
    {
        if(timeBeforeLosingPlayer <= 0f)
        {
            if (MonsterMovement.IsChecking())
            {
                return;
            }

            Invoke(nameof(DoChaseAftermath), 1.5f);
        }
    }
    private void CheckStartTimer()
    {
        if (MonsterMovement.IsChecking())
        {
            return;
        }

        if (timeBeforeStartingChaseLeft <= 0f || playerSoundExposure > MaxPlayerSoundExposure)
        {
            heardPlayer = true;

            startedChase = true;

            MonsterMovement.StartChase();
        }
    }
    private void TryTurnOnExposureTimer()
    {
        if (FOV.canSeePlayer && !startedChase && !heardPlayer)
        {
            stopAndThink = true;

            MonsterMovement.StopMoving();
        }
        else if (!startedChase && !FOV.canSeePlayer && stopAndThink && !MonsterMovement.IsChecking())
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
            timeBeforeLosingPlayer = ChaseLength;
        }
    }
    private void DoChaseAftermath()
    {
        if (MonsterMovement.IsChecking())
        {
            return;
        }

        playerSoundExposure = 0f;

        startedChase = false;

        heardPlayer = false;

        //Stop and go after 4 secs
        MonsterMovement.StopMoving();
        MonsterMovement.Invoke("ContinuePatrolling", 4f);

        //Reset timers
        timeBeforeLosingPlayer = ChaseLength;

        timeBeingCalm -= 0.25f;

        //Change values
        timeBeforeStartingChaseLeft = timeBeforeStartaingChaseDefault;
        timeBeforeStartingChaseLeft -= timePenalty;
        timeBeforeStartaingChaseDefault -= timePenalty;

        //Clamp values
        timeBeingCalm = Mathf.Clamp(timeBeingCalm, 1f, 3.5f);
        timeBeforeStartingChaseLeft = Mathf.Clamp(timeBeforeStartingChaseLeft, 1f, 2.5f);
    }
    private void TryCheckShelter()
    {
        if(PlayerState.instance.Status == PlayerStatus.Hidden)
        {
            if(FOV.canSeePlayer)
            {
                shelter = PlayerState.instance.CurrentShelter;
            }
            else
            {
                CheckPlayerDistance();
            }
        }

        if(shelter != null)
        {
            if(IsNearShelter())
            {
                CheckShelter();
            }
        }
    }
    private void CheckShelter()
    {
        MonsterMovement.StopMoving();

        var script = shelter.GetComponent<ShelterScript>();

        script.TryGetPlayerOut();
    }
    private void CheckPlayerDistance()
    {
        if(MonsterMovement.FindDistanceToTarget(Player.transform) <= 7.5f && !MonsterMovement.IsChecking())
        {
            MonsterMovement.StartChecking();

            InvesigateShelters();
        }
    }
    private void InvesigateShelters()
    {
        MonsterMovement.StopMoving();

        MonsterMovement.PrepareToCheck();
        
        //Get nearby interactables

        Collider[] nearestInteractables = Physics.OverlapSphere(transform.position, 100f, UsableLayer);

        List<ShelterScript> shelters = new List<ShelterScript>();

        //Sort shelters
        foreach(var interactable in nearestInteractables)
        {
            if(interactable.gameObject.tag == "Closet")
            {
                var shelterScript = interactable.gameObject.GetComponent<ShelterScript>();

                shelters.Add(shelterScript);
            }
        }

        MonsterMovement.StartChecking();

        StartCoroutine(CheckShelters(shelters));
    }
    private IEnumerator CheckShelters(List<ShelterScript> shelters)
    {
        shelters = MixList(shelters);

        foreach(var shelter in shelters)
        {
            float chance = 0.225f * shelter.SafetyLevel;

            if(Random.value >= chance)
            {
                MonsterMovement.CheckingTarget = shelter.OutPosition;

                //Change safety level randomly
                if (Random.value >= 0.5f)
                {
                    shelter.SafetyLevel--;
                }
                else
                {
                    shelter.SafetyLevel++;
                }

                //Move to target
                while (true)
                {
                    MonsterMovement.StartChecking();

                    if (MonsterMovement.FindDistanceToTarget(shelter.OutPosition) < 1f)
                    {
                        break;
                    }

                    yield return null;
                }

                transform.rotation = shelter.OutPosition.rotation;

                yield return new WaitForSeconds(1f);

                shelter.TryGetPlayerOut();

                //Jumpscare player here (needed)
            }
            else
            {
                MonsterMovement.CheckingTarget = transform;

                shelter.SafetyLevel--;

                yield return new WaitForSeconds(1.25f);
            }

            yield return new WaitForSecondsRealtime(3.75f);
        }

        MonsterMovement.StopMoving();

        DoChaseAftermath();

        yield break;
    }
    private List<ShelterScript> MixList(List<ShelterScript> shelters)
    {
        for(int i = 0; i < shelters.Count - 1; i++)
        {
            var currentShelter = shelters[i];

            int randomIndex = Random.Range(i, shelters.Count - 1);

            shelters[i] = shelters[randomIndex];

            shelters[randomIndex] = currentShelter;
        }

        return shelters;
    }
    private bool IsNearShelter()
    {
        if(MonsterMovement.FindDistanceToTarget(shelter.transform) <= 4.5f)
        {
            return true;
        }

        return false;
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
    public void IncreaseExposure()
    {
        if (startedChase) return;

        playerSoundExposure += (Random.value * 5f) * Time.deltaTime;
    }
}
