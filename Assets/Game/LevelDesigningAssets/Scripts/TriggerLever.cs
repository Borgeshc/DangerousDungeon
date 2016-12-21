using UnityEngine;
using System.Collections;

public class TriggerLever : MonoBehaviour
{
    public Animator openDoorAnimator;
    public GameObject interactionMessage;
    Animator anim;
    bool wasPressed;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!wasPressed)
            interactionMessage.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire1"))
            {
                wasPressed = true;
                interactionMessage.SetActive(false);
                anim.SetBool("IsPulled", true);
                openDoorAnimator.SetBool("WasOpened", true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            interactionMessage.SetActive(false);
        }
    }
}
