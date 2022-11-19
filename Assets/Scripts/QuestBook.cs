using System.Collections.Generic;
using UnityEngine;

public class QuestBook : MonoBehaviour
{
    [SerializeField] private JournalScript Journal;

    [SerializeField] private GameObject QuestWindowPrefab;

    [SerializeField] private Transform QuestParent;

    private Dictionary<string, GameObject> questWindows = new Dictionary<string, GameObject>();

    private void Start()
    {
        ReorganizeQuestBook();
    }

    //Use on deletion or on addition of new Quest
    public void ReorganizeQuestBook()
    {
        foreach(Quest quest in Journal.Quests.Values)
        {
            if (!questWindows.ContainsKey(quest.Title))
            {
                CreateNewQuestWindow(quest);
            }
        }
    }
    private void CreateNewQuestWindow(Quest quest)
    {
        //Use grid on QuestParent Gameobject
        GameObject newQuest = Instantiate(QuestWindowPrefab, transform.position, Quaternion.identity, QuestParent);

        newQuest.GetComponent<QuestWindowScript>().CreateQuestWindow(quest.Title, quest.Description, quest.Type);
    }
}
