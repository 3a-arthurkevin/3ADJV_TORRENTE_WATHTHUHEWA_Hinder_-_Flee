using UnityEngine;
using System.Collections;

public static class MoveUtilsScript
{

    static public NavMeshPath getCalcPath(Vector3 origin, Vector3 wantToGo)
    {
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(origin, wantToGo, -1, path);

        if (path.corners.Length > 1)
            return path;

        else
            return null;
    }
}
