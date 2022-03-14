
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyCounter : MonoBehaviour
{
    public int value;
    public Text mytext;

    // Update is called once per frame
    void Update()
    {
        mytext.text = value.ToString();
    }
}
