using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField] private string QuestName;
    [SerializeField] private string ConditionName;

    private void OnTriggerEnter(Collider other)
    {
        var incomingObject = other.gameObject;

        if(incomingObject.tag == "Player")
        {
            TryFinishQuest();
        }
    }
    private void TryFinishQuest()
    {
        print("Trigger enter");

        QuestController.DoCondition(ConditionName, QuestName);

        Destroy(gameObject, 0.1f);
    }
}
