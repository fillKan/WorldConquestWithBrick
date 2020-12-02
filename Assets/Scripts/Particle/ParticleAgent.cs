using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAgent : MonoBehaviour
{
    [SerializeField] private float LifeTime;

    [SerializeField] private ParticleSystem Particle;

    private void Reset()
    {
        TryGetComponent(out Particle);
    }

    public void PlayStart(Vector2 position)
    {
        gameObject.SetActive(true);
        transform.position = position;

        StartCoroutine(PlayParticle());
    }
    private void PlayOver()
    {
        gameObject.SetActive(false);

        ParticlePool.Instance.UnUsingParticle(this);
    }
    private IEnumerator PlayParticle()
    {
        yield return new WaitForSeconds(LifeTime);

        PlayOver();
    }
}
