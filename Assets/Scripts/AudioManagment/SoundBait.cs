using UnityEngine;

public class SoundBait : MonoBehaviour
{
    public void SearchForMonster(float radius)
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, radius);

        foreach (var target in targets)
        {
            if(target.gameObject.tag == "Runner")
            {
                target.GetComponent<RunnerSoundLogic>().ReactToSound(transform);
            }    
        }
    }
}
