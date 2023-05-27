using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBeDestroyed : MonoBehaviour
{
    public Transform destroyed;
    public bool ror;
    public bool fof = false;
    public void Dead()
    {
        
        Transform r =Instantiate(destroyed, transform.position, transform.rotation);
        r.Rotate(Vector3.up * 90);
        Destroy(gameObject);
    }



    void Update()
    {
        if( fof)
        {
            Dead();
        }

    }    
        

}
