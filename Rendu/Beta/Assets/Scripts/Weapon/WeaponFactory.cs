using UnityEngine;
using System.Collections;

public static class WeaponFactory
{
    [SerializeField]
    private static GameObject m_prefabs;

    public static GameObject getWeaponById(int idWeapon)
    {
        return (GameObject)GameObject.Instantiate(m_prefabs);
    }
}
