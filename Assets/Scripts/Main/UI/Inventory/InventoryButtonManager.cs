using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class InventoryButtonManager : MonoBehaviour
{
	public int inventoryTag; // slots location in inventory

	public Player drop_point; // location of player to drop item from

	Button btn; //item button of slot
	Price worth;

	RuntimeAnimatorController basicAttack;
	GameObject mycan; // canvas group of button

	void Start()
	{
		mycan = GameObject.Find("Menu");
		Button btn = gameObject.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{

        #region Remove button
        if (gameObject.name == "Remove Button") // If the remove button has been pressed
		{
			if (Inventory.instance.items.Count-1 >= inventoryTag) // if the slot of the remove button contains an item
			{
				Item removeItem = Inventory.instance.items[inventoryTag]; // find item the corresponds with item slot's location in inventory
				removeItem.transform.position = drop_point.transform.position + new Vector3(0,2); // drop the item by reactivating it at the players location
				removeItem.gameObject.SetActive(true);
				if (removeItem.gameObject.GetComponent<Rigidbody2D>() != null)
				{ // if the item contains a rigidbody...
					System.Random rand = new System.Random(); 
					float ranx = (float)rand.NextDouble() * 8;
					float rany = (float)rand.NextDouble() * 2;
					removeItem.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(ranx-4, rany); // then launch item randomly
					Inventory.instance.Remove(removeItem, inventoryTag); // remove dropped item from inventory
					//mycan.GetComponent<Panel>().Hide(); //hide the menu
				}
			}
			else 
			{  
				Debug.Log("No item to Remove"); //otherwise there is no item to the remove
			}

		}
		#endregion 

		#region Use button
		else
		{// Otherwise the item button has been pressed

			Inventory.instance.highlight = inventoryTag;
			if (Inventory.instance.items.Count - 1 >= inventoryTag) // if the slot of the item button contains an item
			{
				Item useItem = Inventory.instance.items[inventoryTag];  // find item the corresponds with item slot's location in inventoryItem 
				if (Inventory.instance.display_mode == Inventory.InventoryMode.market)
				{
					Inventory.instance.items.Remove(useItem);
					Item newItem = Instantiate(useItem); //spawn item into world
					newItem.Collect(); //Add item to inventory

					Inventory.instance.Remove(useItem, inventoryTag);
				} //if we select item from inventory, add item to inventory
				else
				{
					Inventory.instance.Use(useItem, inventoryTag); //use the item
				}
			}
			else //otherwise there is no item to the use
			{
				Debug.Log("No item to Use"); //if no weapon selected, equip "punch" weapon
				if (Inventory.instance.display_mode == Inventory.InventoryMode.market) {

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

		}
        #endregion
    }

}
