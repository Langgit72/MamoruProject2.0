


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    public TMP_Text clock;
    public TMP_Text period;

    // Update is called once per frame
    void Update()
    {
        DayNightCycle.instance.clockText = clock;
        DayNightCycle.instance.periodText = period;

    }
}
