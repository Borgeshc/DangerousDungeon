using UnityEngine;
using System.Collections;

public class GolemBoss : MonoBehaviour
{
    public float speed;
    public float abilityCooldown;
    Animator anim;
    GameObject player;
    bool inRange;
    int chooseAbility;
    float lastAbility;
    bool abilityActive;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if(transform.position.x - player.transform.position.x > 3.5f)
        {
            inRange = false;
            transform.LookAt(player.transform);
            anim.SetBool("IsIdle", false);
            anim.SetInteger("Attack", 0);
            anim.SetBool("IsWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            inRange = true;
            transform.LookAt(transform);
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsIdle", true);

            if (Time.time + abilityCooldown > lastAbility)
            {
                lastAbility = Time.time;
                chooseAbility = Random.Range(1, 5);
                anim.SetInteger("Attack", chooseAbility);
            }
        }
    }
}
