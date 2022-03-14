using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : Item
{
    public float recovery;
    public override void Collect()
    {
        base.Collect();
        consumable = true;


    }



    public override void Use()
    {
        base.Use();
        m_player.current_health += recovery;

    }

}
