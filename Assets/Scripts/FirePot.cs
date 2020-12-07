using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirePot : MonoBehaviour
{
    [SerializeField] private Brick _Brick;

    [SerializeField] private Vector2 FirePoint;
    [SerializeField] private float FirePotteryTime;

    [SerializeField] private Image GaugeImage;

    private Brick _NextBrick;

    public void SetNextBrick(Brick brick)
    {
        if (_NextBrick == null)
        {
            _NextBrick = brick;
        }
    }

    private void Start()
    {
        StartCoroutine(FirePotteryRoutine());
    }
    private IEnumerator FirePotteryRoutine()
    {
        while (gameObject.activeSelf)
        {
            float DeltaTime()
            {
                return Time.deltaTime * Time.timeScale;
            }
            float WaitBakeTime()
            {
                return FirePotteryTime - (FirePotteryTime * BrickAbility.Instance.FirePotAccelation);
            }
            for (float i = 0f; i < WaitBakeTime(); i += DeltaTime())
            {
                GaugeImage.fillAmount = i / WaitBakeTime();

                yield return null;
            }
            Brick brick;

            if (_NextBrick != null)
            {
                brick = _NextBrick;
                        _NextBrick = null;
            }
            else if (BrickAbility.Instance.Special >= Random.value)
            {
                brick = BrickPool.Instance.GetSpecial();
            }
            else
            {
                brick = BrickPool.Instance.Get();
            }
            if (brick.IsSpecial)
            {
                SoundManager.Instance.PlaySound(Sounds.SummonSpecial);
            }
            else
            {
                SoundManager.Instance.PlaySound(Sounds.SummonBrick);
            }
            brick.gameObject.SetActive(true);
            brick.transform.position = transform.TransformPoint(FirePoint);
            brick.transform.rotation = Quaternion.identity;

            brick.GetRigidbody.AddForce(Vector3.one * 4f, ForceMode2D.Impulse);
        }
    }
}
