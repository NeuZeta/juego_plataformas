using UnityEngine;

public class CameraController : MonoBehaviour {

    PlayerController player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        Debug.Log(player.transform.position);
    }

    void Update () {
        transform.position = player.transform.position - new Vector3(-2,0,10);
	}
}
