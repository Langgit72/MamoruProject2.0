using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phases : MonoBehaviour
{
    public int phaseNumber;
    public List<Sprite> phases;
    public List<float> phaseLights;
    SpriteRenderer my_renderer;
    // Start is called before the first frame update
    void Start()
    {
        my_renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        my_renderer.sprite = phases[phaseNumber];
    }
}
