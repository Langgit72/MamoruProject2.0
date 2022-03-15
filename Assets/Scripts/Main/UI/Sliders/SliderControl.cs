using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SliderControl : MonoBehaviour
{
    [SerializeField] private Slider m_slider; // Reference to slider object
    [SerializeField] private Player m_value; //Reference to enemy object

    // Start is called before the first frame update
    void Start()
    {
        // Set up references
        m_slider = GetComponent<Slider>();
        m_value = Player.instance;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Health bar")
        {
            m_slider.value = m_value.current_health / 100f; //Update health bar with current health of player
        }
        else if (gameObject.name == "Strength bar")
        {
            m_slider.value = m_value.current_stamina / 100f; //Update health bar with current health of player
            
        }
        else if (gameObject.name == "Ki bar")
        {
            m_slider.value = m_value.current_chi / 100f; //Update health bar with current health of play
            
        }

    }
}
