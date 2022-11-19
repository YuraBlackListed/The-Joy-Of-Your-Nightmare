using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenScript : MonoBehaviour
{
    [SerializeField] private GameObject LoadingScreenMainMenu;
    [SerializeField] private GameObject LoadingScreenAct1;
    [SerializeField] private GameObject LoadingScreenAct2;
    [SerializeField] private GameObject LoadingScreenAct3;
    [SerializeField] private GameObject LoadingBarGameObject;

    private GameObject currentLoadingBackground;

    private Image loadingBar;

    private void Start()
    {
        loadingBar = LoadingBarGameObject.GetComponent<Image>();
    }
    public void ChooseLoadingBackground(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 0:
                currentLoadingBackground = LoadingScreenMainMenu;
                break;
            case 1:
                currentLoadingBackground = LoadingScreenAct1;
                break;
            case 2:
                currentLoadingBackground = LoadingScreenAct2;
                break;
            case 3:
                currentLoadingBackground = LoadingScreenAct3;
                break;
        }
    }
    public void ActivateLoading()
    {
        currentLoadingBackground.SetActive(true);
        LoadingBarGameObject.SetActive(true);
    }
    public void UpdateProgress(float progress)
    {
        loadingBar.fillAmount = progress;
    }
}
