using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    public void ActiveSwap(GameObject _object)
    {
        _object.SetActive(!_object.activeSelf);
    }
}
