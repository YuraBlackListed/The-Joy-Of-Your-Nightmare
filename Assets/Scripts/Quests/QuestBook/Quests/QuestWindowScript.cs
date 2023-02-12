using UnityEngine;
using TMPro;

public class QuestWindowScript : MonoBehaviour
{
    [SerializeField] private TMP_Text ThisTitle;
    [SerializeField] private TMP_Text ThisDescription;

    [SerializeField] private GameObject MainQuestIcon;
    [SerializeField] private GameObject SideQuestIcon;
    [SerializeField] private GameObject TipIcon;

    public void InitQuestWindow(string title, string desc, QuestType type)
    {
        ThisTitle.text = title;
        ThisDescription.text = desc;

        SetIcon(type);
    }
    private void SetIcon(QuestType type)
    {
        switch (type)
        {
            case QuestType.MainQuest:
                MainQuestIcon.SetActive(true);
                break;
            case QuestType.SideQuest:
                SideQuestIcon.SetActive(true);
                break;
            case QuestType.Tip:
                TipIcon.SetActive(true);
                break;
        }
    }
}
