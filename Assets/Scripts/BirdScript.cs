using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour {

    public PlayerController player;
    public float distanceFromPlayerY;
    public float forwardSpeed = 2f;
    public float newXposition;
    public GameObject objectToSpawn;
    public float secondsToWait;


	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        distanceFromPlayerY = transform.position.y - player.transform.position.y; 
    }

    private void OnEnable()
    {
        StartCoroutine("WaitToSpawnNewObject");
    }

    // Update is called once per frame
    void Update () {

        newXposition = transform.position.x + forwardSpeed * Time.deltaTime;
        transform.position = new Vector3(newXposition, player.transform.position.y + distanceFromPlayerY, 0f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BirdBoundary")
        {
            forwardSpeed *= -1;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    void SpawnDropLooping()
    {
        if (enabled)
        {
            Instantiate(objectToSpawn, transform.position, transform.rotation);
            StartCoroutine("WaitToSpawnNewObject");
        }
    }

    IEnumerator WaitToSpawnNewObject()
    {
        yield return new WaitForSeconds(secondsToWait);
        SpawnDropLooping();
    }

}

