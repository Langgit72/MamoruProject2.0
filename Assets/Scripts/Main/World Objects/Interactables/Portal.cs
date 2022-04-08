using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Interactable
{
    public LevelLoader.SceneType sceneType;
    public string title; // title of item to display in inventory

    public override void setClose(bool value)
    {
        isClose = value;
        if (isClose)
        {
            bottomText.text = "Enter -" + title + "(E)";
        }
    }

    public override void Interact()
    {
        base.Interact();
        LevelLoader.instance.LoadScene(sceneType);


    }
}
