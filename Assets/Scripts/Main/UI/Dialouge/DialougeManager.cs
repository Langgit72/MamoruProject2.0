using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = System.Random;

public class DialougeManager : MonoBehaviour
{
    private bool canBeSkipped;
    public bool isRunning;
    private int sentenceCount=0;

    public TMP_Text nameText;
    public TMP_Text dialougeText;

    public GameObject continueButton;
    public GameObject sp1Button;
    public GameObject sp2Button;

    private Queue<string> sentences; //FIFO list    

    public Dialouge thisDialouge;
    public Animator dialougeBox;

    public static DialougeManager instance; // the single instance of inventory

    #region Unity Methods
    void Awake()
    {
        #region Singleton //Singleton instance
        if (instance == null)
        {
            instance = this; //if there is no current dialouge manager instance in game, occupy singleton with current dialouge manager class
        }
        else
        {
            Debug.LogWarning("More than one instance"); // otherwise there is already a dialouge manager instance in game, and no more can be created
            Destroy(gameObject);
            return;
        }
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
  
    }

    private void Update()
    {
        dialougeBox.SetBool("isOpen", isRunning);
    }
    #endregion

    #region Methods
    public void StartDialouge(Dialouge dialouge) {

        sentenceCount = dialouge.sentences.Length;
        sp1Button.SetActive(false);
        sp2Button.SetActive(false);
        thisDialouge = dialouge;
        isRunning = true;
        sentences.Clear(); //remove sentences from previous convo

        for(int i = 0; i < dialouge.sentences.Length; i += 1)
        {
            sentences.Enqueue(dialouge.sentences[i]); //add all sentences stored in dialouge class to Queue
        }

        nameText.text = dialouge.title; //wait to reveal character name
        canBeSkipped = dialouge.canBeSkipped; //update canBeSkipped value to current spekaer

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {

        if (!canBeSkipped)
        {
        }
        else
        {
            continueButton.GetComponent<Button>().GetComponentInChildren<Text>().text = "Skip"; //if it can be skipped, continue button becomes skip button
        }
    

        if (sentences.Count == 0) //if there are no more sentences to be queued end the dialouge
        {
            EndDialouge();
        }
        else {
            string displayString = sentences.Dequeue(); //grap the next value from the sentence Queue
            StopAllCoroutines();
            StartCoroutine(TypeSentence(displayString)); //begin the letter-by-letter display of dialouge
            if (sentenceCount == thisDialouge.nameReveal) //update name UI to currrent speaker
            {
                nameText.text = thisDialouge.title;
            }
            else
            {
                nameText.text = thisDialouge.title2;
            }
            sentenceCount += 1;


        }

    }

    public void Special1() //special ability #1
    {
   
        if (continueButton.activeSelf) {
            NPC currentNpc = thisDialouge.my_speaker; //grab the NPC class providing current dialouge
            currentNpc.Special1(); //run the special ability
        }
        if (!thisDialouge.sp1triggersDialouge) {
            EndDialouge();
        }
     

    }

    public void Special2() //special ability #2
    {
        if (!thisDialouge.sp2triggersDialouge)
        {
            EndDialouge();
        }
        if (continueButton.activeSelf)
        {
            NPC currentNpc = thisDialouge.my_speaker; //grab the NPC class providing current dialouge
            currentNpc.Special2(); //run the special ability
        }
 


    }

    public void EndDialouge() { 
        isRunning = false; // dialouge manager is no longer running

    }

    IEnumerator TypeSentence(string sentence)
    {
  
        dialougeText.text = ""; // clear text at the begining
        char[] my_characters = sentence.ToCharArray(); //convert sentence into a string of characters
        foreach (char character in my_characters) { //loop through each character in sentence
            dialougeText.text += character; //add character by character to the dialougeText being displayed
            Random rng = new System.Random();
            bool randomBool = rng.Next(0, 2) > 0; //Play click or play audio randomly
            if (DialougeManager.instance.isRunning)
            {
                if (randomBool)
                {
                    AudioController.instance.PlayAudio(AudioType.UI_01);
                }
                else
                {
                    AudioController.instance.PlayAudio(AudioType.UI_02);
                }
            }

            yield return new WaitForSeconds(0.02f);//speed of dialouge appearance
        }
        yield return new WaitForSeconds(0.5f); // wait a bit after dialouge to display continue button
        continueButton.SetActive(true);
        if (sentences.Count == 0)
        {    
            if (thisDialouge.my_speaker.sp1ID!="null") { // If the active NPC has a special ability 1
                sp1Button.GetComponent<Button>().GetComponentInChildren<Text>().text = thisDialouge.my_speaker.sp1ID+" (M)";
                sp1Button.SetActive(true);
            }
            if (thisDialouge.my_speaker.sp2ID != "null") {  // If the active NPC has a special ability 2
                sp2Button.GetComponent<Button>().GetComponentInChildren<Text>().text = thisDialouge.my_speaker.sp2ID +" (N)";
                sp2Button.SetActive(true);
            }
            continueButton.GetComponent<Button>().GetComponentInChildren<Text>().text = "Finish"; // if at the end skip button becomes a finish button
        }
        else
        {
            continueButton.GetComponent<Button>().GetComponentInChildren<Text>().text = "Continue"; //otherwise skip button becomes continue button again
        }
    }
    #endregion


}
