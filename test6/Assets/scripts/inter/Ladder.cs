using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : inter
{
    public bool active = false;
    public CubeMover player;
    public Transform Up;
    public Transform down;

    public IEnumerator Counter()
    {
        while (!Input.GetKeyUp(KeyCode.E))
        {
            yield return null;
        }
        
        active = true;
        player.GetComponent<Rigidbody>().useGravity = false;
        player.CanMove = false;
        Debug.Log("work2");
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            //Debug.Log("ladder");
            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("forward ladder");
                player.transform.position += new Vector3(0, 0.1f, 0);
                if (player.transform.position.y> Up.position.y)
                {
                    Debug.Log("up exit ladder");
                    player.CanMove = true;
                    player.GetComponent<Rigidbody>().useGravity = true;
                    active = false;
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                player.transform.position -= new Vector3(0, 0.1f, 0);

                if (player.transform.position.y < down.position.y)
                {
                    Debug.Log("down exit ladder");
                    player.CanMove = true;
                    player.GetComponent<Rigidbody>().useGravity = true;
                    active = false;
                }
            }

            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("exit ladder");
                player.CanMove = true;
                player.GetComponent<Rigidbody>().useGravity = true;
                active = false;
            }
        }
    }

    public override void Interact(CubeMover mover)
    {
        player = mover;

        //Debug.Log("name:"+transform.name);

        
        StartCoroutine(Counter());
    }
}
