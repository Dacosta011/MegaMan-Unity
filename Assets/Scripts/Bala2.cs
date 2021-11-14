using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala2 : MonoBehaviour
{
    // Start is called before the first frame update
     Rigidbody2D myBody;
     bool derecha;

     float escala;
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        escala = transform.localScale.x;
        if(derecha){
            transform.localScale = transform.localScale * -1;
            transform.localScale = new Vector2(-escala, transform.localScale.y);
        }else{
            transform.localScale = transform.localScale * 1;
            transform.localScale = new Vector2(escala, transform.localScale.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(derecha){
            transform.position += transform.right * Time.deltaTime * 20;
        }else{
            transform.position += -transform.right * Time.deltaTime * 20;
        }
        
        //myBody.AddForce(new Vector2(-2.5f, 0), ForceMode2D.Impulse);
    }
    public void TheStart(bool derecha)
    {
        this.derecha = derecha;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground" || collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
