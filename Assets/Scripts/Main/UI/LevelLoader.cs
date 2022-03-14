using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public int sceneIndex;
    public SceneType activeScene;
    public static LevelLoader instance;
    public float transitionTime=1;

    public Animator transition;

    [System.Serializable]
    public enum SceneType
    {
        None,
        Village,
        Caves
    }
    public Hashtable sceneTable;

    private void Awake()
    {
        #region Singleton instance
        if (instance == null)
        {
            instance = this; //if there is no current inventory instance in game, occupy singleton with current inventory class
        }
        else
        {
            Debug.LogWarning("More than one instance"); // otherwise there is already an inventory instance in game, and no more can be created
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        #endregion

        sceneTable = new Hashtable();
       sceneTable.Add(0, SceneType.Caves);
       sceneTable.Add(1, SceneType.Village);
        Debug.Log("initialized");
    }

    private void Update()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        activeScene = (SceneType)sceneTable[sceneIndex];
    }



    public void LoadNextLevel() {
        UpdateIndex();
        StartCoroutine(LoadLevel(sceneIndex));
        Debug.Log("switching to"+(SceneType)sceneTable[SceneManager.GetActiveScene().buildIndex]);
    }


    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetBool("Fade In",true);
        transition.SetBool("Fade Out", false);
        yield return new WaitForSeconds(transitionTime);
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        while (!operation.isDone)
        {
            Debug.Log("progress"+operation.progress);
            yield return null;
        }
        if (operation.isDone)
        {
            yield return new WaitForSeconds(1);
            transition.SetBool("Fade In", false);
            transition.SetBool("Fade Out", true);
        }
        //load scene

    }

    public void UpdateIndex()
    {
        if (sceneIndex == SceneManager.sceneCount)
        {
            sceneIndex = 0;
        }
        else
        {
            sceneIndex += 1;
        }
    }


}
