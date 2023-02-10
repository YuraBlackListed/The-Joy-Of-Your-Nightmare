using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public static TimeCounter instance;
    public float GameTime { get; private set; }

    [SerializeField] private  GameObject text;
    [SerializeField] private LevelEndingScript EndingScript;
    [SerializeField] private LevelScriptableObject levelScrObj;
    [SerializeField] private GameObject[] mosters;

    public KeyCode InteractButton = KeyCode.H;

    private bool start = false;

    private void Awake()
    {
        instance = this;
    }
    private void Start() 
    {
        Invoke(nameof(StartCountdown), 96f); 
    }
    public void StartCountdown()
    {
        text.SetActive(true);
        start = true;
    }
    private void Update()
    {
        if(start)
        {
            GameTime = Mathf.Clamp(GameTime, 0f, 21600f);

            GameTime += Time.deltaTime * 72f;

            if(GameTime >= 21600 || Input.GetKeyDown(InteractButton))
            {
                EndingScript.EndLevel();
            }

            if(GameTime >= 1200)
            {
                mosters[0].SetActive(true);
            }
            if(GameTime >= 6480)
            {
                mosters[1].SetActive(true);
            }
            if(GameTime >= 12960 || (levelScrObj.Night < 1 && GameTime >= 2320))
            {
                mosters[2].SetActive(true);
            }
        }
    }
    
    public void DeleteTime(float time)
    {
        GameTime -= time;
    }
}
