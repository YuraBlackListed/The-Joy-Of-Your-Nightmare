using UnityEngine;

public enum ItemType
{ 
    Key,
    Pill,
    Note,
}
public class Item : Interactable
{
    public string ItemName;

    public ItemType Type;

    public bool DoQuestLogic;
    public bool IsPickable = true;

    public string QuestName;
    public string ConditionName;

    public override void Interact()
    {
        if(!IsPickable)
        {
            return;
        }

        PickUp();
    }
    private void Update()
    {
        TryChangePickUpAbilitiy();
    }
    private void PickUp()
    {
        if (Type == ItemType.Pill)
        {
            Inventory.AddPill();
        }
        else
        {
            Inventory.AddItem(this);
        }

        if (DoQuestLogic)
        {
            Debug.Log("Quest updated");

            QuestController.DoCondition(ConditionName, QuestName);
        }

        Destroy(gameObject, 0.1f);
    }
    private void TryChangePickUpAbilitiy()
    {
        if (IsPickable)
        {
            return;
        }

        if(QuestController.ContainsQuest(QuestName))
        {
            IsPickable = true;
        }
    }
}
