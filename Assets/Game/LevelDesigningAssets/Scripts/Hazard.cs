using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour
{
    public Transform restartPoint;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.position = restartPoint.transform.position;
        }
    }
}
