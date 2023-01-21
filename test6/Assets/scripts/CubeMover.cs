using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMover : MonoBehaviour
{
    public int hp = 100;
    public Text HPtext;
    bool CanMove = true;


    public Button RestartButton; 
    public head my_head; 

    public Text GameOverText;   

    Rigidbody body;
    // Start is called before the first frame update
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

    public void Damage(int dam)
    {
        hp -= dam;
        if (hp < 0)
        {
            Die();
        }
        ShowHP();
    }
    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            if (Input.GetKey(KeyCode.W))
            {
                body.AddForce(transform.forward * 50);

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
                body.AddForce(transform.up * 500);

            }
        }
    }
}
