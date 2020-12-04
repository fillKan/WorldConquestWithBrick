using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPManager : Singleton<XPManager>
{
    [SerializeField] private TMPro.TextMeshProUGUI ExpText;

    public event Action<int> XPChangeEvent;

    private int mSumXP;

    private void Awake()
    {
        mSumXP = 0;

        ExpText.text = $"0XP";
    }
    public void AddXP(int exp)
    {
        mSumXP += exp;

        ExpText.text = $"{mSumXP}XP";

        XPChangeEvent?.Invoke(mSumXP);
    }
    public bool SubtractXP(int exp)
    {
        if (mSumXP - exp >= 0)
        {
            mSumXP -= exp;

            ExpText.text = $"{mSumXP}XP";

            XPChangeEvent?.Invoke(mSumXP);

            return true;
        }
        return false;
    }
    public bool CanSubtractXP(int exp)
    {
        return mSumXP - exp >= 0;
    }
}
