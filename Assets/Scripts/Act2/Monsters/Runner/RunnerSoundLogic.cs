using UnityEngine;

public class RunnerSoundLogic : MonoBehaviour
{
    [SerializeField] private RunnerMovement MonsterMovement;

    public void ReactToSound(Transform place)
    {
        MonsterMovement.SetSoundPlace(place);
    }
}
