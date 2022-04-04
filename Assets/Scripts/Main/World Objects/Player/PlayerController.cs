using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#pragma warning disable 649
public class PlayerController : MonoBehaviour
{
    public enum MotionState
    {
        RISING,
        FALLING,
        WALKING,
        RUNNING,
        GROUNDED,
        SLIDING,
        EDGE,
    }

    public bool grounded;

    public float k_GroundedRadius = 0f; // Radius of the overlap circle to determine if grounded

    public bool isJumping;

    private Rigidbody2D m_Rigidbody2D;  // Reference to the player's rigidbody component.

    private float lastGroundedTime;
    private float lastJumpTime;

    [SerializeField] private int m_WhatIsGround;// A mask determining what is ground to the character
    [SerializeField] private int m_WhatIsPlatform;// A mask determining what is ground to the character
    
    [SerializeField] float coyoteGroundedTime; //leway time from being grounded
    [SerializeField] float cutMultiplier; //leway time from being grounded
    [SerializeField] private float coyoteJumpTime; //leway time from pressing jump button

    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float airFriction;

    [SerializeField] private float jumpForce;
    [SerializeField] private float midAirControl;
    [SerializeField] private int velPower;

    public InputManager m_input;
    public SpriteRenderer m_Sprite;
    public Animator m_anim;

    public Animator weapon_anim;
    public MotionState motion;
    public Transform attackpos;


    private void Awake() 
    {
  
    }

    private void Start()
    {
        Player.instance.m_Controller = gameObject.GetComponent<PlayerController>();
        Player.instance.m_Sprite = m_Sprite;
        m_input = InputManager.instance;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Sprite = gameObject.GetComponent<SpriteRenderer>();
        Player.instance.weapon_Anim = weapon_anim;
        Player.instance.attackCollider = attackpos;
        DayNightCycle.instance.center = gameObject.transform;
        m_anim.runtimeAnimatorController = Player.instance.m_aniController;

    }

    private void Update()
    {
        Player.instance.m_aniController = m_anim.runtimeAnimatorController;

    }

    private void FixedUpdate()
    {
 

        UpdateRun();
        UpdateJump();

        m_anim.SetBool("Walking", false);
        m_anim.SetBool("Sprinting", false);
        m_anim.SetBool("Rising", false);
        m_anim.SetBool("Falling", false);
        m_anim.SetBool("Idle", false);

        switch (motion)
        {
            case MotionState.WALKING:
               // m_Sprite.color = new Color(255, 0, 0);
                m_anim.SetBool("Walking", true);
                break;
            case MotionState.RUNNING:
               // m_Sprite.color = new Color(0, 255, 0);
                m_anim.SetBool("Sprinting", true);
                break;
            case MotionState.RISING:
               // m_Sprite.color = new Color(0, 0, 255);
                m_anim.SetBool("Rising", true);
                break;
            case MotionState.FALLING:
               // m_Sprite.color = new Color(255, 0, 255);
                m_anim.SetBool("Falling", true);
                break;
            case MotionState.EDGE:
               // m_Sprite.color = new Color(255, 255, 0);
                break;
            case MotionState.GROUNDED:
               // m_Sprite.color = new Color(255, 255, 255);
                m_anim.SetBool("Idle", true);
                break;


        }


    }


    void OnCollisionStay2D(Collision2D collision)
    {

        foreach (var item in collision.contacts)
        {
            if (item.normal == Vector2.up && (collision.collider.gameObject.layer == m_WhatIsGround || collision.collider.gameObject.layer == m_WhatIsPlatform))
            {
                grounded = true;
                m_input.grounded = grounded;
                isJumping = false;

            }

            if (item.normal == Vector2.down && m_Rigidbody2D.velocity.y > 0)
            {
                m_Rigidbody2D.AddForce(Vector2.down * (jumpForce * cutMultiplier), ForceMode2D.Force);
            }
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
        m_input.grounded = grounded;
    }

    private void UpdateJump()
    {

        #region Jump

        #region CoyoteTime


        if (grounded) {
            lastGroundedTime = coyoteGroundedTime;
        }
        else {
            lastGroundedTime -= Time.deltaTime;
        }
        if (m_input.jumpInput)
        {
            lastJumpTime = coyoteJumpTime;
        }
        else {
            lastJumpTime -= Time.deltaTime;
        }


      

        #endregion



        if (m_Rigidbody2D.velocity.y < -0.1)
        {
            motion = MotionState.FALLING;
            m_Rigidbody2D.AddForce(Vector2.down * (jumpForce * cutMultiplier), ForceMode2D.Force);
        }
        if (m_Rigidbody2D.velocity.y > 0.1)
         {
            motion = MotionState.RISING;
            if (!m_input.jumpInput)
            {
               m_Rigidbody2D.AddForce(Vector2.down * (jumpForce * cutMultiplier), ForceMode2D.Force);
            }
        }


        if ((lastJumpTime > 0 && lastGroundedTime > 0) && !isJumping)
        {
            isJumping = true;
            m_Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            lastJumpTime = coyoteJumpTime;


        }
        #endregion
    }

    private void UpdateRun()
    {
        #region Run
        //m_Rigidbody2D.velocity = new Vector2(m_input.moveInput * moveSpeed, m_Rigidbody2D.velocity.y);

        if (m_input.moveInput == -1)
        {
            m_Sprite.flipX = true;
        }
        else if (m_input.moveInput == 1)
        {
            m_Sprite.flipX = false;
        }

        float targetSpeed = m_input.moveInput * moveSpeed;
        float speedDif = targetSpeed - m_Rigidbody2D.velocity.x;
        float newAccel = grounded ? acceleration : acceleration * midAirControl;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? newAccel : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);


        m_Rigidbody2D.AddForce(movement * Vector2.right);



        if (Mathf.Abs(m_Rigidbody2D.velocity.x) > moveSpeed * 0.9)
        {
            motion = MotionState.RUNNING;
        }
        else if (Mathf.Abs(m_Rigidbody2D.velocity.x) > moveSpeed * 0.1)
        {
            motion = MotionState.WALKING;
        }
        else
        {
            motion = MotionState.GROUNDED;
        }
        if(motion == MotionState.GROUNDED)
        {
            grounded = true;
        }

        #endregion
    }

 


}

