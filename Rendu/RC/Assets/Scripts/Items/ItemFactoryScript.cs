using UnityEngine;
using System.Collections;

public static class ItemFactoryScript 
{
    public static GameObject getItemById(int idItem)
    {
        GameObject item = null;

        switch (idItem)
        {
            case 0:
                item = (GameObject)Resources.Load("Prefabs/Items/Potion");
                Debug.LogError("Potion");
                break;

            case 1:
                item = (GameObject)Resources.Load("Prefabs/Items/PiegeLoup");
                Debug.LogError("PiegeLoup");
                break;

            default:
                Debug.Log("Projectile not found");
                break;
        }

        return item;
    }
}
