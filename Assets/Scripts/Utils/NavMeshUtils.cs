using UnityEngine;
using UnityEngine.AI;

public static class NavMeshUtils
{
    public static Vector3 GetRandomPositionOnSurface()
    {
        //Hardcoded radius of the location
        const float radius = 25;

        Vector3 position = Random.insideUnitSphere * radius;

        return GetValidPosition(position);
    }

    public static Vector3 GetValidPosition(Vector3 position)
    {
        if (NavMesh.SamplePosition(position, out NavMeshHit hit, float.MaxValue, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            return position;
        }
    }
}
