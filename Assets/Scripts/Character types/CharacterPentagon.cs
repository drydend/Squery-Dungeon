﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPentagon : Character
{
    public override void UseAbility()
    {
        
    }

    public override void Initialize()
    {
        CharacterType = CharacterType.Pentagon;
        base.Initialize();
    }

    public override void Attack(Vector3 targetPosition)
    {
        throw new System.NotImplementedException();
    }

    public override void IncreaseStat(StatType statType, float value)
    {
        throw new System.NotImplementedException();
    }

    public override void DecreaseStat(StatType statType, float value)
    {
        throw new System.NotImplementedException();
    }
}
