using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioObject : MonoBehaviour //SINGLE AUDIO CLIP WITH ATTACHED AUDIO TYPE
{
    public AudioClip clip;
    public AudioType type;
}
