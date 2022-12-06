using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public float GameTime { get; private set; }

    [SerializeField] private LevelEndingScript EndingScript;

    void Update()
    {
        GameTime += Time.deltaTime * 24f;

        if(GameTime >= 21600)
        {
            EndingScript.EndLevel();
            Debug.Log("The night has ended");
        }
    }
}
