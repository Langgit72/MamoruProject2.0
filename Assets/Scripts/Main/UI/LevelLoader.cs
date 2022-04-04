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

    public Image kamon;
    public TMP_Text tip;
    public TMP_Text title;

    public Sprite[] kamons;
    public LoadTip[] tips;

    LoadTip newTip;
    Sprite newKamon;

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
        sceneTable.Add(SceneType.Village, 0);
        sceneTable.Add(SceneType.Caves,1);

        Debug.Log("initialized");

    }

    private void Start()
    {

        newTip = Player.instance.tip;
        newKamon = Player.instance.kamon;
        tip.text = newTip.tip;
        title.text = newTip.title;
        kamon.sprite = newKamon;
    }

    private void Update()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        activeScene = (SceneType)sceneTable[sceneIndex];
    }



    public void LoadNextLevel() {
        int tipInt = Random.Range(0,tips.Length);
        int spriteInt = Random.Range(0, kamons.Length);
        newTip = tips[tipInt];
        newKamon = kamons[spriteInt];
        Player.instance.tip = newTip;
        Player.instance.kamon = newKamon;

        tip.text = newTip.tip;
        title.text = newTip.title;
        kamon.sprite = newKamon;


        UpdateIndex();
        StartCoroutine(LoadLevel(sceneIndex));
        Debug.Log("switching to"+(SceneType)sceneTable[SceneManager.GetActiveScene().buildIndex]);
    }

    public void LoadScene(LevelLoader.SceneType scene)
    {

        Debug.Log("loading scene" +scene+ ","+ (int)sceneTable[scene]);
        int m_Scene = (int)sceneTable[scene];
        StartCoroutine(LoadLevel(m_Scene));

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
