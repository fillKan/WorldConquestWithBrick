using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickPool : Singleton<BrickPool>
{
    private Stack<Brick> mBrickPool;

    [SerializeField] private Brick OriginBrick;
    [SerializeField] private List<Brick> SpecialBrick;

    [SerializeField] private int HoldingCount;

    public int BrickCount
    {
        get
        {
            return (FindObjectsOfType(typeof(Brick)) as Brick[]).ToList().Count(o => o.gameObject.activeSelf);
        }
    }

    private void Awake()
    {
        mBrickPool = new Stack<Brick>();

        AddPool(HoldingCount);
    }
    public Brick GetSpecial()
    {
        return Instantiate(SpecialBrick[Random.Range(0, SpecialBrick.Count)]);
    }
    public void AddSpecial(Brick brick)
    {
        SpecialBrick.Add(brick);
    }
    public Brick Get()
    {
        if (mBrickPool.Count == 0)
        {
            AddPool();
        }
        mBrickPool.Peek().gameObject.SetActive(true);

        return mBrickPool.Pop();
    }
    public void Add(Brick brick)
    {
        brick.gameObject.SetActive(false);
        brick.GetRigidbody.velocity = Vector2.zero;

        mBrickPool.Push(brick);
    }
    private void AddPool(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            mBrickPool.Push(Instantiate(OriginBrick));

            mBrickPool.Peek().gameObject.SetActive(false);
        }
    }
}
