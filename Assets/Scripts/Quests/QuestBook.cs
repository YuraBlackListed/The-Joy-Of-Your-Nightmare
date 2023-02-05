using System.Collections.Generic;
using UnityEngine;

// This is front-end
public class QuestBook : MonoBehaviour
{
    public static QuestBook instance;

    [SerializeField] private QuestController QuestController;

    [SerializeField] private GameObject QuestWindowPrefab;
    [SerializeField] private GameObject QuestBookGameObject;

    [SerializeField] private CameraXController CameraX;
    [SerializeField] private CameraYController CameraY;

    [SerializeField] private Transform QuestParent;

    [SerializeField] private KeyCode BookInteractionButton;

    private Dictionary<string, GameObject> questWindows = new Dictionary<string, GameObject>();

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
            if (!instance.questWindows.ContainsKey(quest.Title))
            {
                instance.CreateNewQuestWindow(quest);
            }
        }
    }
    public static void DeleteQuestWindow(string questName)
    {
        Destroy(instance.questWindows[questName]);

        instance.questWindows.Remove(questName);
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

        questWindows.Add(quest.Title, newQuest);

        var questWindowScript = newQuest.GetComponent<QuestWindowScript>();

        questWindowScript.InitQuestWindow(quest.Title, quest.Description, quest.Type);
    }

}
