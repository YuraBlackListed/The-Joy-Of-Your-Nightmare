using UnityEngine;

public class CornererAI : MonoBehaviour
{
    public float Progress;
    public float ProgressLimit;

    [SerializeField] private LeaningScript PlayerLeaningScript;

    [SerializeField] private GameObject CornererPrefab;

    [SerializeField] private Transform MainCamera;

    [SerializeField] private LayerMask CornererLayer;

    [SerializeField] private float TimeLeftForAttack;
    [SerializeField] private float TimerAttackLimit;

    [SerializeField] private float TimeBeforeDissapearing;
    [SerializeField] private float TimerLimitForDissapearing;

    [SerializeField] private int calmMod;

    private bool canSpawn = false;
    private bool spawned = false;
    private bool doAttackTimer = false;
    private bool isStopped = false;

    private GameObject currentCornerer;

    private float randomRageMod;

    private void Start()
    {
        ResetRageModifier();

        InvokeRepeating(nameof(TryIncrease), 0f, 1.25f);
    }
    private void Update()
    {
        CheckProgress();

        if(!spawned)
        {
            TrySpawn();
        }

        if (spawned)
        {
            TryAttack();
        }

        TryDoCountdown();

        CheckTimers();
    }
    private void CheckProgress()
    {
        if(Progress >= ProgressLimit)
        {
            canSpawn = true;

            Progress = Mathf.Clamp(Progress, 0f, ProgressLimit);
        }
    }
    private void TrySpawn()
    {
        if(PlayerLeaningScript.CurrentLeanPlace == null)
        {
            return;
        }

        if(!PlayerLeaningScript.IsLeaning)
        {
            return;
        }

        if(!canSpawn)
        {
            return;
        }

        Spawn();

    }
    private void TryAttack()
    {
        RaycastHit lookRay;

        if (Physics.Raycast(MainCamera.position, MainCamera.forward, out lookRay, 10f, CornererLayer))
        {
            if(lookRay.collider.tag == "Cornerer")
            {
                doAttackTimer = true;
            }
            else
            {
                doAttackTimer = false;
            }
        }
        else
        {
            doAttackTimer = false;
        }
    }
    private void CheckTimers()
    {
        if (TimeLeftForAttack <= 0)
        {
            //do Jumpscare here pls

            Debug.Log("sosi zhopu");
        }
        if(TimeBeforeDissapearing <= 0)
        {
            ResetCornerer();
        }

    }
    private void ResetCornerer()
    {
        doAttackTimer = false;

        spawned = false;

        canSpawn = false;

        isStopped = false;

        TimeBeforeDissapearing = TimerLimitForDissapearing;
        TimeLeftForAttack = TimerAttackLimit;

        Progress = 0f;

        Destroy(currentCornerer);

        if(Random.value >= 0.5f)
        {
            calmMod--;
        }
    }
    private void TryDoCountdown()
    {
        if(spawned && !doAttackTimer)
        {
            TimeBeforeDissapearing -= Time.deltaTime;
        }

        if(doAttackTimer)
        {
            TimeLeftForAttack -= Time.deltaTime;
        }
    }
    private void Spawn()
    {
        spawned = true;

        isStopped = true;

        Progress = 0;

        canSpawn = false;

        currentCornerer = Instantiate(CornererPrefab, PlayerLeaningScript.CurrentLeanPlace.CornererSpawner.position, Quaternion.identity);
    }
    private void TryIncrease()
    {
        if(!isStopped)
        {
            Progress += RandomIncreasement();
        }
    }
    private void ResetRageModifier()
    {
        randomRageMod = Random.Range(1, 3);
    }

    private float RandomIncreasement()
    {
        return ((Time.deltaTime * Random.value * 10f) / calmMod) * randomRageMod + Random.Range(0f, 6f);
    }
}
