using UnityEngine;

public class NoteResizer : MonoBehaviour
{
    [SerializeField] private RectTransform NoteParent;

    void Update()
    {
        if (QuestBook.instance.Notes.Count <= 1)
        {
            NoteParent.sizeDelta = new Vector2(NoteParent.sizeDelta.x, 450);

            return;
        }

        int height = Mathf.RoundToInt(QuestBook.instance.Notes.Count * 450);

        NoteParent.sizeDelta = new Vector2(NoteParent.sizeDelta.x, height);
    }
}
