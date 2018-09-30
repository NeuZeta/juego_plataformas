using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Animator anim;

    public float velocidadRotacion = 50;
    public float velocidadLineal = 15f;

    public delegate void triggerDelegate();
    public event triggerDelegate eliminado, endLevel;
    

    Rigidbody2D rigidbody;

    public Transform ruedaTrasera;
    private float radioRueda;

    private bool moveLeft, moveRight, rotateLeft, rotateRight;

 
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

    
        if (moveLeft)
        {
            MoveLeft();
        }
        if (moveRight)
        {
            MoveRight();
        }
        if (rotateLeft)
        {
            RotateLeft();
        }
        if (rotateRight)
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
        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("finish");
            if (endLevel != null) endLevel();

        } else
        {
            Debug.Log("Has perdido");
            if (eliminado != null) eliminado();
            anim.SetTrigger("Died");
        }

    }


    public void MoveLeftDown()
    {
        moveLeft = true;
        anim.SetTrigger("Moving");
    }

    public void MoveRightDown()
    {
        moveRight = true;
        anim.SetTrigger("Moving");
    }

    public void RotateLeftDown()
    {
        rotateLeft = true;
        anim.SetTrigger("Moving");
    }

    public void RotateRightDown()
    {
        rotateRight = true;
        anim.SetTrigger("Moving");
    }

    public void StopMoving()
    {
        moveLeft = moveRight = rotateLeft = rotateRight = false;
        anim.SetTrigger("Moving");
    }

}//PlayerController

