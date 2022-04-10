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
    public CanvasGroup barGroup;
    public float attackCoolOff;
    private float lastCoolTime;

    private float randSeed;
    public bool fadingIn = false;
    public bool fadingOut = false;
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
        lastCoolTime -= Time.deltaTime;
        if (aggro)
        {
            if (!fadingIn)
            {
                StartCoroutine(FadeInBar());
                fadingIn = true;
            }

        }
        else {
            if (!fadingOut)
            {
                StartCoroutine(FadeOutBar());
                fadingOut = true;
            }

        }
        if (m_player == null)
        {
            m_player = Player.instance.m_Controller.transform;
        }
        float newX = (m_Rigidbody2D.position.x + m_Rigidbody2D.velocity.x);

        m_sprite.flipX = m_Rigidbody2D.velocity.x > 0 ? false : true;
        float distance = Mathf.Abs(m_player.position.x - transform.position.x);
        float distanceY = Mathf.Abs(m_player.position.y - transform.position.y);
        if (distance > range * 2)
        {
            if (aggro) {
                minX = minXTransform.position.x;
                maxX = maxXTransform.position.x;
            }
            aggro = false;
        }
        if ((distanceY <2f) && ((distance < range || aggro) && distance > stop))
        {
            aggro = true;
            if (m_Grounded)
            {
                float direction = (m_player.position.x > transform.position.x) ? moveSpeed : -moveSpeed;
                m_Rigidbody2D.velocity = new Vector2(direction, m_Rigidbody2D.velocity.y);
            }
        }
        else if ((distanceY < 2f) && distance<range) {
        
            if(lastCoolTime < 0)
            {
                StartCoroutine(Attack());
                m_Anim.SetTrigger("attack");
                lastCoolTime = attackCoolOff;
            }

        }
        else
        {
            float num = Mathf.PerlinNoise((Time.time+randSeed),1);
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
                }
                else {
                    m_Rigidbody2D.velocity = new Vector2(-walkSpeed, m_Rigidbody2D.velocity.y);
                }
                   
            }



            if (!((newX < maxX) && (newX > minX)))
            {
                if (!flip)
                {
                    if (!aggro)
                    {
                        walkSpeed *= -1; //reverse direction
                    }

                }
                flip = true;
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
        yield return new WaitForSeconds(1f);
        Player.instance.TakeDamage(5);
      


    }

    IEnumerator Attack()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, k_attackRadius, enemyLayers);
        for (int i = 0; i < players.Length; i++)
        {
            Player.instance.Launch(5, m_sprite.flipX);
            StartCoroutine(Damage());
            /*if (!hasAttacked)
            {
                hasAttacked = true;
               
            }*/

            /*
            if (players[i].gameObject != gameObject)
            {
                if (!hasAttacked)
                {
                    Player.instance.TakeDamage(5, m_sprite.flipX);
                    hasAttacked = true;
                    StartCoroutine(Damage());
                }

            }
            */

        }
        yield return new WaitForSeconds(5);

    }


    IEnumerator FadeInBar()
    {
        barGroup.alpha = 0;
        while (barGroup.alpha < 1f) {
            barGroup.alpha += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        fadingOut = false;

    
    }
    IEnumerator FadeOutBar()
    {
        while (barGroup.alpha > 0f)
        {
            barGroup.alpha -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        fadingIn = false;


    }

    #endregion
}
