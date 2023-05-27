using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomHouseB : GreyBuilding
{
    // Start is called before the first frame update
    public List<Transform> stols = new List<Transform>();
    void Start()
    {
        foreach (var stol in stols)
        {
            stol.parent = null;
        }
    }
    public override void Generate(GreatGenerator generator) { 
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
