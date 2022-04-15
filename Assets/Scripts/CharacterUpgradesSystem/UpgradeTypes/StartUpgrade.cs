using UnityEngine;

[CreateAssetMenu(menuName = "Character upgrades/Stat upgrade", fileName ="Upgrade")]
public class StatUpgrade : Upgrade
{
    [SerializeField]
    private StatType _statType;
    [SerializeField]
    private float _value = 0f;

    public float Value => _value;
    public StatType StatType => _statType;
}
