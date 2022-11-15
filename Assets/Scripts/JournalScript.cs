using System.Collections.Generic;
using UnityEngine;

public class JournalScript : MonoBehaviour
{
    public Dictionary<string, Quest> Quests = new Dictionary<string, Quest>();

    [SerializeField] private QuestBook Book;

    private void Update()
    {
        CheckTasks();

        ClearJournal();
    }
    private void CheckTasks()
    {
        foreach (Quest quest in Quests.Values)
        {
            if(quest.CheckConditions())
            {
                Quests[quest.Title].FinishQuest();
            }
        }
    }
    private void ClearJournal()
    {
        foreach(Quest quest in Quests.Values)
        {
            if(quest.IsDone && quest.Type != QuestType.Tip)
            {
                Debug.Log(quest.Title + " is finished");
            }    
        }
    }
    //Template is for telling how this system can work (Can be erased)
    #region Template
    public void CreateExperimentalQuest()
    {
        Quest quest = new Quest(QuestType.SideQuest, "Run", "Do some run thing", 2);

        AddQuest(quest);
    }
    public void DoConditions()
    {
        DoCondition(0, "Run");
        DoCondition(1, "Run");
    }
    public void DoCondition(int indexOfCondition, string questName)
    {
        Quests[questName].Conditions[indexOfCondition] = true;
    }
    #endregion
    public void AddQuest(Quest quest)
    {
        Quests.Add(quest.Title, quest);

        Book.ReorganizeQuestBook();
    }
}
