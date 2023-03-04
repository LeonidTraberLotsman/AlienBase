using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatGenerator : MonoBehaviour
{
    public Transform CityCenter;
    public GameObject road_prefab;


    public List<Transform> points;

    public List<GameObject> grey_prefabes;

    public List<road_part> roads= new List<road_part>();
    public List<cross_road> crosses= new List<cross_road>();
    public class cross_road
    {
        Vector3 position;

        enum sides
        {
            North,
            south,
            west,
            east
        }

        road_part north_road;
        road_part south_road;
        road_part west_road;
        road_part east_road;

        public cross_road(Vector3 point)
        {
        position=point;
        }
        public road_part spawnRoad(GreatGenerator gen)
        {
            road_part road = null;

            List<sides> potentials = new List<sides>();
            potentials.Add(sides.North);
            potentials.Add(sides.south);
            potentials.Add(sides.west);
            potentials.Add(sides.east);

            if (north_road!=null)
            {
                potentials.Remove(sides.North);
            }
            if (south_road != null)
            {
                potentials.Remove(sides.south);
            }
            if (west_road != null)
            {
                potentials.Remove(sides.west);
            }
            if (east_road != null)
            {
                potentials.Remove(sides.east);
            }
            if (potentials.Count == 0) return null;
            int n = Random.Range(0, potentials.Count);
            Debug.Log(n);

            sides side = potentials[n];
            Debug.Log(side);
            bool turned = false;

            Vector3 road_point= new Vector3(0, 0, 30);
            if (side == sides.south)
            {
                road_point = new Vector3(0, 0, -30);
            }

            if (side == sides.west)
            {
                turned = true;
                road_point = new Vector3(-30, 0, 0);
            }
            if (side == sides.east)
            {
                turned = true;
                road_point = new Vector3(30, 0, 0);
            }

            road = gen.spawnRoad(position + road_point, position + 2*road_point,turned, this);
            /*road_part north_road;
        road_part south_road;
        road_part west_road;
        road_part east_road;*/
            if (side == sides.North)north_road = road;
            if (side == sides.south) south_road = road;
            if (side == sides.west) west_road = road;
            if (side == sides.east) east_road = road;
            

            

            return road;
        }
    }

    public class road_part
    {
        GameObject road;

        public cross_road first_cross;
        public cross_road second_cross;

        public road_part(cross_road cross,GameObject spawned_road)
        {
            road = spawned_road;
            first_cross = cross;
        }
        
    }

    
    road_part spawnRoad(Vector3 point, Vector3 new_cross_point,bool turned, cross_road cross)
    {
        GameObject LocalRoad = Instantiate(road_prefab);
        LocalRoad.transform.position = point;

        if (turned)
        {
            LocalRoad.transform.Rotate(new Vector3(0, 90, 0));
        }

        road_part road = new road_part(cross, LocalRoad);

        roads.Add(road);

        cross_road second_cross= new cross_road(new_cross_point);
        crosses.Add(second_cross);
        road.second_cross = second_cross;

        return road;
    }

    void GenerateGrid()
    {
        cross_road cross1 = new cross_road(CityCenter.position);
        crosses.Add(cross1);
        for (int i = 0; i < 50; i++)
        {
            bool t = true;
            while (t)
            {
                int n = Random.Range(0, crosses.Count);
                road_part r = crosses[n].spawnRoad(this);
                if (r != null) t = false;
            }
            
        }

        cross1.spawnRoad(this);
        cross1.spawnRoad(this);
        cross1.spawnRoad(this);
        cross1.spawnRoad(this);
        
        Debug.Log("Grid");
        
    }
    void DoArchitcture() {

        GenerateGrid();
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
