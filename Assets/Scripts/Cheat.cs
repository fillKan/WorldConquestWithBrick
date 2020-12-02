using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public void AAA()
    {
        XPManager.Instance.AddXP(10000);
    }

    private void Update()
    {
#if !UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
#endif
    }
}
