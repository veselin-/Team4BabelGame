using UnityEngine;

namespace Assets.Core.NavMesh
{
    public static class NavMeshExtention {

        public static bool HasReachedTarget(this NavMeshAgent agent)
        {
            return agent.remainingDistance < 0.2;
        }
    }
}
