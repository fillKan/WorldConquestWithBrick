using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private IEnumerator mDamagedMotion;
    private IEnumerator mDeathMotion;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer Renderer;

    [SerializeField] private Sprite DefaultSprite;
    [SerializeField] private Sprite DamagedSprite;
    [SerializeField] private Sprite DeathSprite;

    [SerializeField] private Image GaugeImage;
    public float MaxHealth { get; private set; }
    public float CurHealth { get; private set; }

    public void Init(float maxHealth)
    {
        CurHealth = MaxHealth = (maxHealth - maxHealth % 5);

        gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        Renderer.color = Color.white;

        CurHealth = MaxHealth;
    }
    private void OnDisable()
    {
        Renderer.sprite = DefaultSprite;

        mDamagedMotion = null;
    }

    public void Damaged(float damage)
    {
        if (mDeathMotion == null)
        {
            CurHealth = Mathf.Max(CurHealth - damage, 0f);

            if (CurHealth <= 0f)
            {
                if (mDamagedMotion != null)
                {
                    StopCoroutine(mDamagedMotion); mDamagedMotion = null;
                }
                int exp = Mathf.FloorToInt(MaxHealth);

                MessagePool.Instance.Using($"+{exp}XP", new Vector2(1.8f, 0f), 2f);
                XPManager.Instance.AddXP(exp);

                StartCoroutine(mDeathMotion = DeathMotion(1.5f));
            }
            else if (mDamagedMotion == null)
            {
                StartCoroutine(mDamagedMotion = DamagedMotion(0.15f));
            }
        }
    }

    private IEnumerator DamagedMotion(float playTime)
    {
        Vector2 initPos = transform.position;

        Renderer.sprite = DamagedSprite;

        float DeltaTime()
        {
            return Time.deltaTime * Time.timeScale;
        }
        for (float i = 0; i < playTime; i += DeltaTime())
        {
            transform.position = initPos + Random.insideUnitCircle * 0.1f;

            yield return null;
        }
        transform.position = initPos;

        Renderer.sprite = DefaultSprite;

        mDamagedMotion = null;
    }
    private IEnumerator DeathMotion(float playTime)
    {
        Renderer.sprite = DeathSprite;

        float DeltaTime()
        {
            return Time.deltaTime * Time.timeScale;
        }
        for (float i = 0f; i < playTime; i += DeltaTime())
        {
            Renderer.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), i / playTime);

            yield return null;
        }
        gameObject.SetActive(false);

        mDeathMotion = null;
    }
    private void LateUpdate()
    {
        GaugeImage.fillAmount = CurHealth / MaxHealth;
    }
}
