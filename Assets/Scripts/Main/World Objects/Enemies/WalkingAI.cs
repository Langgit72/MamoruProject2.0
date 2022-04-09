using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAI : MonoBehaviour
{
    const float k_attackRadius = 4f;
    const float k_GroundedRadius = .21f;
    public float range;
    public float stop;
    public float moveSpeed;
    public float walkSpeed;
    public float frequency;
    public bool aggro=false;
    public float randSeed;

    private bool moveValue;
    public bool flip;
    private bool m_Grounded;// Whether or not the player is grounded.
    private bool hasAttacked = false;
    private bool wandering = false;

    private float maxX;
    private float minX;

    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private LayerMask m_WhatIsGround;

    private Transform m_GroundCheck;
    [SerializeField] public Transform m_player;
    [SerializeField] public Transform maxXTransform;
    [SerializeField] public Transform minXTransform;
    SpriteRenderer m_sprite;
    Rigidbody2D m_Rigidbody2D;
    Animator m_Anim;

    #region Unity Methods
    void Start()
    {
        m_GroundCheck = transform.Find("GroundCheck");
        m_sprite = gameObject.GetComponent<SpriteRenderer>();
        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
        m_player = Player.instance.m_Controller.transform;
        randSeed = Random.Range(0, 100);

        minX = minXTransform.position.x;
        maxX = maxXTransform.position.x;

    }

    void Update()
    {
        if (m_player == null)
        {
            m_player = Player.instance.m_Controller.transform;
        }
        float newX = (m_Rigidbody2D.position.x + m_Rigidbody2D.velocity.x);

        m_sprite.flipX = m_Rigidbody2D.velocity.x > 0 ? false : true;
        float distance = Mathf.Abs(m_player.position.x - transform.position.x);
        float distanceY = Mathf.Abs(m_player.position.y - transform.position.y);
        Debug.Log("Tickle" + distanceY);
        if (distance > range * 2)
        {
            if (aggro) {
                minX = minXTransform.position.x;
                maxX = maxXTransform.position.x;
                Debug.Log("resetting transform");
            }
            aggro = false;
        }
        if ((distanceY <2f||aggro) && ((distance < range || aggro) && distance > stop))
        {
            aggro = true;
            if (m_Grounded)
            {
                float direction = (m_player.position.x > transform.position.x) ? moveSpeed : -moveSpeed;
                Debug.Log("rain" +direction);
                m_Rigidbody2D.velocity = new Vector2(direction, m_Rigidbody2D.velocity.y);
            }
        }
        else if ((distanceY < 2f) && distance<range) {
            m_Anim.SetTrigger("attack");
            StartCoroutine(Attack());
        }
        else
        {
            float num = Mathf.PerlinNoise((Time.time+randSeed),1);
            Debug.Log("iga"+num);
            if (num > frequency)
            {
                wandering = false;
                m_Rigidbody2D.velocity = new Vector2(0f, m_Rigidbody2D.velocity.y);
            }
            else 
            {

                if (!wandering)
                {
                    moveValue = Random.value > 0.5f;
                }
                wandering = true;
                if (moveValue)
                {
                    m_Rigidbody2D.velocity = new Vector2(walkSpeed, m_Rigidbody2D.velocity.y);
                    Debug.Log("Leo Left ");
                }
                else {
                    m_Rigidbody2D.velocity = new Vector2(-walkSpeed, m_Rigidbody2D.velocity.y);
                    Debug.Log("Leo Right ");
                }
                   
            }



            if (!((newX < maxX) && (newX > minX)))
            {

              
                if (!flip)
                {
                    walkSpeed *= -1; //reverse direction
                }
                flip = true;
                //m_Rigidbody2D.velocity = new Vector2(-m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y);
                Debug.Log("pilates"); // + minX + "," + newX + "," + maxX + "," + ((newX < maxX) && (newX > minX))) ;
            }
            else {
                flip = false;
            }

        }

        m_Anim.SetFloat("speed", Mathf.Abs(m_Rigidbody2D.velocity.x));



    }

    private void FixedUpdate()
    {

        m_Grounded = false;
        if (m_GroundCheck != null) //if the referenced enemy hasn't been killed
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
     
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;

                }
            }
        }
        m_Anim.SetBool("grounded",m_Grounded);

    }
    #endregion

    #region

    IEnumerator Damage()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.8f);
        hasAttacked = false;

    }

    IEnumerator Attack()
    {
        Debug.Log("Attacking");
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, k_attackRadius, enemyLayers);
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].gameObject != gameObject)
            {
                if (!hasAttacked)
                {
                    Player.instance.TakeDamage(5, m_sprite.flipX);
                    hasAttacked = true;
                    StartCoroutine(Damage());
                }

            }

        }
        yield return new WaitForSeconds(5);

    }
    #endregion
}
