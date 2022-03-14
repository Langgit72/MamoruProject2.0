using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioJob: MonoBehaviour //Each instance defines a running job in game
{
    public AudioAction m_action; //how to play audio
    public AudioType m_type; // what audio to play

    public AudioJob(AudioAction action, AudioType type)
    {
        m_action = action;
        m_type = type;
    }

}
