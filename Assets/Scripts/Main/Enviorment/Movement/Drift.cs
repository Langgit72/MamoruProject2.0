using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drift : MonoBehaviour
{
    public float speed; // speed of drift
    public float loopRate; // how often to cycle clouds
    public float startX; // starting xpos
    Transform m_transform;

    // Start is called before the first frame update
    void Start()
    {        //Setting up references
        m_transform = gameObject.transform;
        startX = gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(m_transform.position.x-startX)>loopRate) { // if end of cycle is reached start over
            m_transform.position = new Vector3(startX, m_transform.position.y, m_transform.position.z);
        }
        m_transform.position = new Vector3(m_transform.position.x+speed, m_transform.position.y, m_transform.position.z); //drift to the right
    }
}
