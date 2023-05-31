using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Timeline;

public class helicopterScript : MonoBehaviour
{

    Animator animator;

    public Camera my_camera;

    public Transform focus;

    public Transform RandevousPoint;

    public Text starterText;


    public bool EvacuateNeeded = false;

    public GameObject Player;
    CubeMover mover;
    Vector3 place;

    public GameObject playersHead;

    Coroutine greatStartCoroutine;
    Coroutine quickStartCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        mover = Player.GetComponent<CubeMover>();
        animator = GetComponent<Animator>();
        animator.SetBool("ShouldSit", true);
        animator.SetBool("Should", true);

        greatStartCoroutine = StartCoroutine(GreatStart());
        quickStartCoroutine = StartCoroutine(quickStart());


    }

    IEnumerator quickStart()
    {
        while (!Input.GetKeyUp(KeyCode.Return))
        {
            yield return null;
        }
        Debug.Log("quickStarted");
        mover.Lock(true);
        starterText.enabled = false;
        Player.transform.position = RandevousPoint.position + Vector3.up * 2;
        Player.transform.parent = null;
        my_camera.enabled = false;
    }

    IEnumerator PreJumpChecker()
    {
        yield return null;




        

        while (Vector3.Distance(transform.position, Player.transform.position) < 5)
        {
            yield return null;
        }
        Player.GetComponent<Rigidbody>().AddForce(-20*Vector3.up);
        Player.GetComponent<Rigidbody>().drag = 0.1f;
        Debug.Log("Away");
        Player.transform.parent = null;
        animator.SetBool("Should", false);

        if (greatStartCoroutine != null) StopCoroutine(greatStartCoroutine);
        if (quickStartCoroutine != null) StopCoroutine(quickStartCoroutine);

        yield return new WaitForSeconds(2);
        yield return Move(transform, place, 4000);
        StartCoroutine(Waiting());
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

    IEnumerator TextFlow()
    {
        starterText.enabled = true;

        for (float i = 0; i < 450; i += 1f)
        {
            starterText.gameObject.GetComponent<RectTransform>().position+=new Vector3(0,4);
            yield return null;
            
        }
        starterText.enabled = false;
    }

    IEnumerator GreatStart()
    {
        mover.Lock(false);
        StartCoroutine(TextFlow());
        place = transform.position;
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
        mover.Lock(true);

        Destroy(my_camera);
        animator.SetBool("ShouldSit", false);
        Time.timeScale = 1;
        StartCoroutine(PreJumpChecker());
        yield return Move(transform, RandevousPoint.position+Vector3.up*20, 400);

        
    }

    IEnumerator Waiting()
    {
        while (!EvacuateNeeded)
        {
            yield return Circle();
        }
        yield return Move(transform, RandevousPoint.position + Vector3.up * 20, 400);
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
