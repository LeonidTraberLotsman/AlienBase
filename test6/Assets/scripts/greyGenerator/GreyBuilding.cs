using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class GreyBuilding : MonoBehaviour
{

    public GreatGenerator greatGenerator;
    public virtual void Generate(GreatGenerator generator) {
        Debug.Log("generated");
        greatGenerator = generator;
    }
}
