using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickUnlockButton : MonoBehaviour
{
    private readonly Color DisableColor = new Color(0.5f, 0.5f, 0.5f);

    [Header("Unlock Indo")]
    [SerializeField] private int Cost;
    [SerializeField] private Brick TargetBrick;
    
    [Header("Text")]
    [SerializeField] private TMPro.TextMeshProUGUI LockText;
    [SerializeField] private TMPro.TextMeshProUGUI NameText;

    [Header("Images")]
    [SerializeField] private Image[] ButtonImages;

    private bool _IsAlreadyInit = false;
    private bool _IsUnlock = false;

    private void OnEnable()
    {
        if (!_IsAlreadyInit)
        {
            XPManager.Instance.XPChangeEvent += UnlockCheck;

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
        if (!_IsUnlock)
        {
            if (XPManager.Instance.CanSubtractXP(Cost))
            {
                TargetBrick = Instantiate(TargetBrick);
                TargetBrick.gameObject.SetActive(false);

                LockText.text = "해금됨";

                LockText.color = Color.yellow;
                NameText.color = Color.yellow;

                XPManager.Instance.SubtractXP(Cost);
                XPManager.Instance.XPChangeEvent -= UnlockCheck;

                BrickPool.Instance.AddSpecial(TargetBrick);

                _IsUnlock = true;

                SoundManager.Instance.PlaySound(Sounds.BrickUnlock);

                var firePot = FindObjectOfType(typeof(FirePot)) as FirePot;
                    firePot?.SetNextBrick(TargetBrick);
            }
        }
    }

    private void UnlockCheck(int sumCost)
    {
        if (sumCost >= Cost)
        {
            ColorChange(Color.white);
        }
        else
        {
            ColorChange(DisableColor);
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
