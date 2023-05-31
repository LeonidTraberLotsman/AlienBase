using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMover : MonoBehaviour
{
    public int hp = 100;
    public Text HPtext;
    public bool CanMove = true;

   

    public bool jumpTrueAndFalse = true;


    public Button RestartButton; 
    public head my_head; 

    public Text GameOverText;   

    Rigidbody body;
    void Start()
    {
        GameOverText.enabled = false;   
        ShowHP();
        body = GetComponent<Rigidbody>();
    }


    public void ShowHP()
    {

        HPtext.text = hp.ToString();
        
    }

    void Die()
    {
        GameOverText.enabled = true;
        RestartButton.gameObject.SetActive(true);
        Debug.Log("player is dead");
        CanMove = false;
        my_head.CanMove = false;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Heal(int Health)
    {
        Debug.Log("Heal");

        hp += Health;
        if (hp > 100) hp = 100;
        ShowHP();
    }

    public void AddAmmo(int AmmoToAdd)
    {
        Debug.Log("BagAmmo");

        my_head.BagAmmo += AmmoToAdd;

        my_head.ShowAmmo();
    }
    public void Damage(int dam)
    {
        hp -= dam;
        if (hp < 0)
        {
            Die();
        }
        ShowHP();
    }
    void Update()
    {
        int speed = 1;
        if (CanMove)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                 speed = 15;
            }
            if (Input.GetKey(KeyCode.W))
            {
                body.AddForce(transform.forward * 50*speed);
               
            }
            if (Input.GetKey(KeyCode.S))
            {
                body.AddForce(-transform.forward * 50);

            }
            if (Input.GetKey(KeyCode.D))
            {
                body.AddForce(transform.right * 50);

            }
            if (Input.GetKey(KeyCode.A))
            {
                body.AddForce(-transform.right * 50);

            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (jumpTrueAndFalse == true)
                {
                    body.AddForce(transform.up * 500);
                    jumpTrueAndFalse = false;
                }
            }
        }
    }


    public void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.layer == 3)
        {
            body.drag = 4;
            jumpTrueAndFalse = true;
        }
    }

    public void Lock(bool state)
    {
        CanMove = my_head.CanMove = state;
    }
    //public void OnTriggerEnter(Collision col)
    //    if (col.gameObject.layer == 8)
    //    {

    //        body.AddForce(Vector3.up * 500);
    //    }
    //}
}
