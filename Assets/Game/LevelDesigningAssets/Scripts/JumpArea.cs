using UnityEngine;
using System.Collections;

public class JumpArea : MonoBehaviour
{
    Movement playerMovement;
    float originalJumpPower;

    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<Movement>();
        originalJumpPower = playerMovement.jumpHeight;
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        playerMovement.jumpHeight = 4;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            playerMovement.jumpHeight = originalJumpPower;
    }
}
