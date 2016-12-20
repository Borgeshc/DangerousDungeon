using UnityEngine;
using System.Collections;

public class SimpleDemoArcher : MonoBehaviour {

    public Transform T;
    public float speed = 10.0f;

    public int curmat;
    public int curCam;

    bool spin;
    public Camera[] cam;

    public Animator anim;
    public float size = 1;
    public Material[] Mat;
    public Renderer[] mesh;
    public GameObject Arrow;


    void Start()
    {
      //  cam = Camera.main;
        spin = false;
        transform.localScale = new Vector3(size, size, size);
        anim = GetComponentInChildren<Animator>();
        curmat = 0;
        HideArrow();
        ChangeCam();
    }

    public void Spin()
    {
        spin = !spin;
        T.eulerAngles = Vector3.zero;
    }

    public void ChangeMat()
    {

        curmat = curmat + 1;

        if(curmat >= Mat.Length)
        {
            curmat = 0;
        }

        for(int i = 0; i < mesh.Length; i++)
        {
            mesh[i].material = Mat[curmat];
        }
        
    }

    public void ChangeCam()
    {

        curCam = curCam + 1;

        if (curCam >= cam.Length)
        {
            curCam = 0;
        }

        for(int i = 0; i < cam.Length; i++)
        {
            cam[i].gameObject.SetActive(false);
        }

        cam[curCam].gameObject.SetActive(true);
    }

    /// animation
    public void Idle()
    {
        anim.Play("idle");
    }

    public void IdleCombat()
    {
        anim.Play("idleCombat");
    }

    public void Walk()
    {
        anim.Play("walk");
    }
    public void Run()
    {
        anim.Play("run");
    }
    public void Hit()
    {
        anim.Play("hit");
    }
    public void Activate()
    {
        anim.Play("activate");
    }

    public void Atk01()
    {
        anim.Play("attack01");
    }
    public void Atk02()
    {
        anim.Play("attack02");
    }

    public void die()
    {
        anim.Play("die");
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            T.eulerAngles = new Vector3(T.eulerAngles.x, T.eulerAngles.y + Input.GetAxis("Mouse X") * -2, T.eulerAngles.z);
        }

        if (spin == false)
        {
            return;
        }
        T.eulerAngles = new Vector3(T.eulerAngles.x, T.eulerAngles.y + speed * Time.deltaTime, T.eulerAngles.z);
    }

    public void HideArrow()
    {
        Arrow.SetActive(false);
    }

    public void ShowArrow()
    {
        Arrow.SetActive(true);
    }

    public void ShowarrowDelay(float time)
    {
        Invoke("ShowArrow", time);        
    }
}
