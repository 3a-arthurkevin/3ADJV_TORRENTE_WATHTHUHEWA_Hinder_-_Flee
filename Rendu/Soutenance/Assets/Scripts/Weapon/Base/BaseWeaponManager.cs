using UnityEngine;
using System.Collections;

public abstract class BaseWeaponManager : MonoBehaviour
{
    [SerializeField]
    protected string m_name;
    public string Name
    {
        get { return m_name; }
        set { m_name = value; }
    }

    [SerializeField]
    protected NetworkView m_networkView = null;

    [SerializeField]
    protected Transform m_weaponOwner;
    public Transform Player
    {
        get { return m_weaponOwner; }
        set { m_weaponOwner = value; }
    }

    protected abstract void initSkill();
    protected abstract void LaunchSkill(Vector3 hit);
}
