using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bocha : MonoBehaviour
{
    public Transform destroyed;
    public bool ror;
    public bool fof = false;
         
    public void Dead()
    {
         
        Transform r =Instantiate(destroyed, transform.position, transform.rotation);


        
        Destroy(gameObject);

    }

    private void Start()
    {
        transform.parent = null;
    }
    public void GetDamage()
    {
        Dead();
    }


}
