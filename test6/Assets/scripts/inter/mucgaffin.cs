using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mucgaffin : inter
{

    BattleManager battle;

    // Start is called before the first frame update
    void Start()
    {
        if (battle == null)
        {

            battle = GameObject.FindGameObjectsWithTag("GreatManager")[0].GetComponent<BattleManager>();
           
        }
        battle.mucgaffins.Add(this);
        //battle.enemies.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void Interact(CubeMover mover)
    {
        battle.mucgaffins.Remove(this);
        battle.GetMucgaffin();
        Destroy(gameObject);
    }

}
