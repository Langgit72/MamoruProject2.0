using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemySliderControl : MonoBehaviour
{
    [SerializeField] private Slider m_slider; // Reference to UI slider
    
    [SerializeField] private Enemy m_value; // Reference to enemy game object

    // Start is called before the first frame update
    void Start()
    {
        //Set up references
        m_slider = GetComponent<Slider>();


    }

    // Update is called once per frame
    void Update()
    {
        m_slider.value = m_value.current_health / 100f; //set slider value to health value of enemy game object

    }
}
