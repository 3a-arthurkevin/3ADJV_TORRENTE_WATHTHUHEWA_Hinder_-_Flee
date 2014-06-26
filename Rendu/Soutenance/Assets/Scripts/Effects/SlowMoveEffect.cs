using UnityEngine;
using System.Collections;

public class SlowMoveEffect : IEffect
{
    private float m_speedReduce = 0.2f;
    public float SpeedReduce
    {
        get { return m_speedReduce; }
        set { m_speedReduce = value; }
    }
    private float m_timeToReduce = 5f;
    public float TimeToReduce
    {
        get { return m_timeToReduce; }
        set { m_timeToReduce = value; }
    }

    private GameObject m_target;

    public override void Apply(GameObject target)
    {
        m_target = target;

        if (m_target.tag.Equals("Zombie"))
        {
            target.GetComponent<MoveManagerZombieScript>().StartCoroutine(ReduceSpeed());
        }
        else if (m_target.tag.Equals("Survivor"))
        {
            target.GetComponent<MoveManagerSurvivorScript>().StartCoroutine(ReduceSpeed());
        }
        else
            Debug.LogError("Target can't be slow");
    }

    public IEnumerator ReduceSpeed()
    {
        if (m_target.tag.Equals("Zombie"))
        {
            MoveManagerZombieScript move = m_target.GetComponent<MoveManagerZombieScript>();

            move.Data.Speed -= m_speedReduce;

            yield return new WaitForSeconds(m_timeToReduce);

            move.Data.Speed += m_speedReduce;

            m_target = null;
        }
        else if (m_target.tag.Equals("Survivor"))
        {
            MoveManagerSurvivorScript move = m_target.GetComponent<MoveManagerSurvivorScript>();

            move.MoveData.Speed -= m_speedReduce;

            yield return new WaitForSeconds(m_timeToReduce);

            move.MoveData.Speed += m_speedReduce;

            m_target = null;
        }
        else
            Debug.LogError("Target can't be slow");
    }

    public override void SetParam(System.Collections.Generic.Dictionary<string, string> param)
    {

    }

    public override void SetParam(string name, string value)
    {

    }
}
