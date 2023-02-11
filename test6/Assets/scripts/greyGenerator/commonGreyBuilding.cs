using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commonGreyBuilding : GreyBuilding
{
    public GameObject center;

    public float DistanceToWall=1;

    public GameObject BedPrafab;
    public GameObject chairPrafab;
    

    public List<GameObject> FurnitureToSpawn;

    public Transform point;
    //vika zaripova
    public override void Generate(GreatGenerator generator)
    {
        Debug.Log("generated");
        greatGenerator = generator;

        //GameObject thatone = Instantiate(TablePrafab);
        //thatone.transform.position= point.position;
        spawnFurniture();
    }

    void spawnFurniture()
    {
        foreach (GameObject prefab in FurnitureToSpawn)
        {
            GameObject furniture = Instantiate(prefab);

            furniture.transform.position= center.transform.position + new Vector3(Random.Range(0, DistanceToWall), Random.Range(0,DistanceToWall), Random.Range(0, DistanceToWall));
        }
    }
}
