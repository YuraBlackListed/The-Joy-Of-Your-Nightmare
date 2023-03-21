using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LevelEndingScript : MonoBehaviour
{
    [SerializeField] private SceneFade sceneLoader;

    [SerializeField] private RawImage fade;

    [SerializeField] private GameObject cursor;

    [SerializeField] private Volume volume;
    
    [SerializeField] private LevelScriptableObject levelScrObj;

    FilmGrain filmGrain;

    private bool ended = false;
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
        if(!ended)
        {
            levelScrObj.Night++;
            levelScrObj.EnemiesLevel = 1;
            cursor.SetActive(false);
            filmGrain.intensity.Override(strength);
            Invoke(nameof(FadeScreen), 0.5f);
            ended = true;
        }
    }
}
