using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    [SerializeField] LevelChanger levelManager;
    private Image fade;
    void Start()
    {
        fade = gameObject.GetComponent<Image>();
        fade.DOFade(0f, 0.7f);
    }
    public void ActivateFade(int id)
    {
        fade.DOFade(1f, 0.7f).OnComplete(()=>levelManager.LoadLevel(id));
    }
}