using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class LevelLoader : MonoBehaviour
{
    public int sceneIndex;
    public SceneType activeScene;
    public static LevelLoader instance;
    public float transitionTime=1;

    public Image icon;
    public TMP_Text tip;
    public TMP_Text title;

    public LoadTip[] tips;

    LoadTip newTip;

    public Animator transition;

    [System.Serializable]
    public enum SceneType
    {
        None,
        Village,
        Sample,
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
        sceneTable.Add(SceneType.Village, 0);
        sceneTable.Add(SceneType.Sample, 1);
        sceneTable.Add(SceneType.Caves,2);

        Debug.Log("initialized");

    }

    private void Start()
    {

        newTip = Player.instance.tip;
        tip.text = newTip.tip;
        title.text = newTip.title;
        icon.sprite = newTip.icon;
    }

    private void Update()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        activeScene = (SceneType)sceneTable[sceneIndex];
    }



    public void LoadNextLevel() {
        int tipInt = Random.Range(0,tips.Length);
        newTip = tips[tipInt];
        Player.instance.tip = newTip;

        tip.text = newTip.tip;
        title.text = newTip.title;
        icon.sprite = newTip.icon;


        UpdateIndex();
        StartCoroutine(LoadLevel(sceneIndex));
        Debug.Log("switching to"+(SceneType)sceneTable[SceneManager.GetActiveScene().buildIndex]);
    }

    public void LoadScene(LevelLoader.SceneType scene)
    {
        int tipInt = Random.Range(0, tips.Length);
        newTip = tips[tipInt];
        Player.instance.tip = newTip;

        tip.text = newTip.tip;
        title.text = newTip.title;
        icon.sprite = newTip.icon;

        int m_Scene = (int)sceneTable[scene];
        StartCoroutine(LoadLevel(m_Scene));
        Debug.Log("loading scene" + scene + "," + (int)sceneTable[scene]);

    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetBool("Fade In",true);
        transition.SetBool("Fade Out", false);
        yield return new WaitForSeconds(transitionTime);
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        //operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            Debug.Log("Loading progress: " + (operation.progress * 100) + "%");
            yield return null;
        }
        if (operation.isDone)
        {
            yield return new WaitForSeconds(1);
            //operation.allowSceneActivation = true;
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
