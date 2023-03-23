using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private LoadingScreenScript LoadingScreen;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("NightNumber"))
        {
            PlayerPrefs.SetInt("NightNumber", 1);
        }
        if(!PlayerPrefs.HasKey("MonstersLevel"))
        {
            PlayerPrefs.SetFloat("MonstersLevel", 0.1f);
        }
    }
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }
    
    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        LoadingScreen.ActivateLoading();

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            yield return null;
        }
    }
}
