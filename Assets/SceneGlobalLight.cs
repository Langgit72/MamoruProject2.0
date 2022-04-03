using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SceneGlobalLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        DayNightCycle.instance.globalLight = gameObject.GetComponent<Light2D>();
    }

}
