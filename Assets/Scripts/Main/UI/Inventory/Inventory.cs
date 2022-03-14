using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class Inventory : MonoBehaviour
{

    public enum InventoryMode
    {
        None,
        misc,
        food,
        clothing,
        weapons,
        market,
    }

    public Hashtable textTable;

    public List<Item> items = new List<Item>(); //List of items stored in inventory
    public List<Item> foods = new List<Item>(); //List of items stored in inventory
    public List<Item> weapons = new List<Item>(); //List of items stored in inventory
    public List<Item> cloths = new List<Item>(); //List of items stored in inventory
    public List<Item> miscs = new List<Item>(); //List of items stored in inventory
    public List<Item> market = new List<Item>(); //List of items stored in inventory
   
    public int highlight = 0; // Current item being highlighted
    public bool isHidden; //if inventory is hidden
    public InventoryMode display_mode;

    public Sprite EmptyIcon; // Empty icon, should just be a blank sprite

    public static Inventory instance; // the single instance of inventory

    #region Unity Methods
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

        DontDestroyOnLoad(gameObject);
        #endregion

        textTable = new Hashtable();

        textTable.Add(InventoryMode.misc, "misc");
        textTable.Add(InventoryMode.food, "food");
        textTable.Add(InventoryMode.clothing, "clothing");
        textTable.Add(InventoryMode.weapons, "weapons");

        setMode(InventoryMode.misc);

        for (int i = 0; i < GameObject.Find("Slots").transform.childCount; i++)
        {
            GameObject.Find("Slots").gameObject.transform.GetChild(i).Find("Item Button").GetComponent<InventoryButtonManager>().inventoryTag = i; // inventory tag is how each use/remove button knows what item it corresponds to
            GameObject.Find("Slots").gameObject.transform.GetChild(i).Find("Remove Button").GetComponent<InventoryButtonManager>().inventoryTag = i;
        }

    }

    void Update() {

        setMode(display_mode);//set current mode of inventory

        #region Update Slots

        for (int i = 0; i < GameObject.Find("Slots").transform.childCount; i++)
        {  
            #region Slot apperance
            if (items.Count - 1 < i) // by default make all slots appear empty
            {
                GameObject.Find("Slots").gameObject.transform.GetChild(i).Find("Item Button").GetComponent<Image>().sprite = EmptyIcon;
            }

            if (i <= items.Count - 1)  //if slot has corresponding item
            {
                // Update the icon of the slot from empty with correspoindg item   
                GameObject.Find("Slots").gameObject.transform.GetChild(i).Find("Item Button").GetComponent<Image>().sprite = items[i].GetComponent<SpriteRenderer>().sprite;
            }
            #endregion

            #region Item Highlighting

            if (i == highlight) //if the slot is highlighted, show it in blue and display info
            {
                GameObject.Find("Slots").gameObject.transform.GetChild(i).Find("Image").GetComponent<Image>().color = new Color(0.56f, 0.56f, 1, 0.773f);

                if (items.Count > highlight) // if the highlight slot contains an item, update the Inventory item image, title, and info 
                {
                    GameObject.Find("Item Image").GetComponent<Image>().sprite = items[i].GetComponent<SpriteRenderer>().sprite; // current item image
                    GameObject.Find("Item Info").GetComponent<TMP_Text>().text = items[i].info; // current info
                    GameObject.Find("Item Title").GetComponent<TMP_Text>().text = items[i].title; // current item title
                }
                else // otherwise Inventory item image, title, and info should be blank
                {

                    GameObject.Find("Item Image").GetComponent<Image>().sprite = EmptyIcon; // blank sprite
                    GameObject.Find("Item Info").GetComponent<TMP_Text>().text = ""; // blank text
                    GameObject.Find("Item Title").GetComponent<TMP_Text>().text = ""; // blank text
                }


            }
            else //if the slot is NOT highlighted, color it normally
            {
                GameObject.Find("Slots").gameObject.transform.GetChild(i).Find("Image").GetComponent<Image>().color = new Color(0.56f, 0.56f, 0.56f, 0.773f);
            }

            #endregion

            //GameObject.Find("Slots").gameObject.transform.GetChild(i).Find("Item Button").GetComponent<InventoryButtonManager>().inventoryTag = i; // inventory tag is how each use/remove button knows what item it corresponds to
            //GameObject.Find("Slots").gameObject.transform.GetChild(i).Find("Remove Button").GetComponent<InventoryButtonManager>().inventoryTag = i;
        }
        #endregion

        if (display_mode != Inventory.InventoryMode.market)
        {
            GameObject.Find("Select Display").GetComponent<TMP_Text>().text = (string)textTable[display_mode]; //display current inventory type
        }

        if (items.Count > (GameObject.Find("Slots").gameObject.transform.childCount))
        {
            Debug.Log("Not Enough Space");  // If there are more items then there are available slots display error
        }

    }
    #endregion

    #region Methods

    public void Remove(Item item, int removeNum) // remove item object from item list at given location
    {
        Inventory.instance.items.Remove(item);
    }

    public void Use(Item item, int removeNum) { // Use the current tiem
        if (item.consumable)
        { // if the item is a consumable "eat" it
            Inventory.instance.items.Remove(item);
        }
        item.Use();

        Debug.Log("Using" + item);
    }

    public void NullUse(Item item, int removeNum)
    {
        
    }

    public void setMode(InventoryMode input) {  //set current mode to corresponding string input

        switch (input) //switch over to corresponding category
        {
            case InventoryMode.food:
                items = foods;
                break;
            case InventoryMode.clothing:
                items = cloths;
                break;
            case InventoryMode.weapons:
                items = weapons;
                break;
            case InventoryMode.misc:
                items = miscs;
                break;
            case InventoryMode.market:
                items = market;
                break;
        }


        display_mode = input;


    }

    public void updateMarket(List<Item> n_market)
    {
        market = n_market;
        Panel.instance.Show();
    }
    #endregion
}
