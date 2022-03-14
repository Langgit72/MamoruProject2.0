using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomController : MonoBehaviour
{
    public static BottomController instance;
    public TMP_Text m_text;

    // Start is called before the first frame update
    void Awake()
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
        m_text = GetComponent<TMP_Text>();
        DontDestroyOnLoad(gameObject);
        #endregion

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LateUpdate()
    {
        if (DialougeManager.instance.isRunning)
        {
            m_text.text = "";
        }
    }
}
