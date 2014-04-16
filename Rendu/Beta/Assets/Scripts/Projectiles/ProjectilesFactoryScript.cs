using UnityEngine;
using System.Collections;

public static class ProjectilesFactoryScript
{
    /*****Factory des projectiles*****
     * Loader le fichier de config et Instancier le projectile demander avec les bon paramètres en fonction
     * du contenu du fichier de config
     */

    public static GameObject getProjectileById(int idProjectile)
    {
        GameObject projectile = null;

        switch (idProjectile)
        {
            case 0:
                projectile = (GameObject)Resources.Load("Prefabs/Projectils/");
                break;

            default:
                Debug.Log("Projectile not found");
                break;
        }

        return projectile;
    }
}
