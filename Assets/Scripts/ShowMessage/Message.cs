using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI Text;
    [SerializeField] private float OriginSize;

    public void SetScale(float scale)
    {
        Text.fontSize = OriginSize * scale;
    }
    public void ShowMessage(string message, Vector2 position)
    {
        Text.text = message;

        transform.localPosition = position;

        gameObject.SetActive(true);
    }
    public void Disable()
    {
        gameObject.SetActive(false);

        MessagePool.Instance.UnUsing(this);
    }
}
