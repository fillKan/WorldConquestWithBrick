using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField] private float ScrollRangeMaxX;
    [SerializeField] private float ScrollRangeMinX;

    private Vector3 mFirstClickPoint;

    private void Update()
    {
        Vector3 MousePoint()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        float Range(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;

            return value;
        }
        if (Input.GetMouseButtonDown(2))
        {
            mFirstClickPoint = MousePoint();
        }
        if (Input.GetMouseButton(2))
        {
            float x = (mFirstClickPoint - MousePoint()).x * 0.2f;

            Vector3 trans = Vector3.right * x + Camera.main.transform.position;
                    trans.x = Range(trans.x, ScrollRangeMinX, ScrollRangeMaxX);

            Camera.main.transform.position = trans;
        }
    }
}
