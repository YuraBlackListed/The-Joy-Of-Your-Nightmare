using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    [SerializeField ]private Image fade;

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
        if(!ghostDelayed && !hasSpawned)
        {
            GhostProgress += GhostIncreasement();     
        }
    }
    private void CheckProgress()
    {
        if (GhostProgress >= 175f && !hasSpawned)
        {
            hasSpawned = true;
            fade.DOFade(1f, 0.3f).OnComplete(()=>Spawn());
        }
    }
    private void Spawn()
    {
        int randomPos = Random.Range(0, Positions.Length - 1);
        Transform ghostTransform = Positions[randomPos];

        GameObject ghost = Instantiate(GhostPrefab, ghostTransform.position, ghostTransform.rotation, gameObject.transform);
        ghost.transform.parent = ghostTransform;

        ghost.GetComponent<GhostAI>().blackout = fade;

        ghost.GetComponent<GhostAI>().ParentScript = this;
        ghost.GetComponent<GhostAI>().InGameTime = time;

        GhostProgress = 0f;

        fade.DOFade(0f, 0.3f);
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
        return (time.GameTime / 1500) * Time.deltaTime * Random.Range(1f, 5f);
    }
    public void ResetGhost()
    {
        GhostProgress = 0;

        chanceToBlock -= 0.1f;

        hasSpawned = false;

        TryBlock();

    }
}
