using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderHouse : GreyBuilding
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Generate(GreatGenerator generator)
    {
        Debug.Log("Ladder house generated");
        greatGenerator = generator;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
