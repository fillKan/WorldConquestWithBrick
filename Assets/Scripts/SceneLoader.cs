using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private int LoadSceneIndex;

    private void Awake()
    {
        Screen.SetResolution(1280, 720, false);
    }

    public void Update()
    {
        if (Input.anyKeyDown) {
            SceneManager.LoadScene(LoadSceneIndex);
        }
    }
}
