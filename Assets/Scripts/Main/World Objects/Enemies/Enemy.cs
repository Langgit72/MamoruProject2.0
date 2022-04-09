using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;


public class Enemy : MonoBehaviour
{
    public int max_health; // max health of enemy
    public float current_health; // current health of enemy
    bool alive = true; //is enemy alive

    public Price worth;
    public Cost money_Drops;

    public GameObject mon; // mon game object
    public GameObject shu; // shu game object
    public GameObject ryo; // ryo game object

    public GameObject drop1;
    public GameObject drop2;
    public GameObject drop3;

    //private Rigidbody2D m_Rigidbody2D; // Reference to rigidbody2D
    private SpriteRenderer m_Sprite; // Reference to sprite renderer

    Animator m_Anim;

    System.Random rand = new System.Random(); //Instantiate coins and launch them randomly

  
    #region Unity Methods
    void Start()
    {
        //Setting up references
        current_health = max_health;
        //m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        m_Sprite = gameObject.GetComponent<SpriteRenderer>();
        m_Anim = GetComponent<Animator>();
    }
    #endregion

    #region Methods

    public void TakeDamage(float damage)
    {
        current_health -= damage/10; //reduce health;

        /*
        Vector3 currentVelocity = m_Rigidbody2D.velocity;
        m_Rigidbody2D.velocity = new Vector3(currentVelocity.x*1f, currentVelocity.y, currentVelocity.z);
        */

        if (current_health <= 0 && alive)
        {
            Die();
            alive = false;

        }

    }

    void Die()
    {
        float ranx = (float)rand.NextDouble() * 4;
        float rany = (float)rand.NextDouble() * 4;
        if (drop1 != null)
        {
            GameObject newItem = Instantiate(drop1) as GameObject;
            newItem.transform.position = new Vector3(transform.position.x + ranx, transform.position.y + rany, 0);
        }
        if (drop2 != null)
        {
            GameObject newItem = Instantiate(drop2) as GameObject;
            newItem.transform.position = new Vector3(transform.position.x + ranx, transform.position.y + rany, 0);
        }
        if (drop3 != null)
        {
            GameObject newItem = Instantiate(drop3) as GameObject;
            newItem.transform.position = new Vector3(transform.position.x + ranx, transform.position.y + rany, 0);
        }

  
        CoinLaunch(mon); //upon dying drop cash
        CoinLaunch(shu);
        CoinLaunch(ryo);

        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject); //gets rid of health bar
        }

        m_Anim.SetTrigger("death");




    }

    void CoinLaunch(GameObject coin)
    {
        int m_count = 0;

        if (coin == mon)
        { //drop correct amount of each coin top
            m_count = worth.mon_cost;
        }
        else if (coin == shu)
        {
            m_count = worth.shu_cost;
        }
        else if (coin == ryo)
        {
            m_count = worth.ryo_cost;
        }
        for (int i = 0; i < m_count; i += 1)
        {


            float ranx = (float)rand.NextDouble() * 4;
            float rany = (float)rand.NextDouble() * 4;
            GameObject newMint = Instantiate(coin) as GameObject;
            newMint.transform.position = new Vector3(transform.position.x + ranx, transform.position.y + rany, 0);
         
        }



    }
    #endregion



}