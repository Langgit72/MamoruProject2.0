using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    //edited from github
    
    public Player drop_point; // location of player to drop item from
    public static InputManager instance;
    PlayerControl m_player_controller;
    UIControl m_UIController;
     
    SpriteRenderer m_sprite;
    [SerializeField] private int m_WhatIsPlayer;// A mask determining what is ground to the character
    [SerializeField] private int m_WhatIsPlatform;// A mask determining what is ground to the character
  

    public float moveInput;
    public bool fallInput;
    public bool jumpInput;

    public bool sprintInput;
    public bool attackInput;
    public bool chiInput;

    public bool interactInput;
    public bool inventoryInput;

    public bool upInput;
    public bool downInput;
    public bool leftInput;
    public bool rightInput;

    public bool typelInput;
    public bool typerInput;

    public bool useInput;
    public bool removeInput;

    private int typeInt = 0;
    private Inventory.InventoryMode[] types;

    public bool nextInput;
    public bool spc1Input;
    public bool spc2Input;

    public bool grounded;

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

        DontDestroyOnLoad(gameObject);
        #endregion

        m_player_controller = new PlayerControl();
        m_UIController = new UIControl();
        m_player_controller.Enable();
        types = new Inventory.InventoryMode[] {Inventory.InventoryMode.weapons, Inventory.InventoryMode.food, Inventory.InventoryMode.clothing, Inventory.InventoryMode.misc };

    }

    // Update is called once per frame
    void Update()
    {

        m_player_controller.MotionControl.Jump.started += context => jumpInput = true;
        m_player_controller.MotionControl.Jump.canceled += context => jumpInput = false;
        m_player_controller.MotionControl.Move.started += context => moveInput = context.ReadValue<float>();
        m_player_controller.MotionControl.Move.canceled += context => moveInput = 0f;
        m_player_controller.MotionControl.Fall.started += context => Physics2D.IgnoreLayerCollision(m_WhatIsPlayer, m_WhatIsPlatform, true); 
        m_player_controller.MotionControl.Fall.canceled += context => StartCoroutine(Ground());

        m_player_controller.AbilityControl.Interact.started += context => interactInput = true;
        m_player_controller.AbilityControl.Interact.canceled += context => interactInput = false;

        m_player_controller.AbilityControl.OpenInventory.started += context => inventoryInput = true;
        m_player_controller.AbilityControl.OpenInventory.canceled += context => inventoryInput = false;

        m_player_controller.AbilityControl.Attack.started += context => attackInput = true;
        m_player_controller.AbilityControl.Attack.canceled+= context => attackInput = false;

        m_player_controller.AbilityControl.Chi.started += context => chiInput = true;
        m_player_controller.AbilityControl.Chi.canceled += context => chiInput = false;



        m_UIController.InventoryMap.Close.started += context => inventoryInput = true;
        m_UIController.InventoryMap.Close.canceled += context => inventoryInput = false;

        m_UIController.InventoryMap.UP.started += context => upInput = true;
        m_UIController.InventoryMap.DOWN.started += context => downInput = true;
        m_UIController.InventoryMap.LEFT.started += context => leftInput = true;
        m_UIController.InventoryMap.RIGHT.started += context => rightInput = true;

        m_UIController.InventoryMap.TYPER.started += context => typerInput = true;
        m_UIController.InventoryMap.TYPEL.started += context => typelInput = true;

        m_UIController.InventoryMap.USE.started += context => useInput = true;
        m_UIController.InventoryMap.REMOVE.started += context => removeInput = true;

        m_UIController.DialougeMap.Next.started += context => nextInput = true;
        m_UIController.DialougeMap.Special1.started += context => spc1Input = true;
        m_UIController.DialougeMap.Special2.started += context => spc2Input = true;


        if (attackInput)
        {
            Debug.Log("Hello?");
            LevelLoader.instance.LoadNextLevel();
        }

        if (!Inventory.instance.isHidden) // if the Inventory is open
        {
            if (DialougeManager.instance.isRunning)
            {
                Panel.instance.Hide(); // if there is dialouge running hide the inventory
            }
            m_player_controller.Disable();
            m_UIController.Enable();

            #region Item Highlighting controls

            if (removeInput)
            {
                if (Inventory.instance.items.Count - 1 >= Inventory.instance.highlight) // if the highlighted item is not empty
                {

                    Item removeItem = Inventory.instance.items[Inventory.instance.highlight]; // find item the corresponds with item slot's location in inventory
                    removeItem.transform.position = drop_point.transform.position + new Vector3(0, 2); // drop the item by reactivating it at the players location
                    removeItem.gameObject.SetActive(true);
                    if (removeItem.gameObject.GetComponent<Rigidbody2D>() != null)
                    { // if the item contains a rigidbody...
                        System.Random rand = new System.Random();
                        float ranx = (float)rand.NextDouble() * 8;
                        float rany = (float)rand.NextDouble() * 2;
                        removeItem.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(ranx - 4, rany); // then launch item randomly
                        Inventory.instance.Remove(removeItem, Inventory.instance.highlight); // remove dropped item from inventory
                                                                                             //mycan.GetComponent<Panel>().Hide(); //hide the menu
                        removeItem.inInventory = false;
                    }
                }
                removeInput = false;
            }

            if (useInput)
            {
                if (Inventory.instance.items.Count - 1 >= Inventory.instance.highlight) // if the slot of the item button contains an item
                {
                    Item useItem = Inventory.instance.items[Inventory.instance.highlight];  // find item the corresponds with item slot's location in inventoryItem 
                    if (Inventory.instance.display_mode == Inventory.InventoryMode.market)
                    {
                        Inventory.instance.items.Remove(useItem);
                        Item newItem = Instantiate(useItem); //spawn item into world
                        newItem.Collect(); //Add item to inventory
                        Inventory.instance.Remove(useItem, Inventory.instance.highlight);
                    } //if we select item from inventory, add item to inventory
                    else
                    {
                        Inventory.instance.Use(useItem, Inventory.instance.highlight); //use the item
                    }
                }
                else //otherwise there is no item to the use
                {
                    Debug.Log("No item to Use"); //if no weapon selected, equip "punch" weapon
                    if (Inventory.instance.display_mode == Inventory.InventoryMode.market)
                    {

                        /*
                        //update the "drop point's"(player's) weapon stats, attack mode, and weapon animation
                        drop_point.weapon_range = 1.5f;
                        drop_point.weapon_speed = 100f;
                        drop_point.strength += 0;
                        drop_point.windUp = 0.5f;

                        drop_point.gameObject.transform.Find("weapon").gameObject.GetComponent<Animator>().runtimeAnimatorController = basicAttack;

                        drop_point.attackMode = 0;
                        */

                    }



                }
                useInput = false;
            }

            else
            {
                Debug.Log("No item to Remove"); //otherwise there is no item to the remove
            }

            int newHighLight = Inventory.instance.highlight; //retrieve current highlighted item tile
            if (typerInput||typelInput)
            {
                if (typelInput)
                {
                    if (typeInt == 0)
                    {
                        typeInt = GameObject.Find("Type Selectors").gameObject.transform.childCount-1;
                    }
                    else
                    {
                        typeInt -= 1;
                    }
                    typelInput = false;
                }
                if (typerInput)
                {
                    if (typeInt == GameObject.Find("Type Selectors").gameObject.transform.childCount - 1)
                    {
                        typeInt = 0;
                    }
                    else
                    {
                        typeInt += 1;
                    }
                    typerInput = false;
                }

            
                Inventory.InventoryMode currentType = types[typeInt];
                if (Inventory.instance.display_mode != Inventory.InventoryMode.market)
                {
                    Inventory.instance.setMode(currentType);
                    Inventory.instance.highlight = 0;  //reset highlight
                }
           
                for (int i = 0; i < GameObject.Find("Type Selectors").gameObject.transform.childCount; i += 1)
                {
                    if (i == typeInt)
                    {
                        GameObject.Find("Type Selectors").gameObject.transform.GetChild(i).GetComponent<Image>().color = new Color(0.5f, 0.5f, 1, 1);
                    }
                    else
                    {
                        GameObject.Find("Type Selectors").gameObject.transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    }
              
                }
            }



            if (upInput)
            {
                newHighLight -= 6;
                upInput = false;
            }
            if (downInput)
            {
                newHighLight += 6;
                downInput = false;
            }
            if (leftInput)
            {
                newHighLight -= 1;
                leftInput = false;
            }
            if (rightInput)
            {
                newHighLight += 1;
                rightInput = false;
            }

            if (0 > newHighLight)
            {
                newHighLight += 18; // if the highlight is in the "negative row" bump it up three rows(circle back to the bottom)
            }
            else if (newHighLight > GameObject.Find("Slots").transform.childCount - 1)
            {
                newHighLight -= 18; // if the highlight is in the "super row" bump it up down rows(circle back to the top)
            }
            Inventory.instance.highlight = newHighLight; //update highlighted item tile
            #endregion;

        }

        else if (DialougeManager.instance.isRunning) //if Dialouge is running
        {
             m_player_controller.Disable();
             m_UIController.Enable();

          
            if (DialougeManager.instance.continueButton.activeSelf) //press space to "press" continue to next dialouge 
            {
                if (nextInput)
                {
                    DialougeManager.instance.DisplayNextSentence();
                    nextInput = false;
                }

            }

            if (DialougeManager.instance.sp1Button.activeSelf) //press space to "press" continue to next dialouge 
            {
                if (spc1Input)
                {
                    DialougeManager.instance.Special1();
                    spc1Input = false;
                }
            }

            if (DialougeManager.instance.sp2Button.activeSelf) //press space to "press" continue to next dialouge 
            {
                if (spc2Input)
                {
                    DialougeManager.instance.Special2();
                    spc2Input = false;
                }
            }

            else
            {
                nextInput = false;
                spc1Input = false;
                spc2Input = false;
            }
        }
        else
        {
            m_player_controller.Enable();
            m_UIController.Disable();
        }



    }

    IEnumerator Ground()
    { 
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreLayerCollision(m_WhatIsPlayer, m_WhatIsPlatform, false);
    }




}
