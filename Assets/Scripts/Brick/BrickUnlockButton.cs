using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickUnlockButton : MonoBehaviour
{
    private readonly Color DisableColor = new Color(0.5f, 0.5f, 0.5f);

    [SerializeField] private int Cost;
    [SerializeField] private Brick TargetBrick;

    [SerializeField] private Image[] ButtonImages;

    private bool _IsAlreadyInit = false;

    private void OnEnable()
    {
        if (!_IsAlreadyInit)
        {
            XPManager.Instance.XPChangeEvent += o =>
            {
                if (o >= Cost)
                {
                    ColorChange(Color.white);
                }
                else
                {
                    ColorChange(DisableColor);
                }
            };
            if (XPManager.Instance.CanSubtractXP(Cost))
            {
                ColorChange(Color.white);
            }
            else
            {
                ColorChange(DisableColor);
            }
            _IsAlreadyInit = true;
        }
    }

    public void Unlock()
    {
        if (XPManager.Instance.CanSubtractXP(Cost))
        {
            XPManager.Instance.SubtractXP(Cost);

            BrickPool.Instance.AddSpecial(TargetBrick);
        }
    }

    private void ColorChange(Color changeColor)
    {
        for (int i = 0; i < ButtonImages.Length; i++)
        {
            ButtonImages[i].color = changeColor;
        }
    }
}
