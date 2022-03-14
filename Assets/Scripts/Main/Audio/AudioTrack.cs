using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //list of audios on the SAME TRACK
public class AudioTrack : MonoBehaviour
{
    public AudioSource source;
    public AudioObject[] audioList;

    private void Awake()
    {
        source = gameObject.GetComponent<AudioSource>();
    }
}
