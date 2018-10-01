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

    private Rigidbody2D rigidBody;

    public Transform ruedaTrasera;
    private float radioRueda;

    private bool moveLeft, moveRight, rotateLeft, rotateRight;

    private void Start () {
        
        rigidBody = GetComponent<Rigidbody2D>();
        radioRueda = ruedaTrasera.GetComponent<CircleCollider2D>().radius + 0.05f;
	}

    public void MoveRight()
    {
        if (TocaElSuelo())
        {
            rigidBody.velocity += new Vector2(transform.right.x * velocidadLineal, transform.right.y * velocidadLineal) * Time.deltaTime;
        }
    }

    public void MoveLeft()
    {
        if (TocaElSuelo())
        {
            rigidBody.velocity -= new Vector2(transform.right.x * velocidadLineal, transform.right.y * velocidadLineal) * Time.deltaTime;
        }
    }

    public void RotateRight()
    {
        rigidBody.MoveRotation(rigidBody.rotation - velocidadRotacion * Time.deltaTime);
    }

    public void RotateLeft()
    {
        rigidBody.MoveRotation(rigidBody.rotation + velocidadRotacion * Time.deltaTime);
    }

    private void Update () {
 
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

    private bool TocaElSuelo() {
        if (Physics2D.OverlapCircleAll(ruedaTrasera.position, radioRueda).Length > 3)
        {
            Debug.Log("Toca el suelo");
            return true;
        } else {
            Debug.Log("Está flotando");
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.enabled)
        {
            if (collision.gameObject.tag == "Finish")
            {
                if (endLevel != null) endLevel();

            }
            else
            {
                if (eliminado != null)
                {
                    eliminado();
                    AudioController.instance.audioSource.PlayOneShot(AudioController.instance.carCrash);
                    anim.SetTrigger("Died");
                }
            }
        }

    }


    public void MoveLeftDown()
    {
        AudioController.instance.audioSource.PlayOneShot(AudioController.instance.click);
        moveLeft = true;
        if (this.enabled)
        {
            anim.SetTrigger("Moving");
        }
    }


    public void MoveRightDown()
    {
        AudioController.instance.audioSource.PlayOneShot(AudioController.instance.click);
        moveRight = true;
        if (this.enabled)
        {
            anim.SetTrigger("Moving");
        }
    }

    public void RotateLeftDown()
    {
        AudioController.instance.audioSource.PlayOneShot(AudioController.instance.click);
        rotateLeft = true;
        if (this.enabled)
        {
            anim.SetTrigger("Moving");
        }
    }

    public void RotateRightDown()
    {
        AudioController.instance.audioSource.PlayOneShot(AudioController.instance.click);
        rotateRight = true;
        if (this.enabled)
        {
            anim.SetTrigger("Moving");
        }
    }

    public void StopMoving()
    {
        moveLeft = moveRight = rotateLeft = rotateRight = false;
        if (this.enabled)
        {
            anim.SetTrigger("Moving");
        }
    }

}//PlayerController

