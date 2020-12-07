using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public Rigidbody2D GetRigidbody
    { get => Rigidbody; }
    public int Damage
    { get => _Damage; }

    private Vector2 MaxVelocity;

    public readonly Color Critcal = new Color(0.4f, 0.6f, 1f);

    public bool IsSpecial => _IsSpecial;

    [Header("Particle Info")]
    [SerializeField] private GameObject Particle;

    [Header("Brick Info")]
    [SerializeField] private Rigidbody2D Rigidbody;
    [SerializeField] private int _Damage;
    [SerializeField] private bool _IsSpecial;
    [SerializeField] private Sounds BreakSound;

    private System.Action mDisableAction;

    private void Reset()
    {
        TryGetComponent(out Rigidbody);
    }

    private void Awake()
    {
        mDisableAction += () =>
        {
            if (_IsSpecial)
            {
                gameObject.SetActive(false);

                Instantiate(Particle, transform.position, Quaternion.identity);
                MainCamera.Instance.Shake(0.5f, 0.6f, true);
            }
            else
            {
                BrickPool.Instance.Add(this);

                ParticlePool.Instance.UsingParticle(transform.position);
                MainCamera.Instance.Shake(0.25f, 0.4f, true);
            }
            SoundManager.Instance.PlaySound(BreakSound);
        };
    }

    private void OnEnable()
    {
        MaxVelocity = Vector2.zero;
    }

    private void Update()
    {
        if (Rigidbody.velocity.magnitude > MaxVelocity.magnitude)
        {
            MaxVelocity = Rigidbody.velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            int damage = Mathf.FloorToInt(BrickAbility.Instance.AttackPower * MaxVelocity.magnitude);

            int exp = Mathf.FloorToInt(damage * BrickAbility.Instance.ExpScaling);

            if (_IsSpecial)
            {
                exp *= 3;
            }
            XPManager.Instance.AddXP(exp);

            if (_IsSpecial)
            {
                damage *= 3;

                gameObject.SetActive(false);

                Instantiate(Particle, transform.position, Quaternion.identity);
                MainCamera.Instance.Shake(0.5f, 0.6f, true);

                MessagePool.Instance.Using($"+{exp}XP", transform.position, 1.75f);
            }
            else
            {
                BrickPool.Instance.Add(this);

                ParticlePool.Instance.UsingParticle(transform.position);
                MainCamera.Instance.Shake(0.25f, 0.4f, true);

                MessagePool.Instance.Using($"+{exp}XP", transform.position, MaxVelocity.magnitude * 0.07f);
            }
            SoundManager.Instance.PlaySound(BreakSound);

            enemy.Damaged(damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OutArea"))
        {
            mDisableAction?.Invoke();
        }
    }
}
