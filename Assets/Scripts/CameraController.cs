using UnityEngine;

public class CameraController : MonoBehaviour {
    private PlayerController player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        Debug.Log(player.transform.position);
    }

    private void Update () {
        transform.position = player.transform.position - new Vector3(-2,0,10);
	}
}
