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
    [SerializeField] private Image ButtonImage;
    [SerializeField] private TMPro.TextMeshProUGUI Cost;
    [SerializeField] private TMPro.TextMeshProUGUI Level;

    [Header("Upgrade Target")]
    [SerializeField] private Upgrade _Upgrade;

    [Header("Cost Info")]
    [SerializeField] private int IncreaseCost;
    [SerializeField] private int  UpgradeCost;

    private int mLevel = 1;

    private void Update()
    {
        if (XPManager.Instance.CanSubtractXP(UpgradeCost))
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
                    // To do...
                    break;

                case global::Upgrade.SpecialBrick:
                    ability.Special += 0.05f;
                    break;
            }
             UpgradeCost += IncreaseCost;

             Cost.text = $"{UpgradeCost}XP";
            Level.text = $"Lv.{++mLevel}";
        }
    }
}
