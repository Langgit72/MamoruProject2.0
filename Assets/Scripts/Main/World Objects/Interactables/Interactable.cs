using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Interactable : MonoBehaviour
{
    public bool isClose; // If player is close to item
    protected float r; // RGB color values
    protected float g;
    protected float b;

    public TMP_Text bottomText; //reference to text at the bottom;

    public virtual void Start(){
        bottomText = BottomController.instance.GetComponent<TMP_Text>();
    }

    protected void Update()
    {
        HighLight();
     
    }

    public virtual void setClose(bool value){ //set method
        isClose = value;
        if (isClose)
        {
            bottomText.text = "Interact -E";
        }

    }

    public virtual void Interact()
    {

    }
    public virtual void HighLight()
    {

    }
}
