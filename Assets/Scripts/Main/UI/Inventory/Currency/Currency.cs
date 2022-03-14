using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : Item
{
    public string type;

    public override void Collect()
    {

        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
        if (type == "mon")
        {
            GameObject.Find("Counters").gameObject.transform.GetChild(0).GetComponent<CurrencyCounter>().value+=1;
        }
        else if (type == "shu")
        {
            GameObject.Find("Counters").gameObject.transform.GetChild(1).GetComponent<CurrencyCounter>().value += 1;
        }
        else if(type == "ryo"){
            GameObject.Find("Counters").gameObject.transform.GetChild(2).GetComponent<CurrencyCounter>().value += 1;

        }

    }
}
