using UnityEngine;
using TMPro;

public class NightSnippet : MonoBehaviour
{
    [SerializeField] private TMP_Text nightText;

    private void Start()
    {
        if(nightText != null)
        {
            nightText.SetText(PlayerPrefs.GetInt("NightNumber").ToString());
        }
    }

    public void NewGame()
    {
        if(!PlayerPrefs.HasKey("NightNumber"))
        {
            PlayerPrefs.SetInt("NightNumber", 1);
            PlayerPrefs.SetFloat("MonstersLevel", 0.1f);
            PlayerPrefs.Save();
        }
    }

    private void SelfOff()
    {
        gameObject.SetActive(false);
    }
}
