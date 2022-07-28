using UnityEngine;

public class StatUpgrade : PlayerUpgrade
{   
    [SerializeField]
    protected float _value = 0f;

    public float Value => _value;

    public override void ApplyUpgrade(Player player)
    {
        
    }

    public override void RevertUpgrade(Player player)
    {
        
    }
}
