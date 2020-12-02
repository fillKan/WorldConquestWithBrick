using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float Force;

    private GameObject mBrick;

    private void Update()
    {
        Vector3 MousePoint()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonDown(0))
        {
            var hitInfo = Physics2D.Raycast(MousePoint(), Vector2.zero, 15f);

            if (hitInfo != default) {
                if (hitInfo.collider.CompareTag("Brick"))
                {
                    mBrick = hitInfo.collider.gameObject;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (mBrick != null) {
                if (mBrick.TryGetComponent(out Rigidbody2D rigidbody))
                {
                    SoundManager.Instance.PlaySound(Sounds.Shoot);

                    if (Mathf.Abs(rigidbody.velocity.y) < 0.5f)
                    {
                        Vector2 direction = (mBrick.transform.position - MousePoint()).normalized;

                        rigidbody.AddForce(direction * Force);
                    }
                }
                mBrick = null;
            }
        }
    }
}
