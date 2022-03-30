using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : Interactable
{


    public bool consumable; // If item is consumable
    public bool autocollect; // If item autocollect is enabled
    public bool inInventory=true;
    public Player m_player;

    public string title; // title of item to display in inventory
    public string info; // information item to display in inventory
    public Price cost;

    protected SpriteRenderer m_sprite; // Reference to sprite renderer
    protected Item this_item; // Reference to item component
    protected Sprite icon; // Reference to sprite
    protected Color currentColor; //// Reference to color
    public Inventory.InventoryMode m_type;



    // Start is called before the first frame update



    protected virtual private void Awake()
    {
        m_player = Player.instance;
    }
    new protected virtual void Start()
    {
        base.Start();
  

        if (!inInventory)
        {
            //Destroy(gameObject);
        }


        //Setting up references
        this_item = gameObject.GetComponent<Item>();
       m_sprite = gameObject.GetComponent<SpriteRenderer>();
       icon = m_sprite.sprite;
       r = m_sprite.color.r;
       g = m_sprite.color.g;
       b = m_sprite.color.b;
      

    }

    // Update is called once per frame




    public virtual void Collect()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false); //deactivate item
        inInventory = true;
        switch (m_type) //switch over to corresponding category
        {
            case Inventory.InventoryMode.food:
                Inventory.instance.foods.Add(this);
                break;
            case Inventory.InventoryMode.clothing:
                Inventory.instance.cloths.Add(this);
                break;
            case Inventory.InventoryMode.weapons:
                Inventory.instance.weapons.Add(this);
                break;
            case Inventory.InventoryMode.misc:
                Inventory.instance.miscs.Add(this);
                break;
        }

    }
    public virtual void Use()
    {

  
    }

    public virtual void NullUse() {

    }

    public override void Interact() {
        base.Interact();
        Debug.Log("collecting");
        Collect();
    }
    public override void HighLight()
    {
 
        if (!isClose) // if item is close highlight it green
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(r, g, b);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.7f, 0.5f);
        }
    }

    public override void setClose(bool value)
    {
        isClose = value;
        if (isClose&&!autocollect)
        {
            bottomText.text = "Pick Up -" + title + "(E)";
        }
        

    }



}
