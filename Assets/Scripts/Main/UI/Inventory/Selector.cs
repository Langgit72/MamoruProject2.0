using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


public class Selector : MonoBehaviour
{
    public Inventory.InventoryMode type;
    public Button itemButton;
    public Image m_image;
    public CanvasGroup canvasGroup;

    #region Unity Methods
    void Start()
    {
        Button btn = itemButton.GetComponent<Button>();
        m_image = gameObject.GetComponent<Image>();
        btn.onClick.AddListener(TaskOnClick);
 
    }

    private void Update()
    {
        if (Inventory.instance.display_mode == Inventory.InventoryMode.market)
        {
            Hide();
        }
        else {
           Show();
        }
    }
    #endregion

    #region Methods

    public void Hide()
    {
        canvasGroup.alpha = 0f; //this makes everything transparent
        canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
      
    }

    public void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    void TaskOnClick()
    {
        Inventory.instance.setMode(type);
        Inventory.instance.highlight = 0;  //reset highlight
        for (int i = 0; i < GameObject.Find("Type Selectors").gameObject.transform.childCount; i += 1) 
        {
            GameObject.Find("Type Selectors").gameObject.transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
       
        m_image.color = new Color(0.5f, 0.5f, 1, 1);// if selected color button darker
    }
    #endregion
}
