using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public ParticleSystem rain;
    int time;
    float intensity;
    float dropOff;
    public float rainLength = 100;
    float multiplier = 3.1415f * 2f;

    // Start is called before the first frame update
    void Start()
    {
        intensity = 0.5f;
        dropOff = 2;
        AudioController.instance.LoopAudio(AudioType.WT_01);
    }

    // Update is called once per frame
    void Update()
    {
       time += 1;
       float output = ((Mathf.Sqrt((1 + Mathf.Pow(dropOff, 2) / (1 + (Mathf.Pow(dropOff, 2) * Mathf.Pow(Mathf.Cos(time * 1 / rainLength * multiplier), 2))))) * Mathf.Cos(time * 1 / rainLength * multiplier)) * intensity) + intensity;
       var emission = rain.emission;
       emission.rateOverTime = output*300f;
       AudioTrack rainTrack = (AudioTrack)AudioController.instance.audioTable[AudioType.WT_01];
       rainTrack.source.volume = output;
    }


}
