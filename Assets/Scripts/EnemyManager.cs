using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private TMPro.TextMeshProUGUI HPText;
    [SerializeField] private TMPro.TextMeshProUGUI LevelText;

    [SerializeField] private float HealthScaling;
    [SerializeField] private float MaxHealth;
    [SerializeField] private Enemy[] Enemies;


    private int mCurrentEnemyIndex;
    private int mLevel;

    private void Start()
    {
        mLevel = 1;
        mCurrentEnemyIndex = 0;

        Enemies[mCurrentEnemyIndex].Init(MaxHealth);
    }

    private void Update()
    {
        var currentEnemy = Enemies[mCurrentEnemyIndex];

        int curHP = Mathf.FloorToInt(currentEnemy.CurHealth);
        int maxHP = Mathf.FloorToInt(currentEnemy.MaxHealth);

        HPText.text = $"{curHP} / {maxHP}";

        if (!currentEnemy.gameObject.activeSelf)
        {
            mCurrentEnemyIndex = mCurrentEnemyIndex < Enemies.Length - 1 ? mCurrentEnemyIndex + 1 : 0;

            Enemies[mCurrentEnemyIndex].Init(MaxHealth *= HealthScaling);

            LevelText.text = $"Lv.{++mLevel}";
        }
    }
}
