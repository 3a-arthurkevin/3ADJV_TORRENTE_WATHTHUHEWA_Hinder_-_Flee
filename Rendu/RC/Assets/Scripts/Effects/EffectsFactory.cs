using UnityEngine;
using System.Collections;

public static class EffectsFactory
{
    public static IEffect getEffect(int idEffect)
    {//Return Effect who have idEffect
        IEffect effect = null;

        switch (idEffect)
        {
            case 0:
                effect = new TakeDamageEffect();
                break;

            default :
                Debug.Log("Effect not found");
                break;
        }

        return effect;
    }
}
