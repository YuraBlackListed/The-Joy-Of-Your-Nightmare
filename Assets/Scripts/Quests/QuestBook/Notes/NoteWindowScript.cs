using UnityEngine;
using TMPro;

public class NoteWindowScript : MonoBehaviour
{
    [SerializeField] private TMP_Text ThisTitle;
    [SerializeField] private TMP_Text ThisDescription;

    public void InitNoteWindow(string title, string desc)
    {
        ThisTitle.text = title;
        ThisDescription.text = desc;
    }
}
