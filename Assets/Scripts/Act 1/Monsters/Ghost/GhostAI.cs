using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GhostAI : MonoBehaviour
{
    public GhostSpawner ParentScript;
    public TimeCounter InGameTime;

    public bool PlayerLookingAtMe = false;

    private float ghostTimer = 7.5f;
    private float timeLeftToWatch = 3f;

    public Image blackout;

    private void Update()
    {
        CheckTime();

        if(!PlayerLookingAtMe)
        {
            ghostTimer -= Time.deltaTime;
        }
        else
        {
            timeLeftToWatch -= Time.deltaTime;
        }
    }
    
    private void CheckTime()
    {
        if (ghostTimer <= 0f)
        {
            ReverseTime();
            blackout.DOFade(1f, 0.3f).OnComplete(()=>ReverseTime());
        }

        if (timeLeftToWatch <= 0f)
        {
            blackout.DOFade(1f, 0.3f).OnComplete(()=>Dissapear());
        }
    }
    private void Dissapear()
    {
        ParentScript.ResetGhost();
        Destroy(gameObject);
        blackout.DOFade(0f, 0.3f);
    }
    private void ReverseTime()
    {
        InGameTime.DeleteTime(3600f);

        ParentScript.ResetGhost();
        blackout.DOFade(0f, 0.3f);
        Destroy(gameObject);
    }
}
