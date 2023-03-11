using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Transform player;
    public List<CoverPlace> covers;


    public CoverPlace GetNearestCover(Vector3 point){
        CoverPlace candidate=covers[0];
        float Max=100000000;
        foreach(CoverPlace place in covers){
                Debug.Log("BBB");
                if(place==null) continue;
                Debug.Log("BBB2");
                if(place.CoveredOne!=null) {
                    Debug.Log("там уже кто-то есть");
                    continue;
                    
                    }//если там уже кто-то есть
                    else{
                        Debug.Log(place.CoveredOne);
                    }

                float dist= Vector3.Distance(place.transform.position, point);
                if(dist<Max){
                    Max=dist;
                     Debug.Log("chosen");
                    candidate=place;
                }
        }
        return candidate;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
