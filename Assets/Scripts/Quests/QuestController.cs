using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public Dictionary<string, Quest> Quests { get; private set; } = new Dictionary<string, Quest>();

    public Dictionary<string, NotePrefab> Notes { get; private set; } = new Dictionary<string, NotePrefab>();

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
        Dictionary<string, QuestType> questsForDeletion = new Dictionary<string, QuestType>();

        //Check for finished quests
        foreach(Quest quest in Quests.Values)
        {
            if(quest.IsDone && quest.Type != QuestType.Tip)
            {
                questsForDeletion.Add(quest.Title, quest.Type);

                print("Quest is finished!");
            }
        }
        
        //Delete quests
        foreach(var title in questsForDeletion.Keys)
        {
            QuestBook.DeleteQuestWindow(title);

            Quests.Remove(title);

            print("Quest is deleted!");
        }

        QuestBook.ReorganizeQuestBook();

        questsForDeletion.Clear();
    }
    private void AddQuest(Quest quest)
    {
        if(Quests.ContainsKey(quest.Title))
        {
            return;
        }

        Quests.Add(quest.Title, quest);

        print("Quest created!");

        QuestBook.ReorganizeQuestBook();
    }
    public static void CreateQuest(QuestPrefab questPrefab)
    {
        Quest quest = new Quest(questPrefab);

        print("Quest created!");

        instance.AddQuest(quest);
    }
    public static void AddNote(NotePrefab prefab)
    {
        if(instance.Notes.ContainsKey(prefab.NoteName))
        {
            Debug.LogWarning("Already has such note (Possible double take bug)");

            return;
        }

        instance.Notes.Add(prefab.NoteName, prefab);

        QuestBook.ReorganizeQuestBook();
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
