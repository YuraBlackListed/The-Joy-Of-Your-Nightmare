using UnityEngine;

public class WindowStages : MonoBehaviour
{
    [SerializeField] private WindowMonsterAI AI;

    [SerializeField] private Transform Stage0;
    [SerializeField] private Transform Stage1;
    [SerializeField] private Transform Stage2;
    [SerializeField] private Transform Stage3;

    [SerializeField] private GameObject MyhtmareTransform;

    [SerializeField] private Animator Animator;

    private int level;

    private void Update()
    {
        GetLevel();

        SetPosition();

        Animator.SetInteger("StageLevel", level);
    }
    private void GetLevel()
    {
        if(AI.Progress >= AI.ProgressLimit * 0.9f)
        {
            level = 3;
            return;
        }
        if(AI.Progress >= AI.ProgressLimit * 0.45f)
        {
            level = 2;
            return;
        }
        if(AI.Progress >= AI.ProgressLimit * 0.175f)
        {
            level = 1;
            return;
        }

        level = 0;
    }
    private void SetPosition()
    {
        switch (level)
        {
            case 0:
                MyhtmareTransform.transform.position = Stage0.position;
                break;
            case 1:
                MyhtmareTransform.transform.position = Stage1.position;
                break;
            case 2:
                MyhtmareTransform.transform.position = Stage2.position;
                break;
            case 3:
                MyhtmareTransform.transform.position = Stage3.position;
                break;
        }
    }
}
