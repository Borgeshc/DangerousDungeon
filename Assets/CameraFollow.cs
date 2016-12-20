using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject player;
    public float cameraSpeed;
    public Vector3 cameraOffset;
    public bool snapMovement;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.x <= 0)
        {
            cameraOffset.x = 5;
        }
        if(transform.position.x >= 35)
        {
            cameraOffset.x = -5;
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + cameraOffset.x, player.transform.position.y + cameraOffset.y, player.transform.position.z + cameraOffset.z), Time.deltaTime * cameraSpeed);

    }
}
