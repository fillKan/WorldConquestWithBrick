using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickAbility : Singleton<BrickAbility>
{
    [SerializeField] private float _AttackPower;
    public float AttackPower 
    {
        get => _AttackPower;
        set => _AttackPower = value;
    }

    [SerializeField] private float _FirePotAccelation;
    public float FirePotAccelation
    {
        get => _FirePotAccelation;
        set => _FirePotAccelation = value;
    }

    [Header("Exp")]
    [SerializeField] private float _ExpScaling;
    public float ExpScaling
    {
        get => _ExpScaling;
        set => _ExpScaling = value;
    }
    
    [Range(0f, 1f)]
    [SerializeField] private float _Special;
    public float Special
    {
        get => _Special;
        set => _Special = value;
    }
}
