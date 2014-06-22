using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class IEffect
{
    public abstract void Apply(GameObject target);

    public abstract void SetParam(Dictionary<string, string> param);

    public abstract void SetParam(string name, string value);
}
