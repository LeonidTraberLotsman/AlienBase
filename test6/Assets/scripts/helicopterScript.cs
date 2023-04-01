using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class helicopterScript : MonoBehaviour
{

    Animator animator;

    public Camera my_camera;

    public Transform focus;

    public Transform RandevousPoint;

    public GameObject Player;
    public GameObject playersHead;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("ShouldSit", true);
        animator.SetBool("Should", true);

        StartCoroutine(GreatStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Circle()
    {
        for (float i = 0; i < 360; i += 1f)
        {
            yield return null;
            transform.Rotate(-Vector3.up);
            transform.position += 10 * transform.forward * 0.94f;
        }
    }

    IEnumerator GreatStart()
    {

        //Time.timeScale = 7;
        for (int i = 0; i < 50; i++)
        {

            transform.position -= 10 * transform.forward * 0.94f;
        }
        for (int i = 0; i < 50; i++)
        {
            yield return null;
            transform.position +=10* transform.forward * 0.94f;
        }
        yield return Circle();


        focus.transform.position = my_camera.transform.position+ 4*my_camera.transform.forward;
        my_camera.transform.LookAt(focus);
        Vector3 focus_place = playersHead.transform.position + 4 * playersHead.transform.forward;

        StartCoroutine(Move(focus.transform, focus_place, 60));
        StartCoroutine(Move(my_camera.transform, playersHead.transform.position, 60));

        for (int i = 0; i < 60; i++)
        {
            yield return null;
            my_camera.transform.LookAt(focus);
        }

        Destroy(my_camera);
        animator.SetBool("ShouldSit", false);
        Time.timeScale = 1;
        Vector3 place = transform.position;
        yield return Move(transform, RandevousPoint.position+Vector3.up*20, 400);





        Player.transform.parent = null;
        while (Vector3.Distance(transform.position, Player.transform.position) < 10)
        {
            yield return null;
        }
        animator.SetBool("Should", false);

        yield return new WaitForSeconds(2);
        yield return Move(transform, place, 4000);
        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        while (true)
        {
            yield return Circle();
        }
    }

    IEnumerator Move(Transform who,Vector3 where,int frames)
    {
        Vector3 v = where - who.transform.position;
        v = v / frames;
        for (int i = 0; i < frames; i++)
        {
            yield return null;
            who.transform.position += v;
        }
    }
}