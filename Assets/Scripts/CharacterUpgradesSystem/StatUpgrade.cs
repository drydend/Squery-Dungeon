using UnityEngine;

public class StatUpgrade : Upgrade
{   
    [SerializeField]
    protected float _value = 0f;

    public float Value => _value;

    public override string GetDiscription()
    {
        return _discription + _value.ToString();
    }
}
