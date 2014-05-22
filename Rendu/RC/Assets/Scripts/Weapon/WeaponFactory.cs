using UnityEngine;
using System.Collections;

public static class WeaponFactory
{
    [SerializeField]
    private static GameObject m_prefabs;

    public static GameObject getWeaponById(int idWeapon)
    {
        GameObject weapon = null;
        switch (idWeapon)
        {
            case 0:
                weapon = (GameObject)Resources.Load("Prefabs/Weapons/Weapon");
                break;
            
            default:
                Debug.Log("Weapon not found");
                break;
        }

        return weapon;
    }
}
