using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Character stat upgrade")]
public class StatUpgrade : PowerUp
{
    [SerializeField]
    private StatType _statType;
    [SerializeField]
    private float _value = 0f;

    public float Value => _value;
    public StatType StatType => _statType;
}
