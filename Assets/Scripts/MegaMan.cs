using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaMan : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] GameObject bala;
    [SerializeField] float NextFire;

    Animator myAnimator;
    Rigidbody2D myBody;
    BoxCollider2D myCollider;
    bool dobleSalto;
    float time = 0.0f;
    bool count;
    float canfire, tamY, tamX;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        tamY = (GetComponent<SpriteRenderer>()).bounds.size.y;
        tamX = (GetComponent<SpriteRenderer>()).bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
        Saltar();
        Falling();
        Fire();
        Dash();
    }

    void Dash()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            myAnimator.SetBool("dash", true);
            myBody.AddForce(new Vector2(transform.localScale[0], 0), ForceMode2D.Impulse);
        }
    }
    void unDash()
    {
        myAnimator.SetBool("dash", false);
    }
    void Fire()
    {
        if (Input.GetKey(KeyCode.X) && Time.time >= canfire)
        {
            if(transform.localScale[0] == 1)
            {
                Instantiate(bala, transform.position - new Vector3(-tamX / 2, 0, 0), transform.rotation);
            }
            else
            {
                Instantiate(bala, transform.position - new Vector3(tamX / 2, 0, 0), transform.rotation);
            }
            
            count = false;
            time = 0.0f;
            myAnimator.SetLayerWeight(1, 1);
            count = true;
            canfire = Time.time + NextFire;
        }
        if (count)
        {
            time += Time.deltaTime;
            if (time >= 5)
            {
                myAnimator.SetLayerWeight(1, 0);
                time = 0.0f;
                count = false;
            }
        }
    }
    void Mover()
    {
        float move = Input.GetAxis("Horizontal");

        if (move != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(move), 1);
            myAnimator.SetBool("isRunning", true);
            transform.Translate(new Vector2(move * speed * Time.deltaTime, 0));
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }
    }
    void Saltar()
    {
        
        if (isGrounded())
        {
            myAnimator.SetBool("falling", false);
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                myAnimator.SetTrigger("takeoff");
                myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                dobleSalto = true;
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.UpArrow) && dobleSalto)
            {
                myAnimator.SetTrigger("takeoff");
                myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                dobleSalto = false;
            }
        }
        
    }

    bool isGrounded()
    {
        RaycastHit2D ray = Physics2D.Raycast(myCollider.bounds.center, Vector2.down,myCollider.bounds.extents.y + 0.3f,LayerMask.GetMask("Ground"));
        Debug.DrawRay(myCollider.bounds.center, new Vector2(0, (myCollider.bounds.extents.y + 0.3f)*-1),Color.red);
        return ray.collider != null;
    }

    void Falling()
    {
        if (myBody.velocity.y < 0 && !myAnimator.GetBool("dash"))
        {
            myAnimator.SetBool("falling", true);
        }
    }

    void afterTakeOffEvent()
    {
        if (!myAnimator.GetBool("dash"))
        {
            myAnimator.SetBool("falling", true);
        }
       
    }
    
}
