using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    CubeMover PlayerScript;
    Coroutine cor;

    public int HP = 100;

    // Start is called before the first frame update
    void Start()
    {
           agent = GetComponent<NavMeshAgent>();
        PlayerScript = player.GetComponent<CubeMover>();
        cor=StartCoroutine(CatchPlayer());
    }

    public void Damage(int damage_count)
    {
        HP = HP - damage_count;
        if (HP < 1)
        {
            Die();
        }
    }

    public void Die()
    {
        StopCoroutine(cor);

        agent.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;

        transform.Rotate(new Vector3(10, 10, 10));

         //Destroy(this.gameObject);
    }
    IEnumerator CatchPlayer()
    {
        while (true){
            agent.destination = player.position;
            yield return null;
            if (Vector3.Distance(player.position, transform.position) < 10)
            {
                
                PlayerScript.Damage(5);
                yield return new WaitForSeconds(7);
            }
            

        }
    }
   
}
