using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    public string nextLevel;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
