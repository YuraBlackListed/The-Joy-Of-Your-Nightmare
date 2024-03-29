using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public static TimeCounter instance;
    public float GameTime { get; private set; }

    [SerializeField] private  GameObject text;
    [SerializeField] private LevelEndingScript EndingScript;
    [SerializeField] private GameObject[] mosters;

    public KeyCode InteractButton = KeyCode.H;

    private bool start = true;

    private void Awake()
    {
        instance = this;
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

            if(GameTime >= 21600 /*|| Input.GetKeyDown(InteractButton)*/)
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
            if(GameTime >= 12960 || (PlayerPrefs.GetFloat("MonstersLevel") >= 1 && GameTime >= 6480))
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
