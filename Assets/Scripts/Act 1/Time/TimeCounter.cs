using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public static TimeCounter instance;
    public float GameTime { get; private set; }

    [SerializeField] private LevelEndingScript EndingScript;
    [SerializeField] private LevelScriptableObject levelScrObj;
    [SerializeField] private GameObject[] mosters;
    public KeyCode InteractButton = KeyCode.H;

    private void Awake()
    {
        instance = this;
    }
    
    private void Update()
    {
        GameTime = Mathf.Clamp(GameTime, 0f, 21600f);

        GameTime += Time.deltaTime * 72f;

        if(GameTime >= 21600 || Input.GetKeyDown(InteractButton))
        {
            EndingScript.EndLevel();
        }

        if(GameTime >= 4320)
        {
            mosters[0].SetActive(true);
        }
        if(GameTime >= 12960 || (levelScrObj.Night < 1 && GameTime >= 2320))
        {
            mosters[2].SetActive(true);
        }
        if(GameTime >= 6480)
        {
            mosters[1].SetActive(true);
        }
    }
    public void DeleteTime(float time)
    {
        GameTime -= time;
    }
}
