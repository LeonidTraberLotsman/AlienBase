using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdecvEnemy : MonoBehaviour
{

    public int HP = 100;

    public ParticleSystem shot;

    public GameObject DebugCube;

    public Animator animator;

    public Transform center;
    public enum Role
    {
        common,
        insane,
        afraid,
        fresh
    }
    public Role role=Role.fresh;

    List<Rigidbody> rigidbodies = new List<Rigidbody>();

    public float speed;
    public BattleManager battle;
    public Transform Player;
    private NavMeshAgent NMAgent;
    CoverPlace my_cover;
    public List<AudioClip> clips;

    AudioSource source;

    Coroutine  my_routine;
    Coroutine Tactic_routine;
    Coroutine Cover_routine;

    void AddAllRigidbodies(Transform t)
    {
        Rigidbody rigidbody = t.GetComponent<Rigidbody>();
        if (rigidbody != null) rigidbodies.Add(rigidbody);

        for (int i = 0; i < t.childCount; i++)
        {
            AddAllRigidbodies(t.GetChild(i));
        }
    }
    public void ChangeRole(Role new_role)
    {

        if (role == new_role) return;
        if(Tactic_routine!=null) StopCoroutine(Tactic_routine);

        role = new_role;
        if (new_role == Role.common)
        {
            transform.name = "common alien";
            Tactic_routine = StartCoroutine(CommonTactic());
            DebugCube.GetComponent<Renderer>().material.color =new Color(0,0,1,0.4f) ;
        }
        if (new_role == Role.insane)
        {
            transform.name = "Insane alien";
            Tactic_routine = StartCoroutine(InsaneTactic());
            DebugCube.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.4f);
        }
        if (new_role == Role.afraid)
        {
            transform.name = "afraid alien";
            Tactic_routine = StartCoroutine(AfraidTactic());
            DebugCube.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0.4f);
        }
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if (battle == null)
        {
           
            battle = GameObject.FindGameObjectsWithTag("GreatManager")[0].GetComponent<BattleManager>();
            if (battle != null)
            {
                Player = battle.player;

            }
        }
        battle.enemies.Add(this);


        NMAgent = GetComponent<NavMeshAgent>();
        //Tactic_routine=StartCoroutine(CommonTactic());
        if(Cover_routine!=null)StopCoroutine(Cover_routine);

    }

    public void GetDamage(int damage)
    {
        Die();
    }
    void Die()
    {
        animator.enabled = false;

        if(Tactic_routine!=null)StopCoroutine(Tactic_routine);
        NMAgent.speed = 0;
        NMAgent.enabled = false;
        //transform.Rotate(-Vector3.right * 90, Space.Self);
        //transform.position -= new Vector3(0, 0.65f);
        GetComponent<Collider>().enabled = false;
        center.parent = null;
        foreach (Rigidbody body in rigidbodies)
        {
            body.isKinematic = false;
            body.useGravity = true;
        }
        StartCoroutine(MoveCorpse());

    }
    IEnumerator MoveCorpse()
    {
        Rigidbody body = center.GetComponent<Rigidbody>();
        while (true)
        {
            body.AddForce(Vector3.up * 20);
            yield return null;
        }
    }
    public static Transform GetParent(Transform it)
    {
        Transform res = it;
        while (res.parent != null)
        {
            res = res.parent;
        }
        return res;
    }

    void LeaveCover(){
        animator.SetBool("Crouch", false);
        if (my_cover!= null) 
        my_cover.CoveredOne=null;
        my_cover = null;
        
    }


    void Start()
    {
        AddAllRigidbodies(transform);

        foreach (Rigidbody body in rigidbodies)
        {
            body.isKinematic = true;
            body.useGravity = false;
        }
    }


    void Update()
    {
       
    }


    public IEnumerator TakeCover()
    {
        animator.SetBool("Crouch", false);
        Debug.Log(battle); 
        CoverPlace place=battle.GetNearestCover(transform.position);
        place.CoveredOne=this;
        my_cover = place;
        yield return  ReachPoint(place.transform.position,false);
        animator.SetBool("Crouch", false);
    }

    public IEnumerator ReachPoint(Vector3 point,bool NeedLeavingCover )
    {
        float dis = Vector3.Distance(transform.position, point);
        Vector3 direction = -transform.position + point;
        Debug.DrawRay(transform.position, direction,new Color(0.5f,0.5f,0,0.5f),dis);

        if (NeedLeavingCover)LeaveCover();
        NMAgent.destination=point;

        animator.SetBool("Walking", true);

        while (Vector3.Distance(point,transform.position)>6){
            yield return null;
        }
        animator.SetBool("Walking", false);
    }

    bool CanSee()
    {
        RaycastHit hitinfo;
        Vector3 direct = Player.position - (transform.position + transform.forward * 3);
        if (Physics.Raycast(transform.position + transform.forward * 3, direct, out hitinfo))
        //if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hitinfo)) 
        {

            //GameObject that_cube = Instantiate(DebugCube);
            //that_cube.transform.position = hitinfo.point;

            //battle.AddText("target = " + hitinfo.transform.name);
            //Debug.Log("Hit Something"); 
          
            CubeMover that_player = hitinfo.transform.GetComponent<CubeMover>();
            if (that_player)
            {
                // battle.AddText("0_0");
                //yield return new WaitForSeconds(10.6f);
                return true;
                //yield return new WaitForSeconds(10.6f);
            }
        }
        return false;
    }


    void PlaySound(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    IEnumerator findTheTarget()
    {
        yield return null;
        while (!CanSee())
        {
            yield return null;
            NMAgent.destination = Player.position;

        }
    }
    public IEnumerator Shoot()
    {
        yield return null;
        LeaveCover();
        animator.SetBool("Walking", true);
        NMAgent.destination = Player.position;
        

        yield return new WaitForSeconds(0.5f);
        PlaySound(clips[0]);

        RaycastHit hitinfo;

        Vector3 direct = Player.position - (transform.position + transform.forward * 3);

        shot.Play();
        if (Physics.Raycast (transform.position+transform.forward*3, direct, out hitinfo)) 
        //if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hitinfo)) 
        {

            //GameObject that_cube = Instantiate(DebugCube);
            //that_cube.transform.position = hitinfo.point;

            //battle.AddText("target = " + hitinfo.transform.name);
            //Debug.Log("Hit Something"); 
            Debug.DrawRay(transform.position, transform.TransformDirection (Vector3.forward) * hitinfo.distance, Color.red);
            CubeMover that_player = hitinfo.transform.GetComponent<CubeMover>();
            if (that_player)
            {
               // battle.AddText("0_0");
                //yield return new WaitForSeconds(10.6f);
                that_player.Damage(1);
                //yield return new WaitForSeconds(10.6f);
            }
        }
    }

    public IEnumerator CommonTactic()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("CommonTactic");
        while(true){

            yield return TakeCover();
            yield return new WaitForSeconds(1);
            //NMAgent.speed = 0.01f;
            //NMAgent.destination = Player.position;
            yield return new WaitForSeconds(2);
            NMAgent.speed = speed;

            yield return findTheTarget();
            Coroutine Shoot_routine=StartCoroutine(Shoot());
            yield return new WaitForSeconds(1.2f);
            //yield return new WaitForSeconds(5.6f);
            if(Shoot_routine!= null)StopCoroutine(Shoot_routine);
            
        }
    }

    public IEnumerator InsaneTactic()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Walking", true);
        Debug.Log("InsaneTactic");
        while (true)
        {

            

            Coroutine Shoot_routine = StartCoroutine(Shoot());
            yield return new WaitForSeconds(0.6f);
            if (Shoot_routine != null) StopCoroutine(Shoot_routine);
        }
    }

    public IEnumerator AfraidTactic()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("AfraidTactic");
        while (true)
        {

        
            while (Vector3.Distance(Player.transform.position,transform.position)<30)
            {

                Vector3 d = Player.transform.position - transform.position;
                NMAgent.destination = transform.position - d;
                yield return new WaitForSeconds(2);


            }

            TakeCover();
            yield return new WaitForSeconds(Random.Range(0.0f,5.0f));
            LeaveCover();

            Coroutine Shoot_routine = StartCoroutine(Shoot());
            yield return new WaitForSeconds(0.6f);
            if (Shoot_routine != null) StopCoroutine(Shoot_routine);
        }
    }




}
