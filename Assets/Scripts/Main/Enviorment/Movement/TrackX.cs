using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackX : MonoBehaviour
{
    public bool shiftConstant = false;
    public Transform player;
    Transform m_transform;
    public Vector3 offset = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        m_transform = gameObject.GetComponent<Transform>();
        if (!shiftConstant)
        {
            offset = m_transform.position - player.position;
        }

    }

    // Update is called once per frame

    private void Update()
    {

    }
    void FixedUpdate()
    {
        m_transform.position = new Vector3(player.position.x + offset.x,offset.y, player.position.z + offset.z);
    }
}
