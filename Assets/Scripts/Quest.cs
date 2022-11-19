using System.Collections.Generic;
public enum QuestType
{ 
    MainQuest,
    SideQuest,
    Tip
}

public class Quest
{
    public List<bool> Conditions = new List<bool>();
    public bool IsDone { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public QuestType Type { get; private set; }

    public Quest(QuestType type, string title, string desc, int countOfConditions)
    {
        Title = title;
        Description = desc;
        Type = type;

        for(int i = 0; i < countOfConditions; i++)
        {
            Conditions.Add(false);
        }
    }
    public void FinishQuest()
    {
        IsDone = true;
    }
    public bool CheckConditions()
    {
        foreach(bool cond in Conditions)
        {
            if(!cond)
            {
                return false;
            }
        }

        return true;
    }
}
