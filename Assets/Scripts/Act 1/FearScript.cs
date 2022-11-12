using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FearScript : MonoBehaviour
{
    [SerializeField] private LampScript lamp;

    [SerializeField] private Volume volume;

    FilmGrain filmGrain;

    private float strength = 0.03f;

    void Start()
    {
        volume.profile.TryGet<FilmGrain>(out filmGrain);
        filmGrain.intensity.Override(strength);
    }

    void FixedUpdate()
    {
        if(lamp.Active == false)
        {
            strength+=0.002f;
            filmGrain.intensity.Override(strength);
        }
    }
}