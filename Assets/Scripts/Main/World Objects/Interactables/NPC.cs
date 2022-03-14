using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public int dialougeID = 0;
    public bool facingLeft;
    public string sp1ID = "null"; //the special ability names to appear in dialouge
    public string sp2ID = "null";

    public DialougeTree dialougeTree;
    public Dialouge activeDialouge; //reference to Dialouge class
    public Transform player;


    public override void Start()
    {
        base.Start();
        activeDialouge = dialougeTree.dialougeList[dialougeID];
    }

    new private void Update()
    {
        activeDialouge.my_speaker = this;
        player = Player.instance.m_Controller.GetComponent<Transform>();
        base.Update();
        sp1ID = activeDialouge.reponses[0];
        sp2ID = activeDialouge.reponses[1];
        /*if (sp1ID != "null") {
           
        }
        if (sp2ID != "null")
        {
           
        }
        */

    
    }

    public void triggerDialouge()
    {
        activeDialouge.my_speaker = this;
        DialougeManager.instance.StartDialouge(activeDialouge); //tell singleton of DialougeManager to begin displaying the NPC's dialouge
    }
    public virtual void Special1()
    {
        Debug.Log("This NPC has no special ability #1 :("); //the most basic NPCs have no special abilities

    }
    public virtual void Special2()
    {
        Debug.Log("This NPC has no special ability #2 :(");
    }
    public override void Interact()
    {
        base.Interact();
        triggerDialouge();
 
    }
    public override void setClose(bool value)
    {
        isClose = value;
        if (isClose)
        {

            if ( player.position.x> gameObject.transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = facingLeft;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = !facingLeft;
            }
            bottomText.text = "Talk to -"+ activeDialouge.title+ " (E)";
        }
        else
        {
            bottomText.text = "";
        }
    }


}
