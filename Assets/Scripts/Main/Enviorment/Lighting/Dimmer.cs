using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class Dimmer : MonoBehaviour
{
    public Light2D m_light;

    // Start is called before the first frame update
    void Start()
    {
     m_light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_light.intensity = (1 - DayNightCycle.instance.og_day.a)*0.75f;
    }
}
