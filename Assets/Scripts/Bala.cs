using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    float speed = 20;
    Rigidbody2D myBody;
    GameObject player;
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("MegaMan");

        if (player.transform.localScale[0] == 1)
        {
            myBody.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
        }
        else
        {
            myBody.AddForce(new Vector2(-speed, 0), ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    void destruir()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground" || collision.gameObject.tag == "Enemy")
        {
            myAnimator.SetBool("destroy", true);
        }
    }
}
