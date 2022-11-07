using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public float GameTime { get; private set; }

    void Update()
    {
        GameTime += Time.deltaTime * 24f;

        if(GameTime >= 21600)
        {
            Debug.Log("The night has ended");
        }
    }
}
