﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IEffectable
{
    public void ApplyEffect(Effect effect);
    public void RemoveEffect(Type type);
    public bool CanApplyEffect(Effect effect);
}

