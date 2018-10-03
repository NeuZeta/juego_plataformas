using UnityEngine;

public class CameraController : MonoBehaviour {

    private PlayerController player;
    Vector3 offset;
    float cameraSpeed = 10f;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        offset = player.transform.position - transform.position;    
    }

    private void Update () {
        //transform.position = player.transform.position - offset;
        transform.position = Vector3.Lerp(transform.position, player.transform.position - offset, cameraSpeed * Time.deltaTime);
	}
}
