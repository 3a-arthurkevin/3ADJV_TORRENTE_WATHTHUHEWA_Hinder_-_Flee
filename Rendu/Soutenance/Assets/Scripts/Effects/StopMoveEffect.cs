using UnityEngine;
using System.Collections;

public class StopMoveEffect : IEffect
{
    float m_effectDuration = 3f;

    public StopMoveEffect(float duration)
        : base()
    {
        m_effectDuration = duration;
    }

    public override void Apply(GameObject target)
    {
        if (target.tag.Equals("Zombie"))
        {
            target.GetComponent<MoveManagerZombieScript>().StartCoroutine(StopCharater(target));
        }
        else if (target.tag.Equals("Survivor"))
        {
            target.GetComponent<MoveManagerSurvivorScript>().StartCoroutine(StopCharater(target));
        }
        else
            Debug.LogError("Target can't be stop");
    }

    public IEnumerator StopCharater(GameObject target)
    {
        if (target.tag.Equals("Zombie"))
        {
            MoveManagerZombieScript move = target.GetComponent<MoveManagerZombieScript>();

            float defaultSpeed = move.getDefaultSpeed();
            move.Data.Speed = 0;

            yield return new WaitForSeconds(m_effectDuration);

            move.Data.Speed = defaultSpeed;
        }
        else if (target.tag.Equals("Survivor"))
        {
            MoveManagerSurvivorScript move = target.GetComponent<MoveManagerSurvivorScript>();
            
            float defaultSpeed = move.getDefaultSpeed();
            move.MoveData.Speed = 0;

            yield return new WaitForSeconds(m_effectDuration);

            move.MoveData.Speed = defaultSpeed;
        }
        else
            Debug.LogError("Target can't be stop");
    }

    public override void SetParam(System.Collections.Generic.Dictionary<string, string> param)
    {
    }

    public override void SetParam(string name, string value)
    {
    }
}

