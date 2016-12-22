using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    public Transform Target;
    private Vector3 offset;

    public bool Smooth;
    public float smoothValue = 4;

    void Start()
    {
        offset = transform.position;
    }

    void LateUpdate()
    {
        if (Smooth == false)
        {
            transform.position = Target.position + offset;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, Target.position + offset, smoothValue * Time.deltaTime);
        }
    }
        
}
