using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public static TimeCounter instance;
    public float GameTime { get; private set; }

    [SerializeField] private LevelEndingScript EndingScript;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        GameTime = Mathf.Clamp(GameTime, 0f, 21600f);

        GameTime += Time.deltaTime * 24f;

        if(GameTime >= 21600)
        {
            EndingScript.EndLevel();
            Debug.Log("The night has ended");
        }
    }
    public void DeleteTime(float time)
    {
        GameTime -= time;
    }
}
