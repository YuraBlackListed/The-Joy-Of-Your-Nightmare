using System.Collections.Generic;
using UnityEngine;

// This is front-end
public class QuestBook : MonoBehaviour
{
    public static QuestBook instance;

    public Dictionary<string, GameObject> QuestWindows { get; private set; } = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> Notes { get; private set; } = new Dictionary<string, GameObject>();

    [SerializeField] private QuestController QuestController;

    [SerializeField] private GameObject QuestWindowPrefab;
    [SerializeField] private GameObject NotePrefab;
    [SerializeField] private GameObject QuestBookGameObject;

    [SerializeField] private CameraXController CameraX;
    [SerializeField] private CameraYController CameraY;

    [SerializeField] private Transform QuestParent;
    [SerializeField] private Transform NoteParent;

    [SerializeField] private KeyCode BookInteractionButton;

    private bool bookIsOpened = false;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ReorganizeQuestBook();
    }
    private void Update()
    {
        if (Input.GetKeyDown(BookInteractionButton)) BookInteract();
    }

    //Use on deletion or on addition of new Quest
    public static void ReorganizeQuestBook()
    {
        foreach(Quest quest in instance.QuestController.Quests.Values)
        {
            if (!instance.QuestWindows.ContainsKey(quest.Title))
            {
                instance.CreateNewQuestWindow(quest);
            }
        }

        foreach(NotePrefab note in instance.QuestController.Notes.Values)
        {
            if(!instance.Notes.ContainsKey(note.NoteName))
            {
                instance.CreateNewNote(note);
            }
        }
    }
    public static void DeleteQuestWindow(string questName)
    {
        Destroy(instance.QuestWindows[questName]);

        instance.QuestWindows.Remove(questName);
    }
    private void BookInteract()
    {
        if (!bookIsOpened)
        {
            QuestBookGameObject.SetActive(true);

            CameraY.enabled = false;
            CameraX.enabled = false;

            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0f;
        }
        else
        {
            QuestBookGameObject.SetActive(false);

            CameraY.enabled = true;
            CameraX.enabled = true;

            Time.timeScale = 1f;
        }

        bookIsOpened = !bookIsOpened;
    }
    private void CreateNewQuestWindow(Quest quest)
    {
        //Use grid on QuestParent Gameobject
        GameObject newQuest = Instantiate(QuestWindowPrefab, QuestParent.position, QuestParent.rotation, QuestParent);

        QuestWindows.Add(quest.Title, newQuest);

        var questWindowScript = newQuest.GetComponent<QuestWindowScript>();

        questWindowScript.InitQuestWindow(quest.Title, quest.Description, quest.Type);
    }
    private void CreateNewNote(NotePrefab note)
    {
        GameObject newNote = Instantiate(NotePrefab, NoteParent.position, NoteParent.rotation, NoteParent);

        Notes.Add(note.NoteName, newNote);

        var noteWindowScript = newNote.GetComponent<NoteWindowScript>();

        noteWindowScript.InitNoteWindow(note.NoteName, note.NoteText);
    }
}
