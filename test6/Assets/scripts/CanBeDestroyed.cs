using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBeDestroyed : MonoBehaviour
{
    public Transform destroyed;

    public void Dead()
    {
        Instantiate(destroyed, transform.position, transform.rotation); 
        Destroy(gameObject);
    }
}
