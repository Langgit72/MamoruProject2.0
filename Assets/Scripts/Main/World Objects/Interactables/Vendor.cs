using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Vendor : NPC
{
    public string marketName;

    public List<Item> stock;
    public List<Price> catalog;

    public PlayerDisplay displayer;

    public override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        base.Interact();
       

    }

    public void triggerMarket() {

        Inventory.instance.updateMarket(stock);
        Inventory.instance.display_mode = Inventory.InventoryMode.market;

        GameObject.Find("Select Display").GetComponent<TMP_Text>().text = marketName;
        displayer.player = gameObject;

    }

    public override void Special1()
    {
        DialougeManager.instance.EndDialouge();
        triggerMarket(); //Trigger the Market
    }
    public override void Special2()
    {

        dialougeID += 1;
        activeDialouge = dialougeTree.dialougeList[dialougeID];
        DialougeManager.instance.StartDialouge(activeDialouge);
    }


}
