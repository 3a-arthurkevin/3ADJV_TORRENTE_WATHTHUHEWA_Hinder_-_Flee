using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfigLevelManager : MonoBehaviour {

   public static List<List<PointScript>> getMovePointForFirtsLevel()
    {
        List<List<PointScript>> levelOne    = new List<List<PointScript>>();
        List<PointScript> firstStair        = new List<PointScript>();
        PointScript point                   = ScriptableObject.CreateInstance<PointScript>();
        
        point.Level = 1;
        
        point.Position = new Vector3(-10, 0, 0);
        firstStair.Add(point.Clone());

        point.Position = new Vector3(0, 0, 0);
        firstStair.Add(point.Clone());

        point.Position = new Vector3(10, 0, 0);
        firstStair.Add(point.Clone());

        List<PointScript> secondStair = new List<PointScript>();
        point.Level = 2;

        point.Position = new Vector3(-10, 0, -35);
        secondStair.Add(point.Clone());

        point.Position = new Vector3(0, 0, -35);
        secondStair.Add(point.Clone());

        point.Position = new Vector3(10, 0, -35);
        secondStair.Add(point.Clone());

        levelOne.Add(firstStair);
        levelOne.Add(secondStair);

        return levelOne;
    }
}
