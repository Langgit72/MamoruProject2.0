using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothing : Item
{

    public RuntimeAnimatorController m_anim;


    public override void Collect()
    {
        base.Collect();
        
    }



    public override void Use()
    {
        Debug.Log("well yes" + m_anim + "but actually" + m_player.gameObject.GetComponent<Animator>().runtimeAnimatorController);
        m_player.gameObject.GetComponent<Animator>().runtimeAnimatorController = m_anim;
        

    }
}
