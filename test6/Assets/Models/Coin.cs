
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class oin : MonoBehaviour
{
    public GameObject player;
    public GameObject vrag;
    public float speed;
 
    // Start is called before the first frame update
    void Start()
    {
       
    }
 
    // Update is called once per frame
    void Update()
    {
        Vector2 player1 = player.transform.position;
        Vector2 vrag1 = vrag.transform.position;
 
        if (Vector2.Distance(player1, vrag1) < 16)
        {
            vrag.transform.position = Vector2.MoveTowards(vrag1, player1, speed * Time.deltaTime);
        }
    }
}
