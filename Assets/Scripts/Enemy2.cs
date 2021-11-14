using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] int lives;
    [SerializeField] bool right;
    [SerializeField] GameObject bala;
    [SerializeField] AudioClip sfx_death;

    Animator myAnimator;
    int canfire = 1;
    float tamY, tamX;
    private float escala;
    // Start is called before the first frame update
    void Start()
    {
        tamY = (GetComponent<SpriteRenderer>()).bounds.size.y;
        tamX = (GetComponent<SpriteRenderer>()).bounds.size.x;
        myAnimator = GetComponent<Animator>();
        escala = transform.localScale[0];
        if (right)
        {
            transform.localScale = new Vector2(-escala, escala);
        }
        else
        {
            transform.localScale = new Vector2(escala, escala);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("player")) != null)
        {
            if (canfire == 2)
            {
                StartCoroutine(fire());
            }
            canfire++;
        }
    }

    IEnumerator fire()
    {
        while (true)
        {
            if (right)
            {
                GameObject balas = Instantiate(bala, transform.position - new Vector3(-tamX / 2, 0, 0), transform.rotation) as GameObject;
                balas.SendMessage("TheStart", right);
                yield return new WaitForSeconds(2f);
            }
            else
            {
                GameObject balas = Instantiate(bala, transform.position - new Vector3(tamX / 2, 0, 0), transform.rotation) as GameObject;
                balas.SendMessage("TheStart", right);
                yield return new WaitForSeconds(2f);

            }
        }

    }
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, range);
    }
    public void destroy()
    {
        Destroy(gameObject);
        GameManager.Instance.EnemyDied();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "proyectil")
        {
            if (lives != 0)
            {
                lives--;
            }
            else
            {
                myAnimator.SetBool("destroy", true);
                AudioSource.PlayClipAtPoint(sfx_death, Camera.main.transform.position);
            }
        }

    }
}
