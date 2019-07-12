using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    //public GameObject Camera;

    public GameObject sceneSwitcher;
    private SceneController sceneController;

    void Awake()
    {
        sceneController = sceneSwitcher.GetComponent<SceneController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EndLevel"))
        {
            Invoke("ReloadScene", 2f);
        }
    }

    void ReloadScene()
    {
        sceneController.ReloadScene();
    }
}
  