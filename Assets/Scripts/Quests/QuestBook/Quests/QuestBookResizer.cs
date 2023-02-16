using UnityEngine;

public class QuestBookResizer : MonoBehaviour
{
    [SerializeField] private RectTransform QuestParentTransfrom;

    private void Update()
    {
        if(QuestBook.instance.QuestWindows.Count <= 4)
        {
            QuestParentTransfrom.sizeDelta = new Vector2(QuestParentTransfrom.rect.width, 450);

            return;
        }

        int height = Mathf.RoundToInt(QuestBook.instance.QuestWindows.Count * 112.5f);

        QuestParentTransfrom.sizeDelta = new Vector2(QuestParentTransfrom.rect.width, height);
    }
}
