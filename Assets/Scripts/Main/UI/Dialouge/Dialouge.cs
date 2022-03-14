using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialouge // JUST A CLASS
{
    [TextArea(3,9)]
    public string name;
    public string title;
    public string title2;
    public string[] sentences;
    public string[] reponses;
    public int nameReveal;
    public bool canBeSkipped;
    public bool sp1triggersDialouge;
    public bool sp2triggersDialouge;
    public NPC my_speaker;
   
}
