using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Image ButtonImage;
    [SerializeField] private TMPro.TextMeshProUGUI Cost;
    [SerializeField] private TMPro.TextMeshProUGUI Level;

    [SerializeField] private float IncreaseIncreaseScale;

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

            // 공격력 10%증가
            ability.AttackPower *= 1.1f;

            // 치명타 확률 2.5%증가
            ability.CritcalProbability += 0.025f;

            // 치명타 피해량 10%증가
            ability.CritcalScaling += 0.1f;

            // 특수 벽돌 5%증가
            ability.Special += 0.05f;

             UpgradeCost += IncreaseCost;
            IncreaseCost = Mathf.FloorToInt(IncreaseCost * IncreaseIncreaseScale);
            IncreaseCost -= IncreaseCost % 10;

            Cost.text = $"{UpgradeCost}XP";

            Level.text = $"Lv.{++mLevel}";
        }
    }
}
