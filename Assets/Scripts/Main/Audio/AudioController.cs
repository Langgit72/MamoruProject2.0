using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour //AUDIO CONTROLLER
{
    // Audio Controller - Singleton that controls all audio in the game
    // Audio Track - Object that stores a list of audio objects
    // AudioType - Enum that contains ALL AUDIO IN GAME
    // AudioObject - Object that contains an AudioType with corresponding Audio Clip Asset
    // Audio Action - Enum that contains all forms of running audio in game i.e start stop loop etc...
    // Audio Table permanent dictionary that links audio types to the tracks they are playing on
    // Job Table variable dictionary that links running job types to the jobRunners playing them
    // Audio Job Object that contains an AudioType to be run, and an Audio Action to run Type as

    public static AudioController instance;

    public AudioTrack[] tracks; //all tracks within game
  
    public Hashtable audioTable;
    public Hashtable jobTable;

    #region Unity Methods
    void Awake()
    {
        #region Singleton Instance
        if (instance == null)
        {
            instance = this;
        } //Define Singleton Instance
        else
        {
            Debug.LogWarning("More than one instance");
            return;
        }
        DontDestroyOnLoad(gameObject);
        #endregion

        audioTable = new Hashtable();
        jobTable = new Hashtable();
        Populate(); //permanently fill the audioTable
    }
    #endregion

    #region Methods

    void Populate() //link every single audio object to its track through a table
    {
        foreach(AudioTrack i in tracks) //for every track in the Audio Controller
        {
            foreach(AudioObject j in i.audioList)  // for every audio object within that track
            {
                //do not duplicate keys
                if (!audioTable.Contains(j.type))
                {
                    audioTable.Add(j.type, i); //add a link between the audio type and the track containing it
                }
                else
                {
                    Debug.Log("duplicate register: "+j.type); // audio type is contained within two of the same tracks
                }
        
            }
        }
    }

    #region JobManagement
    void AddJob(AudioJob job) //Begin Audio Job
    {
        // deal with conflicts
        RemoveConflictingJobs(job.m_type);

        //start job
        IEnumerator runningJob = RunAudioJob(job); //return proper Coroutine
        StartCoroutine(runningJob);
        Debug.Log("Running job " + job.m_action +" "+ job.m_type + " with coroutine " + runningJob);
        jobTable.Add(job.m_type, runningJob);
    }

    void RemoveJob(AudioType type) //Add running job to table
    {
        if (!jobTable.ContainsKey(type))
        {
            Debug.Log("Job doesn't exist in table");
        }
        else
        {
            IEnumerator runningJob = (IEnumerator)jobTable[type];
            StopCoroutine(runningJob);  //stop the coroutine running in job;
            jobTable.Remove(type);// remove job from table of running jobs
        }
    }

    void RemoveConflictingJobs(AudioType type)
    {
        if (jobTable.Contains(type)) //If this AudioType is already running, easy, override it
        {
            Debug.Log("Already running" + type + "removing");
            RemoveJob(type);
        }

        // Check to see if a different AudioType on the same track is running

        AudioType conflictAudio = AudioType.None;
        AudioTrack trackNeeded = (AudioTrack)audioTable[type]; // AudioTrack associated with this AudioJob's Audio Type

        foreach (DictionaryEntry entry in jobTable) //loop through every running job
        {
            AudioType entryAudio = (AudioType)entry.Key; //AudioType Audiojob is running
            AudioTrack trackInUse = (AudioTrack)audioTable[entryAudio]; //AudioTrack associated with current AudioJob's Audio Type
            if (trackInUse == trackNeeded)
            {
                conflictAudio = entryAudio; // if the track needed is the entry AudioType's current track
            }
 
        }
        if (conflictAudio != AudioType.None)
        {
            RemoveJob(conflictAudio); //then the entry AudioType needs to be removed from the table
        }

    }

    public AudioClip GetAudioClipOfTypeFromTrack(AudioTrack _track, AudioType _type)
    {
        AudioClip returnClip = null;
        foreach (AudioObject _object in _track.audioList) //for every type in the Audio Track
        {
            if (_object.type == _type) 
            {
                returnClip= _object.clip; //return clip from specified track
            }
        }
        if (returnClip == null)
        {
            Debug.Log("AudioType " + _type + " is not contained with AudioTrack" + _track);
        }
        return returnClip;
    }

    IEnumerator RunAudioJob(AudioJob job) //based on the Action Type of the Job to run return the proper coroutine
    {
        AudioTrack runTrack = (AudioTrack)audioTable[job.m_type];
        AudioSource runSource = runTrack.source;
        AudioClip newClip = GetAudioClipOfTypeFromTrack(runTrack, job.m_type);
        runSource.clip = newClip;

        switch (job.m_action)
        {
            case AudioAction.START:
                runSource.loop = false;
                runSource.Play();
                break;
            case AudioAction.STOP:
                runSource.Stop();
                break;
            case AudioAction.LOOP:
                runSource.loop = true;
                runSource.Play();
                break;
        }
        yield return null;

    }
    #endregion

    #region AudioCommands
    public void PlayAudio(AudioType type)
    {
        Debug.Log("Playing" + type);
        AddJob(new AudioJob(AudioAction.START,type));
    }
    public void RestartAudio(AudioType type)
    {
        Debug.Log("Restarting" + type);
        AddJob(new AudioJob(AudioAction.RESTART, type));
    }
    public void StopAudio(AudioType type)
    {
        Debug.Log("Stoping" + type);
        AddJob(new AudioJob(AudioAction.STOP, type));
    }
    public void LoopAudio(AudioType type)
    {
        Debug.Log("Looping" + type);
        AddJob(new AudioJob(AudioAction.LOOP, type));
    }
    public void FadeInAudio(AudioType type)
    {
        Debug.Log("Fade In" + type);
        AddJob(new AudioJob(AudioAction.FADE_IN, type));
    }
    public void FadeOutAudio(AudioType type)
    {
        Debug.Log("Fade Out" + type);
        AddJob(new AudioJob(AudioAction.FADE_OUT, type));
    }
    #endregion

    #endregion

}
