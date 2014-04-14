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
                throw new System.ArgumentException();
        }

        return effect;
    }
}
