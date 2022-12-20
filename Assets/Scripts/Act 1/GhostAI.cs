using UnityEngine;

public class GhostAI : MonoBehaviour
{
    public GhostSpawner ParentScript;
    public TimeCounter InGameTime;

    public bool PlayerLookingAtMe = false;

    private float ghostTimer = 7.5f;
    private float timeLeftToWatch = 25f;

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
        }

        if (timeLeftToWatch <= 0f)
        {
            Dissapear();
        }
    }
    private void Dissapear()
    {
        ParentScript.ResetGhost();
        Destroy(gameObject);
    }
    private void ReverseTime()
    {
        InGameTime.DeleteTime(3600f);

        ParentScript.ResetGhost();
        Destroy(gameObject);
    }
}
