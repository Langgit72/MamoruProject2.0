using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

#pragma warning disable 649
public class Player : MonoBehaviour
{

    #region properties
    public static Player instance;

    #region Stats
    public float max_health = 100; // max health of character
    public float max_stamina = 100; // max health of character
    public float max_chi = 100; // max chi of character
    public float strength;

    public float current_health; // current health of character
    public float current_stamina; // current staminia of character
    public float current_chi; // current chi of character

    [SerializeField] float norm_health;
    [SerializeField] float norm_strength;


    // chi multipliers
    private float chi_health;
    private float chi_strength;
    private float weaponPositionX;


    #endregion

    #region Weapon status
    public float base_range;
    public float weapon_range;
    public float weapon_speed;
    public float windUp;
    public int attackMode;
    #endregion


    [SerializeField] private LayerMask enemyLayers; // A mask determining what are enemies to the character
    #endregion

    #region States
    public bool chiEnable; // whether or not chi mode is active
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private bool canAttack = true;
    #endregion

    #region Component References
    public Transform attackCollider; //A position marking hwere to to for player attacks
    public Transform m_Transform;

    public Animator m_Anim;  // Reference to the player's animator component.
    public RuntimeAnimatorController m_aniController;
    public Animator weapon_Anim; // Reference to the player weapon animator component.

    public SpriteRenderer m_Sprite; // Reference to the player's sprite component(variable).
    private GameObject weapon;
    public TMP_Text bottomText; //reference to text at the bottom;
    public Rigidbody2D m_Rigidbody2D;
    public InputManager m_input;
    public PlayerController m_Controller;



    #endregion



