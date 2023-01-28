using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatGenerator : MonoBehaviour
{

    public List<Transform> points;

    public List<GameObject> grey_prefabes;


    void DoArchitcture() {
        //TODO choose 3 points for mac guffins


        foreach (Transform point in points)
        {
            spawnGrey(point);
        }
    
    }

    void Start()
    {
        DoArchitcture();
    }

    void spawnGrey(Transform point)
    {
        GameObject building = Instantiate(grey_prefabes[0]);

        building.transform.position = point.position;

        building.GetComponent<GreyBuilding>().Generate(this);
    }
}
