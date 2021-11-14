using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] int lives;
    [SerializeField] GameObject bala;
    [SerializeField] AudioClip sfx_death;
    Animator myAnimator;
    float tamY, tamX;
    private float escala;
    int canfire = 1;
    // Start is called before the first frame update
    void Start()
    {
        tamY = (GetComponent<SpriteRenderer>()).bounds.size.y;
        tamX = (GetComponent<SpriteRenderer>()).bounds.size.x;
        myAnimator = GetComponent<Animator>();
        escala = transform.localScale[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("player")) != null)
        {
            if (canfire == 2)
            {
                myAnimator.SetBool("fire", true);
            }
            canfire++;
        }
    }
    public void fire()
    {
        GameObject b = Instantiate(bala, transform.position - new Vector3(-0.8f, 0f, 0), transform.rotation) as GameObject;
        b.SendMessage("TheStart", true);

        GameObject b1 = Instantiate(bala, transform.position - new Vector3(0.8f, 0f, 0), transform.rotation) as GameObject;
        b1.SendMessage("TheStart", false);
    }
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(0, 0, 1, 0.5f);
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
            Debug.Log("colision");
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
