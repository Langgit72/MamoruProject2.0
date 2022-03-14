using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{
    public AudioController audioController;
    public AudioType type1;
    public AudioType type2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        /*
         * 
         * 
         * 
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(BattleTheme(type1, type2));
            
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            audioController.LoopAudio(AudioType.ST_03);
            audioController.LoopAudio(AudioType.WT_02);
        }
        */

        IEnumerator BattleTheme(AudioType introType,AudioType loopType)
        {
            audioController.PlayAudio(introType);
            AudioTrack introTrack = (AudioTrack)audioController.audioTable[introType];
            AudioClip introClip = audioController.GetAudioClipOfTypeFromTrack(introTrack, introType);
            yield return new WaitForSeconds(introClip.length);
            audioController.LoopAudio(loopType);
        }
 
    }
}
