using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commonGreyBuilding : GreyBuilding
{
    public GameObject TablePrafab;
    public GameObject BedPrafab;
    public GameObject chairPrafab;
    public GameObject ShellPrafab;

    public Transform point;
    //vika zaripova
    public override void Generate(GreatGenerator generator)
    {
        Debug.Log("generated");
        greatGenerator = generator;

        GameObject thatone = Instantiate(TablePrafab);
        thatone.transform.position= point.position;

    }
}
