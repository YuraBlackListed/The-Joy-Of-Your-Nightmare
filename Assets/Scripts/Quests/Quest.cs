using System.Collections.Generic;
public enum QuestType
{ 
    MainQuest,
    SideQuest,
    Tip
}

public class Quest
{
    public Dictionary<string, bool> Conditions = new Dictionary<string, bool>();
    public bool IsDone { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public QuestType Type { get; private set; }

    public Quest(QuestPrefab prefab)
    {
        Title = prefab.Title;
        Description = prefab.Description;
        Type = prefab.Type;

        foreach (var condition in prefab.ConditionNamesList)
        {
            Conditions.Add(condition, false);
        }
    }
    public void FinishQuest()
    {
        IsDone = true;
    }
    public bool CheckConditions(Quest quest)
    {
        foreach(var condition in quest.Conditions)
        {
            if(!condition.Value)
            {
                return false;
            }
        }

        return true;
    }
}
