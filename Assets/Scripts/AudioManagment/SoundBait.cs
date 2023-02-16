using UnityEngine;

public class SoundBait : MonoBehaviour
{
    [SerializeField] private LayerMask MonsterLayer;

    public void SearchForMonster(float radius)
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, radius, MonsterLayer);

        foreach (var target in targets)
        {
            target.GetComponent<RunnerSoundLogic>().ReactToSound(transform);   
        }
    }
}
