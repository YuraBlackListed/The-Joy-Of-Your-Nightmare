using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class LevelEndingScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> monsters;

    [SerializeField] private SceneFade sceneLoader;

    [SerializeField] private Image fade;

    [SerializeField] private GameObject cursor;

    [SerializeField] private Volume volume;

    FilmGrain filmGrain;
    float strength = 1;

    void Start()
    {
        volume.profile.TryGet<FilmGrain>(out filmGrain);
        filmGrain.intensity.Override(strength);
    }

    private void SwapToMenu()
    {
        sceneLoader.ActivateFade(0);
    }
    private void FadeScreen()
    {
        for(int i = 0; i < monsters.Count; i++)
        {
            monsters[i].SetActive(false);
        }
        AudioSystem.PlaySetSoundAt("LevelPast", AudioType.Effect);
        fade.DOFade(1f, 1f);
        Invoke(nameof(SwapToMenu), 1.5f);
    }
    public void EndLevel()
    {
        cursor.SetActive(false);
        filmGrain.intensity.Override(strength);
        Invoke(nameof(FadeScreen), 0.5f);
    }
}
