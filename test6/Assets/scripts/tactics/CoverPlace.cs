using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPlace : MonoBehaviour
{
    public AdecvEnemy CoveredOne;
    // Start is called before the first frame update
    void Start()
    {
       
            
            BattleManager battle = GameObject.FindGameObjectsWithTag("GreatManager")[0].GetComponent<BattleManager>();
        //Debug.Log(battle.transform.name);
        if(!battle.covers.Contains(this)) battle.covers.Add(this);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
