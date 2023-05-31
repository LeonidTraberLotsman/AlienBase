using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatGenerator : MonoBehaviour
{
    public GameObject MarkerPrefab;

    public Transform CityCenter;
    public GameObject road_prefab;

    public GameObject tree_prefab;
    public int minTreeDistance;
    public int maxTreeDistance;


    public int d2;

    public List<Vector3> points;
    public List<building_point> build_points= new List<building_point>();

    public List<GameObject> grey_prefabes;

    public List<AudioClip> clips;
    AudioSource source;

    public class building_point
    {

        public Vector3 point;
        public bool state;
        public building_point(Vector3 point, bool state)
        {
            this.point = point;
            this.state = state;
        }       
    }

    public GameObject RedBuildingPrefab;

    public List<road_part> roads= new List<road_part>();
    public List<cross_road> crosses= new List<cross_road>();

    BattleManager battle;
    void SpawnOneTree()
    {
        float x = 0;
        float y = 0;
        bool OK = true;
        while (OK)
        {
             x = Random.Range(-maxTreeDistance, maxTreeDistance);
             y = Random.Range(-maxTreeDistance, maxTreeDistance);
            if (Mathf.Sqrt(x * x + y * y) > minTreeDistance)
            {
                OK = false;
            }
        }
        


        GameObject newTree = Instantiate(tree_prefab);
        tree_prefab.transform.position=CityCenter.transform.position+ new Vector3(x,5,y);
    }
    public void SpawnTrees()
    {
        for (int i = 0; i < 100; i++)
        {
            SpawnOneTree();
        }
        
    }

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

            Vector3 road_point= new Vector3(0, 0, 35);
            if (side == sides.south)
            {
                road_point = new Vector3(0, 0, -35);
            }

            if (side == sides.west)
            {
                turned = true;
                road_point = new Vector3(-35, 0, 0);
            }
            if (side == sides.east)
            {
                turned = true;
                road_point = new Vector3(35, 0, 0);
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
        public GameObject road;

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

        foreach (road_part roadT in roads)
        {
            if (Vector3.Distance(roadT.road.transform.position, point) < 14)
            {
                return null;
            }
        }

        GameObject LocalRoad = Instantiate(road_prefab);
        LocalRoad.transform.position = point;

        if (turned)
        {
            LocalRoad.transform.Rotate(new Vector3(0, 90, 0));
            //LocalRoad.GetComponent<Renderer>().material.color = Color.red;
        }

        


        road_part road = new road_part(cross, LocalRoad);

        roads.Add(road);

        cross_road second_cross= new cross_road(new_cross_point);
        crosses.Add(second_cross);
        road.second_cross = second_cross;

        //spawnGrey(point,turned);
        building_point example = new building_point(point, turned);
        build_points.Add(example);
        //points.Add(point);

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
        
        
    }
    void DoArchitcture() {

        GenerateGrid();
        SpawnTrees();
        //TODO choose 3 points for mac guffins
        int redsToSpawn=3;
        while(redsToSpawn>0)
        {

            int n = Random.Range(0, build_points.Count - 1);

            Debug.Log("Count:" + build_points.Count.ToString());

            Debug.Log("Random:" + n.ToString());

            building_point build = build_points[n];
            if (!build.state)
            {
                SpawnBuilding(build.point, true, RedBuildingPrefab);
                redsToSpawn--;
                build_points.Remove(build);
            }
            //points.Remove(point);
        }

        foreach (building_point build in build_points)
        {
            spawnGrey(build.point, build.state);
            //if (!build.state)
            //{
            //    GameObject marker = Instantiate(MarkerPrefab);
            //    marker.transform.position = build.point;
            //} 
            
        }
        
        Debug.Log("w" + build_points.Count.ToString());


    }

    void Start()
    {
        DoArchitcture();
        battle = GetComponent<BattleManager>();
        StartCoroutine(battle.SpawnCheck());

        source = GetComponent<AudioSource>();
        StartCoroutine(music());
    }

    IEnumerator music()
    {
        while (true)
        {
            
            yield return null;

            foreach(AudioClip item in clips)
            {
                source.clip = item;
                source.Play();
                yield return new WaitForSeconds(item.length+1);
            }
        }
    }
    void SpawnBuilding(Vector3 point, bool turned,GameObject prefab)
    {
        if (turned)
        {
            //d++;
            GameObject building = Instantiate(prefab);
            building.transform.position = point + new Vector3(-25, 0, 0);
            //building.transform.Rotate(Vector3.up * 90);

            building.GetComponent<GreyBuilding>().Generate(this);
        }
        else
        {
        //    GameObject building = Instantiate(prefab);
        //    building.transform.position = point + new Vector3(0, 0, 25);
        //    building.transform.Rotate(Vector3.up * 90);

        //    building.GetComponent<GreyBuilding>().Generate(this);
        }
    }
    void spawnGrey(Vector3 point,bool turned)
    {

        SpawnBuilding(point,!turned, grey_prefabes[Random.Range(0, grey_prefabes.Count)]);
       
    }
}
