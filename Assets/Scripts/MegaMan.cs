using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaMan : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] GameObject bala;
    [SerializeField] float NextFire;
    [SerializeField] GameObject vfx;
    [SerializeField] AudioClip sfx_death;
    [SerializeField] AudioClip sfx_jump;
    [SerializeField] AudioClip sfx_shoot;
    [SerializeField] AudioClip sfx_dash;
    [SerializeField] AudioClip sfx_fall;

    Animator myAnimator;
    Rigidbody2D myBody;
    BoxCollider2D myCollider;
    bool dobleSalto;
    float time = 0.0f;
    bool count;
    bool pause = false;
    float canfire, tamY, tamX;
    int sound = 0;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        tamY = (GetComponent<SpriteRenderer>()).bounds.size.y;
        tamX = (GetComponent<SpriteRenderer>()).bounds.size.x;
        //StartCoroutine(ShowTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            Mover();
            Saltar();
            Falling();
            Fire();
            Dash();
        }
    }


    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnimator.SetBool("dash", true);
            myBody.AddForce(new Vector2(transform.localScale[0] > 0 ? 20 : -20, 0), ForceMode2D.Impulse);

            AudioSource.PlayClipAtPoint(sfx_dash, Camera.main.transform.position);
            

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
            if (transform.localScale[0] == 1)
            {
                Instantiate(bala, transform.position - new Vector3(-tamX / 2, 0, 0), transform.rotation);
                AudioSource.PlayClipAtPoint(sfx_shoot, Camera.main.transform.position);
            }
            else
            {
                Instantiate(bala, transform.position - new Vector3(tamX / 2, 0, 0), transform.rotation);
                AudioSource.PlayClipAtPoint(sfx_shoot, Camera.main.transform.position);
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
                AudioSource.PlayClipAtPoint(sfx_jump, Camera.main.transform.position);
                dobleSalto = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && dobleSalto)
            {
                myAnimator.SetTrigger("takeoff");
                myBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                AudioSource.PlayClipAtPoint(sfx_jump, Camera.main.transform.position);
                dobleSalto = false;
            }
        }

    }

    bool isGrounded()
    {
        RaycastHit2D ray = Physics2D.Raycast(myCollider.bounds.center, Vector2.down, myCollider.bounds.extents.y + 0.3f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(myCollider.bounds.center, new Vector2(0, (myCollider.bounds.extents.y + 0.3f) * -1), Color.red);
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "proyectil_malo")
        {
            pause = true;
            StartCoroutine(Die());
        }
    }
    IEnumerator Die()
    {
        myBody.isKinematic = true;
        myAnimator.SetBool("death", true);
        yield return new WaitForSeconds(1);
        AudioSource.PlayClipAtPoint(sfx_death, Camera.main.transform.position);
        Instantiate(vfx, transform.position, transform.rotation);
        StartCoroutine(GameOver());
    }
    IEnumerator GameOver(){
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        UIManager.Instance.ShowGameOverScreen();
    }

}
