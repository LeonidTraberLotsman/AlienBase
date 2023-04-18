using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingGreyBuilding : GreyBuilding
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Generate(GreatGenerator generator)
    {
        Debug.Log("Parking house generated");
        greatGenerator = generator;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
