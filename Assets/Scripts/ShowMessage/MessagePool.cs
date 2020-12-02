using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePool : Singleton<MessagePool>
{
    [SerializeField] private Canvas ParentCanvas;

    [SerializeField] private Message Message;
    [SerializeField] private int HoldingCount;

    private Stack<Message> mPool;

    private void Awake()
    {
        mPool = new Stack<Message>();

        CreateParticle(HoldingCount);
    }
    public void Using(string message, Vector2 position)
    {
        if (mPool.Count == 0)
        {
            CreateParticle();
        }
        mPool.Peek().ShowMessage(message, position);
        mPool.Pop();
    }
    public void Using(string message, Vector2 position, float scale)
    {
        if (mPool.Count == 0)
        {
            CreateParticle();
        }
        mPool.Peek().SetScale(scale);
        mPool.Peek().ShowMessage(message, position);
        mPool.Pop();
    }
    public void UnUsing(Message message)
    {
        message.SetScale(1f);

        mPool.Push(message);
    }
    private void CreateParticle(int count = 1)
    {
        for (int i = 0; i < count; ++i)
        {
            mPool.Push(Instantiate(Message, ParentCanvas.transform));

            mPool.Peek().gameObject.SetActive(false);
        }
    }
}
