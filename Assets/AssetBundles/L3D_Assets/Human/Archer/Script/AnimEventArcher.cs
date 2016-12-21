using UnityEngine;
using System.Collections;

public class AnimEventArcher : MonoBehaviour {

    public GameObject Arrow;

    SimpleDemoArcher Demo;

    void Start()
    {
        Demo = GetComponentInParent<SimpleDemoArcher>();
        HideArrow();
    }

    public void HideArrow()
    {
        Arrow.SetActive(false);
    }

    public void ShowArrow()
    {
        Arrow.SetActive(true);
    }

}
