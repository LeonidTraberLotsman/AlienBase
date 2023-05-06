using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBuilding : GreyBuilding
{
    // Start is called before the first frame update
    public override void Generate(GreatGenerator generator)
    {
        Debug.Log("generated");
        greatGenerator = generator;

        
    }
}
