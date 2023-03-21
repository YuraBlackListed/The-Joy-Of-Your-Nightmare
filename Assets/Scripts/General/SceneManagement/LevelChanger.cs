using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private LoadingScreenScript LoadingScreen;

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
