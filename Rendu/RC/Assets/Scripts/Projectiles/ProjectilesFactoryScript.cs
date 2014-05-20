using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ProjectilesFactoryScript
{
    /*****Factory des projectiles*****
     * Loader le fichier de config et Instancier le projectile demander avec les bon paramètres en fonction
     * du contenu du fichier de config
     */

    //private static List<GameObject> m_listProjectile = new List<GameObject>();

    public static GameObject getProjectileById(int idProjectile)
    {
        GameObject projectile = null;

        switch (idProjectile)
        {
            case 0:
                projectile = (GameObject)Resources.Load("Prefabs/Projectiles/ProjectileSingleTarget");
                break;

            case 1:
                projectile = (GameObject)Resources.Load("Prefabs/Projectiles/ProjectileAOE");
                break;

            default:
                Debug.Log("Projectile not found");
                break;
        }

        return projectile;
    }
}
