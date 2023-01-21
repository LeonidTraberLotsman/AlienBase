using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject prefab;

    public Transform Player;//

    void SpawnDog()
    {
        GameObject new_dog = Instantiate(prefab);
        new_dog.transform.position = transform.position+ new Vector3(Random.Range(-15f,15f),0, Random.Range(-15f, 15f));

        new_dog.GetComponent<enemy>().player = Player;//
        new_dog.GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        Debug.Log("called");
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            SpawnDog();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
