using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenScript : MonoBehaviour
{
    private GameObject currentLoadingBackground;

    public void ActivateLoading()
    {
        currentLoadingBackground.SetActive(true);
    }
}
