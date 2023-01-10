using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public static TimeCounter instance;
    public float GameTime { get; private set; }

    [SerializeField] private LevelEndingScript EndingScript;
    public KeyCode InteractButton = KeyCode.H;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        GameTime = Mathf.Clamp(GameTime, 0f, 21600f);

        GameTime += Time.deltaTime * 24f;

        if(GameTime >= 21600 || Input.GetKeyDown(InteractButton))
        {
            EndingScript.EndLevel();
        }
    }
    public void DeleteTime(float time)
    {
        GameTime -= time;
    }
}
