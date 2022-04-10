using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int type;

    public float range;
    public float speed;
    public float strength;
    public float windUp;
    public RuntimeAnimatorController m_anim;
 



    public override void Use()
    {

        
        m_player = Player.instance;
        m_player.weapon_range = range;
        m_player.weapon_speed = speed;
        m_player.strength += strength;
        m_player.windUp = windUp;

        m_player.m_Controller.gameObject.transform.Find("weapon").gameObject.GetComponent<Animator>().runtimeAnimatorController = m_anim;
        m_player.attackMode=type;

    }

    


}
