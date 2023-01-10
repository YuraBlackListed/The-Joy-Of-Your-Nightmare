using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LevelEndingScript : MonoBehaviour
{
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
