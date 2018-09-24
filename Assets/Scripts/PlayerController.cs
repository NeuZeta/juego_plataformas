using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float velocidadRotacion = 50;
    public float velocidadLineal = 1f;
    Rigidbody2D rigidbody;



	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();	
	}
	
    public void MoveRight()
    {
        //rigidbody.velocity += new Vector2(transform.right.x * velocidadLineal, transform.right.y * velocidadLineal) * Time.deltaTime;      
        rigidbody.velocity += new Vector2(transform.right.x * velocidadLineal, 0f);

    }

    public void MoveLeft()
    {
        rigidbody.velocity -= new Vector2(transform.right.x * velocidadLineal, 0f);
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
}
