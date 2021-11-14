using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy1 : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] GameObject player;
    [SerializeField] int lives;
    [SerializeField] AudioClip sfx_death;
    private AIPath aiPath;
    private float escala;
    private float initial;
    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        aiPath = GetComponent<AIPath>();
        aiPath.canMove = false;
        escala = transform.localScale[0];
        initial = transform.position[0];
    }

    // Update is called once per frame
    void Update()
    {
        //if(Vector2.Distance(player.transform.position,transform.position) <=range){
        //  Debug.Log("persigalo");
        //}
        if (Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("player")) != null)
        {
            aiPath.canMove = true;
        }
        PositionEnemy();
    }

    void PositionEnemy()
    {
        if (initial > transform.position[0])
        {
            transform.localScale = new Vector2(escala, escala);
            initial = transform.position[0];
        }
        else if (initial == transform.position[0])
        {
            transform.localScale = new Vector2(escala, escala);
            initial = transform.position[0];
        }
        else
        {
            transform.localScale = new Vector2(-escala, escala);
            initial = transform.position[0];
        }

    }
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(1, 1, 0, 0.5f);
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
