using UnityEngine;
using TMPro;

public class NightSnippet : MonoBehaviour
{
    [SerializeField] private LevelScriptableObject levelScrObj;

    [SerializeField] private TMP_Text nightText;

    private void Start()
    {
        nightText.SetText(levelScrObj.Night.ToString());
    }
}
