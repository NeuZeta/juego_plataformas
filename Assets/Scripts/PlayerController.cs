using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float velocidadRotacion = 50;
    public float velocidadLineal = 15f;
    Rigidbody2D rigidbody;

    public Transform ruedaTrasera;
    private float radioRueda;

   




    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        radioRueda = ruedaTrasera.GetComponent<CircleCollider2D>().radius + 0.01f;
	}
	
    public void MoveRight()
    {
        if (TocaElSuelo())
        {
            rigidbody.velocity += new Vector2(transform.right.x * velocidadLineal, transform.right.y * velocidadLineal) * Time.deltaTime;
        }
    }

    public void MoveLeft()
    {
        if (TocaElSuelo())
        {
            rigidbody.velocity -= new Vector2(transform.right.x * velocidadLineal, transform.right.y * velocidadLineal) * Time.deltaTime;
        }
    }

    public void RotateRight()
    {
        rigidbody.MoveRotation(rigidbody.rotation - velocidadRotacion * Time.deltaTime);
    }

    public void RotateLeft()
    {
        rigidbody.MoveRotation(rigidbody.rotation + velocidadRotacion * Time.deltaTime);
    }

 
    void Update () {

    
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            RotateLeft();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            RotateRight();
        }

    
    }

    bool TocaElSuelo() {
        if (Physics2D.OverlapCircleAll(ruedaTrasera.position, radioRueda).Length > 2)
        {
            Debug.Log("tocando");
            return true;
        } else {
            Debug.Log("flotando");
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != gameObject)
        {
            Debug.Log("Te has dado la vuelta");
        }
    }



}//PlayerController

