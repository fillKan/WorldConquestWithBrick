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

    [Header("Particle Info")]
    [SerializeField] private GameObject Particle;

    [Header("Brick Info")]
    [SerializeField] private Rigidbody2D Rigidbody;
    [SerializeField] private int _Damage;
    [SerializeField] private bool IsSpecial;

    private System.Action mDisableAction;

    private void Reset()
    {
        TryGetComponent(out Rigidbody);
    }

    private void Awake()
    {
        mDisableAction += () =>
        {
            if (IsSpecial)
            {
                gameObject.SetActive(false);

                Instantiate(Particle, transform.position, Quaternion.identity);

                SoundManager.Instance.PlaySound(Sounds.SpecialBreak);

                MainCamera.Instance.Shake(0.5f, 0.6f, true);
            }
            else
            {
                BrickPool.Instance.Add(this);

                ParticlePool.Instance.UsingParticle(transform.position);

                SoundManager.Instance.PlaySound(Sounds.BrickBreak);

                MainCamera.Instance.Shake(0.25f, 0.4f, true);
            }
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

            if (IsSpecial)
            {
                exp *= 3;
            }
            XPManager.Instance.AddXP(exp);

            if (IsSpecial)
            {
                damage *= 3;

                gameObject.SetActive(false);

                Instantiate(Particle, transform.position, Quaternion.identity);

                SoundManager.Instance.PlaySound(Sounds.SpecialBreak);

                MainCamera.Instance.Shake(0.5f, 0.6f, true);

                MessagePool.Instance.Using($"+{exp}XP", transform.position, 1.75f);
            }
            else
            {
                BrickPool.Instance.Add(this);

                ParticlePool.Instance.UsingParticle(transform.position);

                SoundManager.Instance.PlaySound(Sounds.BrickBreak);

                MainCamera.Instance.Shake(0.25f, 0.4f, true);

                MessagePool.Instance.Using($"+{exp}XP", transform.position, MaxVelocity.magnitude * 0.07f);
            }
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
