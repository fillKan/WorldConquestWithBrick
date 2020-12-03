using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Upgrade
{
    BrickPower, FirePotSpeed, SpecialBrick
}

public class UpgradeButton : MonoBehaviour
{
    public const int MAX_LEVEL = 20;
    public const float FIREPOT_ACCELATION_PERCENT = 0.01f;
    public const float SPECIAL_APPEARANCE_PERCENT = 0.03f;

    [SerializeField] private Image ButtonImage;
    [SerializeField] private TMPro.TextMeshProUGUI Cost;
    [SerializeField] private TMPro.TextMeshProUGUI Level;

    [Header("Upgrade Target")]
    [SerializeField] private Upgrade _Upgrade;

    [Header("Cost Info")]
    [SerializeField] private int IncreaseCost;
    [SerializeField] private int  UpgradeCost;

    private int mLevel = 0;

    private void Update()
    {
        // 업그레이드 가능, 혹은 최대 레벨일 때
        if (mLevel >= MAX_LEVEL || 
            XPManager.Instance.CanSubtractXP(UpgradeCost))
        {
            ButtonImage.color = Color.white;
        }
        else
        {
            ButtonImage.color = Color.white * 0.5f + Color.black;
        }
    }

    public void Upgrade()
    {
        if (mLevel < MAX_LEVEL)
        {
            var ability = BrickAbility.Instance;

            if (XPManager.Instance.SubtractXP(UpgradeCost))
            {
                SoundManager.Instance.PlaySound(Sounds.LevelUp);

                switch (_Upgrade)
                {
                    case global::Upgrade.BrickPower:
                        ability.AttackPower *= 1.1f;
                        break;

                    case global::Upgrade.FirePotSpeed:
                        ability.FirePotAccelation += FIREPOT_ACCELATION_PERCENT;
                        break;

                    case global::Upgrade.SpecialBrick:
                        if (mLevel >= MAX_LEVEL / 2)
                        {
                            ability.Special += SPECIAL_APPEARANCE_PERCENT / 2;
                        }
                        else 
                            ability.Special += SPECIAL_APPEARANCE_PERCENT;
                        break;
                }
                UpgradeCost += IncreaseCost;
                mLevel++;

                if (mLevel >= MAX_LEVEL)
                {
                    Cost.text = $"BRICK";

                    Level.text = $"Lv.MAX";
                    Level.color = new Color(0.33f, 0.9f, 1f);
                }
                else
                {
                    Cost.text = $"{UpgradeCost}XP";
                    Level.text = $"Lv.{mLevel}";
                }
            }
        }
    }
}
