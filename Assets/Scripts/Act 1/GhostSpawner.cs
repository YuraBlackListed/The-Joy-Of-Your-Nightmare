using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public float GhostProgress;

    [SerializeField] private Transform[] Positions;

    [SerializeField] private GameObject GhostPrefab;

    private bool hasSpawned = false;
    private bool ghostDelayed = false;

    private TimeCounter time;

    private float chanceToBlock = 0.8f;
    private float ghostDelay = 5f;

    private void Start()
    {
        TryBlock();

        time = TimeCounter.instance;
    }
    private void Update()
    {
        TryIncreaseProgress();

        GhostProgress = Mathf.Clamp(GhostProgress, 0f, 300f);

        CheckProgress();
    }
    private void TryIncreaseProgress()
    {
        if(!ghostDelayed)
        {
            GhostProgress += GhostIncreasement();     
        }
    }
    private void CheckProgress()
    {
        if (GhostProgress >= 175f && !hasSpawned)
        {
            hasSpawned = true;

            Spawn();
        }
    }
    private void Spawn()
    {
        Transform ghostTransform = Positions[Random.Range(0, Positions.Length - 1)];

        GameObject ghost = Instantiate(GhostPrefab, ghostTransform.position, ghostTransform.rotation, gameObject.transform);

        ghost.GetComponent<GhostAI>().ParentScript = this;
        ghost.GetComponent<GhostAI>().InGameTime = time;
    }
    private void TryBlock()
    {
        float chance = Random.value;

        if(chance <= chanceToBlock)
        {
            ghostDelayed = true;

            Invoke(nameof(StartGhost), ghostDelay);
        }
    }
    private void StartGhost()
    {
        ghostDelayed = false;
    }
    private float GhostIncreasement()
    {
        return (time.GameTime / 1500) * Time.deltaTime * Random.Range(1f, 15f);
    }
    public void ResetGhost()
    {
        chanceToBlock -= 0.1f;

        GhostProgress = 0f;

        hasSpawned = false;

        TryBlock();

    }
}