    #region Unity Methods
    private void Awake()
    {


        #region Singleton instance
        if (instance == null)
        {
            instance = this; //if there is no current inventory instance in game, occupy singleton with current inventory class
        }
        else
        {
            Debug.LogWarning("More than one instance"); // otherwise there is already an inventory instance in game, and no more can be created
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        #endregion

        #region Reference definitions

        bottomText = BottomController.instance.GetComponent<TMP_Text>();
        //GameManager.instance.player = this;
        //m_Anim = Player.instance.m_Anim; 
        #endregion

        #region Stat Initialization
        max_health = norm_health; // set physcial cability to normal levels
        strength = norm_strength;


        current_health = max_health; //set player to full health, chi, and stamina
        current_chi = max_chi;
        current_stamina = max_stamina;


        chi_health =  norm_health*2;
        chi_strength = norm_strength*2;
        #endregion

        m_Transform = GetComponent<Transform>();

     


    }

    private void Start()
    {
        m_input = InputManager.instance;
        m_input.drop_point = this;
        weapon = weapon_Anim.gameObject;
        weaponPositionX = weapon.transform.localPosition.x;
    }
    

    private void Update()
    {

        Interact(); //always be interacting with the world
        canAttack = attackCollider.localScale.x == base_range; //if the attack collider has return to the base range, the player can attack once more
    
        #region Attack Animation Controls
        m_Anim.SetInteger("Attack Mode",attackMode); //set both player and weapon animation to the attackMode
        weapon_Anim.SetInteger("Attack Mode", attackMode);
        #endregion
  
        weapon.GetComponent<SpriteRenderer>().flipX = m_Sprite.flipX;
        float positionMod = m_Sprite.flipX ? weaponPositionX * -1 : weaponPositionX;
        Vector3 newPosition = new Vector3(positionMod, weapon.transform.localPosition.y, weapon.transform.localPosition.z);
        weapon.transform.localPosition = newPosition;

        if (m_input.attackInput)
        {
            StartCoroutine(AttackRoutine());
        }
 
        /*
           if (m_input.chiInput)
           {
               chiEnable = !chiEnable;
           }



           #region Chi Management
           for (int i = 0; i < transform.childCount; i += 1) 
           {
               GameObject childObj = gameObject.transform.GetChild(i).gameObject;
               if (childObj.name == "Chi") // find chi child object
               {
                   childObj.GetComponent<Light2D>().intensity = 1+(current_chi /30f); // create intensity grop off
                   //childObj.GetComponent<ParticleSystem>().emission.rateOverTime
                   if (!chiEnable) //disable chi
                   {
                       childObj.SetActive(false);
                   }

                   if (chiEnable&&current_chi>0) // if chi bar is not empty, and chi is enabled
                   {
                       max_health = chi_health; //boost stats to chi levels
                       strength = chi_strength;
                       childObj.SetActive(true); //activate chi
                       StartCoroutine("decreaseChi"); //decrease chi over time interval

                   }
                   else if(current_chi<max_chi) { // if chi is empty
                       chiEnable = false;
                       StartCoroutine("increaseChi"); 
                       childObj.SetActive(false);
                       max_health = norm_health;
                       strength = norm_strength;
                   }
               }
           }
           #endregion
        */



    }

    private void FixedUpdate()
    {



    }

    void OnDrawGizmos()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackCollider.position, attackCollider.localScale);
    }


    #endregion

    #region Methods

    /*

              #region Flip Character
              // If the input is moving the player right and the player is facing left...

              if (move > 0 && !m_FacingRight)
              {
                  // ... flip the player.
                  Flip();
              }
              // Otherwise if the input is moving the player left and the player is facing right...
              else if (move < 0 && m_FacingRight)
              {
                  // ... flip the player.
                  Flip();
              }
              #endregion 
     */

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        attackCollider.localPosition = new Vector3(0.05f, attackCollider.localPosition.y, attackCollider.localPosition.z);
        attackCollider.localScale = new Vector3(1f, attackCollider.localScale.y, attackCollider.localScale.z);


    }

    public void Interact() 
    {
        #region Interaction Management
        Vector3 interactPosition = m_Controller.GetComponent<Transform>().position;
        Collider2D[] interactables = Physics2D.OverlapCircleAll(interactPosition, 10f, LayerMask.GetMask("Items", "NPC")); //All npcs are items currently being interacted with
        bool somethingClose = false;
        for (int i = 0; i < interactables.Length; i++)
        {
            bool m_auto = false; //can current interactble be auto-collected
           
            if (interactables[i].GetComponent<Interactable>() != null) //Game objects in "Items or NPC" layer should always contain/inheirt an Interactable component
            {
                Interactable m_interactable = interactables[i].GetComponent<Interactable>(); //grab Interactable compoment from current Interactable
                float distance = Vector2.Distance(interactPosition, interactables[i].transform.position); //calculate distance between player and item positions
                if (distance > 2f)
                {
                    m_interactable.setClose(false); // far from the item
                }
                else
                {
                    m_interactable.setClose(true); //close to the item
                    somethingClose = true;
                    if (interactables[i].GetComponent<Item>() != null)
                    {
                        m_auto = interactables[i].GetComponent<Item>().autocollect; //determine whether or not the item can be autocollected
                        if (m_auto)
                        {
                            Debug.Log(interactables[i].name + "auto");
                        }
                    }


                    if (m_input.interactInput)// if close to the item, collect if "E" is pressed, or if autocollect is enabled
                    {
                        m_interactable.Interact(); //interact with the item
                    }
                }
            }

        }
        if (!somethingClose||DialougeManager.instance.isRunning)
        {
            bottomText.text = "";
        }
        #endregion
    }



    public void TakeDamage(float damage, bool dir) // Player taking damage
    {
        DialougeManager.instance.EndDialouge(); // knock player out of any dialouge

        current_health -= damage; // reduce health
        int direction = dir ? -2 : 2; //knock back direction
        m_Rigidbody2D.velocity = m_FacingRight ? new Vector2(damage / 2 * direction, damage) : new Vector2(damage / 2 * direction, damage); //launch character

        if (current_health <= 0)  // player is dead if health is zero
        {
            print("The way of the samurai.."); //...well right now he is immortal
        }


    }

    #endregion

    #region Coroutines
    IEnumerator Hurt()
    {
        yield return new WaitForSeconds(0.8f); // keep player hp from instantly draining
    }
    IEnumerator decreaseChi()
    {
        yield return new WaitForSeconds(0.8f); // keep player chi from instantly draining
        current_chi -= 0.1f;
    }
    IEnumerator increaseChi()
    {
        yield return new WaitForSeconds(0.8f); // keep player chi from instantly charging
        current_chi += 0.05f;
    }
    IEnumerator AttackRoutine()
    {
        int direction = m_FacingRight ? 1 : -1; // stores player direction

        if (canAttack) //If attack button is pressed
        {
            //play attack animation
            m_Anim.SetBool("Attack",true);
            weapon_Anim.SetBool("Attack", true);

            yield return new WaitForSeconds(windUp); //wait for weapon wind up portion of attack

            #region Extend and Retract weapon
            while (attackCollider.localScale.x<weapon_range) { //Extend attack collider up to weapon range

                yield return new WaitForSeconds((1f/weapon_speed)+0.00001f);// the faster the weapon the shorter the wait, avoid dividing by 0

                // move weapon collider backwards, while increasing it's width
                attackCollider.localPosition = new Vector3(attackCollider.localPosition.x + 0.025f, attackCollider.localPosition.y, attackCollider.localPosition.z); 
                attackCollider.localScale = new Vector3(attackCollider.localScale.x + 0.5f, attackCollider.localScale.y, attackCollider.localScale.z);

                Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackCollider.position, attackCollider.localScale, enemyLayers); //check for damaged enemies

                for (int i = 0; i < hitEnemies.Length; i++) //damage all hit enemies
                {
                    if (hitEnemies[i].GetComponent<Enemy>() != null)
                    {
                        hitEnemies[i].GetComponent<Enemy>().TakeDamage(strength); 
                        Debug.Log("Attacking" + "," + attackCollider.position + "," + attackCollider.localScale);
                    }
                }

            }

            while (attackCollider.localScale.x >= base_range) // Retract weapon colider to initial range
            {
                yield return new WaitForSeconds((1f / weapon_speed) + 0.00001f); // the faster the weapon the shorter the wait, avoid dividing by 0

                //  move weapon collider forwards, while decreasing it's width
                attackCollider.localPosition = new Vector3(attackCollider.localPosition.x - 0.025f, attackCollider.localPosition.y, attackCollider.localPosition.z); 
                attackCollider.localScale = new Vector3(attackCollider.localScale.x - 0.5f, attackCollider.localScale.y, attackCollider.localScale.z);

                Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackCollider.position, attackCollider.localScale, enemyLayers); //check for damaged enemies

                for (int i = 0; i < hitEnemies.Length; i++) //damage all hit enemies
                {
                    if (hitEnemies[i].GetComponent<Enemy>() != null)
                    {
                        hitEnemies[i].GetComponent<Enemy>().TakeDamage(strength); 
                        Debug.Log("Attacking" + "," + attackCollider.position + "," + attackCollider.localScale);
                    }
                }
            }
            #endregion

            //reset base range?
            attackCollider.localPosition = new Vector3(base_range/50f, attackCollider.localPosition.y, attackCollider.localPosition.z);
            attackCollider.localScale = new Vector3(base_range, attackCollider.localScale.y, attackCollider.localScale.z);
            m_Anim.SetBool("Attack", false);
            weapon_Anim.SetBool("Attack", false);
        }
    }




    #endregion
}

