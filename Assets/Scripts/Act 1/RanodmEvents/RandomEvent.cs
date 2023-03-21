using UnityEngine;

public class RandomEvent : MonoBehaviour
{
    [SerializeField] TimeCounter counter;
    private int eventTime;
    private bool done = false;
    private void Start() 
    {
        eventTime = Random.Range(1200, 21600);
    }
    
    private void Update()
    {
        if(!done)
        {
            if(counter.GameTime >= eventTime)
            {
                EventStart();
                done = true;   
            }
        }
    }
    protected virtual void EventStart()
    {

    }
    protected virtual void EventEnd()
    {
        
    }
}
