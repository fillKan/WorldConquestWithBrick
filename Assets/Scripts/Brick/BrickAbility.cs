using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickAbility : Singleton<BrickAbility>
{
    [SerializeField] private float _AttackPower;
    public float AttackPower 
    {
        get
        {
            return _AttackPower;
        }
        set => _AttackPower = value;
    }

    [Header("Critcal")]

    [Range(0f,1f)]
    [SerializeField] private float _CritcalProbability;
    [SerializeField] private float _CritcalScaling;
    public float CritcalProbability 
    { 
        get => _CritcalProbability; 
        set => _CritcalProbability = value; 
    }
    public float CritcalScaling
    {
        get => _CritcalScaling;
        set => _CritcalScaling = value;
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
    public float PureAttackPower()
    {
        return _AttackPower;
    }
}
