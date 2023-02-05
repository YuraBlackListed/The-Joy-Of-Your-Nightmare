using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public Dictionary<string, Quest> Quests = new Dictionary<string, Quest>();

    public static QuestController instance;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        CheckTasks();

        ClearJournal();
    }
    private void CheckTasks()
    {
        foreach (var quest in Quests.Values)
        {
            if(quest.CheckConditions(quest))
            {
                Quests[quest.Title].FinishQuest();
            }
        }
    }
    private void ClearJournal()
    {
        Queue<string> questsFordeletion = new Queue<string>();

        //Check for finished quests
        foreach(Quest quest in Quests.Values)
        {
            if(quest.IsDone && quest.Type != QuestType.Tip)
            {
                questsFordeletion.Enqueue(quest.Title);

                print("Quest is finished!");
            }
        }

        //Delete them
        for (int i = 0; i < questsFordeletion.Count; i++)
        {
            string title = questsFordeletion.Dequeue();

            QuestBook.DeleteQuestWindow(title);

            Quests.Remove(title);

            print("Quest is deleted!");
        }

        QuestBook.ReorganizeQuestBook();

        questsFordeletion.Clear();
    }
    private void AddQuest(Quest quest)
    {
        if(Quests.ContainsKey(quest.Title))
        {
            return;
        }

        Quests.Add(quest.Title, quest);

        print("Quest added!");

        QuestBook.ReorganizeQuestBook();
    }
    public static void CreateQuest(QuestPrefab questPrefab)
    {
        Quest quest = new Quest(questPrefab);

        print("Quest created!");

        instance.AddQuest(quest);
    }
    public static void DoCondition(string conditionName, string questName)
    {
        instance.Quests[questName].Conditions[conditionName] = true;
    }
    public static bool ContainsQuest(string questTitle)
    {
        return instance.Quests.ContainsKey(questTitle);
    }
}
