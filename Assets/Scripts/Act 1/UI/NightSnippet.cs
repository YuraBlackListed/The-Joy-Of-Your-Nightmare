using UnityEngine;
using TMPro;

public class NightSnippet : MonoBehaviour
{
    [SerializeField] private LevelScriptableObject levelScrObj;

    [SerializeField] private TMP_Text nightText;

    private void Start()
    {
        if(nightText != null)
        {
            nightText.SetText(levelScrObj.Night.ToString());
        }
    }

    public void NewGame()
    {
        if(levelScrObj != null)
        {
            levelScrObj.Night = 1;
            levelScrObj.EnemiesLevel = 0.1f;

        }
    }

    private void SelfOff()
    {
        gameObject.SetActive(false);
    }
}
