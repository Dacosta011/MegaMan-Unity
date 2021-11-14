using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala3 : MonoBehaviour
{
    Rigidbody2D myBody;
    bool derecha;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        
    }

    public void TheStart(bool derecha)
    {
        this.derecha = derecha;
    }

    // Update is called once per frame
    void Update()
    {
        if (derecha)
        {
            //myBody.AddForce(new Vector2(0.0000000001f,0.0000000001f), ForceMode2D.Impulse);
            transform.Translate(new Vector2(0.5f * 10 * Time.deltaTime, 0.5f * 20 * Time.deltaTime));
        }
        else
        {
            //myBody.AddForce(new Vector2(-0.01f,0.01f), ForceMode2D.Impulse);
            transform.Translate(new Vector2(-0.5f * 10 * Time.deltaTime, 0.5f * 20 * Time.deltaTime));
        }
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
