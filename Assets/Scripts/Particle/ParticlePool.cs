using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : Singleton<ParticlePool>
{
    [SerializeField] private int HoldingCount;
    [SerializeField] private ParticleAgent Particle;
    [SerializeField] private GameObject SpecialParticle;

    private Stack<ParticleAgent> mPool;

    private void Awake()
    {
        mPool = new Stack<ParticleAgent>();

        CreateParticle(HoldingCount);
    }
    public void UsingSpecial(Vector2 position)
    {
        Instantiate(Particle, position, Quaternion.identity);

        Debug.Log("BBB");
    }
    public void UsingParticle(Vector2 position)
    {
        if (mPool.Count == 0)
        {
            CreateParticle();
        }
        mPool.Peek().PlayStart(position);
        mPool.Pop();
    }
    public void UnUsingParticle(ParticleAgent particle)
    {
        mPool.Push(particle);
    }
    private void CreateParticle(int count = 1)
    {
        for (int i = 0; i < count; ++i)
        {
            mPool.Push(Instantiate(Particle));

            mPool.Peek().gameObject.SetActive(false);
        }
    }
}
