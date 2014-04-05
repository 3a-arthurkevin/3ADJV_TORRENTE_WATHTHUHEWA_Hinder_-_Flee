using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IEffect
{
    void Apply(GameObject target);
    void SetParam(Dictionary<string, string> param);
}
