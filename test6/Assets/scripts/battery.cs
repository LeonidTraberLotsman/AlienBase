using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battery : MonoBehaviour
{
    public int hpToHeal = 10;
    public bool Useful = true;
    public GameObject pl;

    public bool isMed = true;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision col)
    {
        Debug.Log("med kit touched");
        if (!Useful) return;
        CubeMover player = col.gameObject.GetComponent<CubeMover>();

        if (player!=null)
        {
            if(isMed)
            {
                Debug.Log("med kit touched by player");
                if (player.hp == 100) return;
                player.Heal(hpToHeal);
            }
            else
            {
                player.AddAmmo(hpToHeal);
            }
            Useful = false;
            Destroy(pl);
        }
    }
}
