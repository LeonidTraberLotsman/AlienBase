using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdecvEnemy : MonoBehaviour
{
    public BattleManager battle;
    public Transform Player;
    private NavMeshAgent NMAgent;
    CoverPlace my_cover;


    Coroutine  my_routine;
    Coroutine Dela_routine;
    Coroutine Cover_routine;
    private void Awake()
    {
        NMAgent = GetComponent<NavMeshAgent>();
        Dela_routine=StartCoroutine(Dela());
        if(Cover_routine!=null)StopCoroutine(Cover_routine);

    }


    void LeaveCover(){
        if(my_cover!= null) 
        my_cover.CoveredOne=null;
    }


    void Start()
    {
        
    }


    void Update()
    {
       
    }


    public IEnumerator TakeCover()
    {
        
        //Debug.Log(Vector3.Distance()); 
        CoverPlace place=battle.GetNearestCover(transform.position);
        place.CoveredOne=this;
        yield return  ReachPoint(place.transform.position,false);
    }

    public IEnumerator ReachPoint(Vector3 point,bool NeedLeavingCover )
    {
        if(NeedLeavingCover)LeaveCover();
        NMAgent.destination=point;
        while(Vector3.Distance(point,transform.position)>6){
            yield return null;
        }
    }


    public IEnumerator Shoot()
    {
        yield return null;
        NMAgent.destination = Player.position;
        RaycastHit hitinfo;
        if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hitinfo)) 
        {
            Debug.Log("Hit Something"); 
            Debug.DrawRay(transform.position, transform.TransformDirection (Vector3.forward) * hitinfo.distance, Color.red);
            CubeMover that_player = hitinfo.transform.GetComponent<CubeMover>();
            if (that_player)
            {
                that_player.Damage(10);
                yield return new WaitForSeconds(10.6f);
            }
        }
    }

    public IEnumerator Dela()
    {

        Debug.Log("Dela");
        while(true){
            
            yield return TakeCover();          

            yield return new WaitForSeconds(2);

            Coroutine Shoot_routine=StartCoroutine(Shoot());
            yield return new WaitForSeconds(0.6f);
            if(Shoot_routine!= null)StopCoroutine(Shoot_routine);
        }
    }


    public IEnumerator Piu()
    {
      
      yield return null;


        while(true){
        yield return null;
       RaycastHit hitinfo;
        NMAgent.destination = Player.position;
        if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hitinfo)) 
        {
            Debug.Log("Hit Something"); 
            Debug.DrawRay(transform.position, transform.TransformDirection (Vector3.forward) * hitinfo.distance, Color.red);
            CubeMover that_player = hitinfo.transform.GetComponent<CubeMover>();
            if (that_player)
            {
                that_player.Damage(10);
                yield return new WaitForSeconds(10.6f);
            }
        }

        }
        
    }
}
