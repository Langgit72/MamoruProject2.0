using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour

{
    bool isActive;
    public CanvasGroup canvasGroup;
    public static Panel instance; // the single instance of inventory
    public InputManager m_input;

    #region Unity Methods
    void Awake()
    {
        #region Singleton Instance
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

        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    void Start()
    {
        m_input = InputManager.instance;
        Hide();
    }

    void Update()
    {

        
        if (m_input.inventoryInput)
        {
            if (isActive)
            {
                Hide();
                //Inventory.instance.set_mode = Inventory.InventoryMode.misc;
                isActive = !isActive;

            }
            else
            {
                Show();
                isActive = !isActive;


            }
        }
        

    }
    #endregion

    public void Hide()
    {
        canvasGroup.alpha = 0f; //this makes everything transparent
        canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
        Inventory.instance.setMode(Inventory.InventoryMode.misc);
        Inventory.instance.isHidden = true;
        Time.timeScale = 1;
    }
    public void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        Inventory.instance.isHidden = false;
        Time.timeScale = 0.6f;
    }

}
