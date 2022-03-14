using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAI : MonoBehaviour
{
    const float k_attackRadius = 4f;
    const float k_GroundedRadius = .21f;
    public float range;
    private bool m_Grounded;// Whether or not the player is grounded.
    private bool hasAttacked = false;

    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private LayerMask m_WhatIsGround;

    private Transform m_GroundCheck;
    [SerializeField] public Transform attackPoint;
    [SerializeField] public Transform m_player;

    SpriteRenderer m_sprite;
    Rigidbody2D m_Rigidbody2D;
    Animator m_Anim;

    #region Unity Methods
    void Start()
    {
        attackPoint = transform.Find("SamuariPoint");
        m_GroundCheck = transform.Find("GroundCheck");
        m_sprite = gameObject.GetComponent<SpriteRenderer>();
        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();

    }

    void Update()
    {


        if (m_Grounded)
        {
            if (m_player.position.x > attackPoint.transform.position.x)
            {


                m_sprite.flipX = false;
                m_Rigidbody2D.velocity = new Vector2(1.5f, m_Rigidbody2D.velocity.y); ;
                m_Anim.SetFloat("speed", Mathf.Abs(m_Rigidbody2D.velocity.x));
                if (Mathf.Abs(m_player.position.x - attackPoint.transform.position.x) < 1.2 && (Mathf.Abs(m_player.position.y - attackPoint.transform.position.y) < 50))
                {

                    m_Anim.SetTrigger("attack");
                    Attack();
                }


            }
            else if (m_player.position.x < attackPoint.transform.position.x)
            {
                m_sprite.flipX = true;

                m_Rigidbody2D.velocity = new Vector2(-1.5f, m_Rigidbody2D.velocity.y);
                m_Anim.SetFloat("speed", Mathf.Abs(m_Rigidbody2D.velocity.x));

                if (Mathf.Abs(m_player.position.x - attackPoint.transform.position.x) < 1.2 && (Mathf.Abs(m_player.position.y - attackPoint.transform.position.y) < 50))
                {

                    m_Anim.SetTrigger("attack");

                    StartCoroutine(Attack());
                }
            }

        }





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
        Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.position, k_attackRadius, enemyLayers);
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].gameObject != gameObject)
            {
                if (!hasAttacked)
                {
                    players[i].gameObject.GetComponent<Player>().TakeDamage(5, m_sprite.flipX);
                    hasAttacked = true;
                    StartCoroutine(Damage());
                }

            }

        }
        yield return new WaitForSeconds(5);

    }
    #endregion
}
