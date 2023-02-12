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

    public NotePrefab MyNotePrefab;

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
        switch (Type)
        {
            case ItemType.Pill:
                Inventory.AddPill();
                break;
            case ItemType.Key:
                Inventory.AddItem(this);
                break;
            case ItemType.Note:
                QuestController.AddNote(MyNotePrefab);
                break;
        }

        if (DoQuestLogic)
        {
            Debug.Log("Quest updated");

            QuestController.DoCondition(ConditionName, QuestName);
        }

        Destroy(gameObject);
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
