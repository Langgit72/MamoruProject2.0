using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public Player player;
    public RuntimeAnimatorController playerAnimator;

    public float savedTime;


    public 

    void Awake()
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

        #endregion

        if (playerAnimator != null) {
            Player.instance.m_Anim.runtimeAnimatorController = playerAnimator;
        }
        DontDestroyOnLoad(gameObject);

    }


    // Update is called once per frame
    void Update()
    {
        savedTime = DayNightCycle.instance.startTime + Time.deltaTime;
        playerAnimator = Player.instance.GetComponent<Animator>().runtimeAnimatorController;


    }
}
