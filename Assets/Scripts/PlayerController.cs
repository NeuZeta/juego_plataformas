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

    public float disabledTime = 2f;

    private void Start () {
        
        rigidBody = GetComponent<Rigidbody2D>();
        radioRueda = ruedaTrasera.GetComponent<CircleCollider2D>().radius;
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
        if (Physics2D.OverlapCircleAll(ruedaTrasera.position, radioRueda * 1.1f).Length > 2)
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
            else if (collision.gameObject.tag == "Drop")
            {
                if (this.enabled) { 
                StartCoroutine("DisablePlayerForSomeTime");
                StopMoving();
                AudioController.instance.audioSource.PlayOneShot(AudioController.instance.uhoh);
                }
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

    public void MoveLeftUp()
    {
        AudioController.instance.audioSource.PlayOneShot(AudioController.instance.click);
        moveLeft = false;
        if (this.enabled)
        {
            anim.SetTrigger("Moving");
        }
    }


    public void MoveRightUp()
    {
        AudioController.instance.audioSource.PlayOneShot(AudioController.instance.click);
        moveRight = false;
        if (this.enabled)
        {
            anim.SetTrigger("Moving");
        }
    }

    public void RotateLeftUp()
    {
        AudioController.instance.audioSource.PlayOneShot(AudioController.instance.click);
        rotateLeft = false;
        if (this.enabled)
        {
            anim.SetTrigger("Moving");
        }
    }

    public void RotateRightUp()
    {
        AudioController.instance.audioSource.PlayOneShot(AudioController.instance.click);
        rotateRight = false;
        if (this.enabled)
        {
            anim.SetTrigger("Moving");
        }
    }

    IEnumerator DisablePlayerForSomeTime()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(disabledTime);
        rigidBody.constraints = RigidbodyConstraints2D.None;
    }

    void StopMoving()
    {
        moveLeft = moveRight = rotateLeft = rotateRight = false;
    }


}//PlayerController

