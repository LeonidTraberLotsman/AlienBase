using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Transform player;
    public List<CoverPlace> covers;
    public List<AdecvEnemy> enemies;
    public List<AdecvEnemy> insaners;
    public List<AdecvEnemy> afraids;

    public CoverPlace GetNearestCover(Vector3 point){

        Debug.Log("GetNearestCover");



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
        Debug.Log("GetNearestCover end");
        return candidate;

    }

    IEnumerator tacticRefresher()
    {
        yield return null;
        while (true)
        {
            foreach (AdecvEnemy guy in enemies.ToArray())
            {
                if (guy == null) {
                    enemies.Remove(guy);
                    continue;

                }
                if (guy.HP < 0)
                {
                    enemies.Remove(guy);
                    continue;
                }
            }

            foreach (AdecvEnemy guy in insaners.ToArray()) if (!enemies.Contains(guy)) insaners.Remove(guy);
            foreach (AdecvEnemy guy in afraids.ToArray()) if (!enemies.Contains(guy)) afraids.Remove(guy);

            if (insaners.Count == 0 && enemies.Count > 4) for (int i = 0; i < Random.Range(1,3); i++)
                {
                    int n = Random.Range(0, enemies.Count);
                    enemies[n].ChangeRole(AdecvEnemy.Role.insane);
                    insaners.Add(enemies[n]);
                }

            if (afraids.Count == 0 && enemies.Count > 8) for (int i = 0; i < Random.Range(1, 3); i++)
                {
                    int n = Random.Range(0, enemies.Count);
                    enemies[n].ChangeRole(AdecvEnemy.Role.afraid);
                    afraids.Add(enemies[n]);
                }

            foreach (AdecvEnemy guy in enemies.ToArray())
            {
                if (!afraids.Contains(guy) && !insaners.Contains(guy)) guy.ChangeRole(AdecvEnemy.Role.common);
            }

            yield return new WaitForSeconds(2);

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(tacticRefresher());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
