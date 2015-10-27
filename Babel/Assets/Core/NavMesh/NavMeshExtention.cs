using UnityEngine;

namespace Assets.Core.NavMesh
{
    public static class NavMeshExtention {

        public static bool HasReachedTarget(this NavMeshAgent mNavMeshAgent)
        {
            return mNavMeshAgent.remainingDistance < 1f;
            //if (mNavMeshAgent.pathPending) return false;
            //return mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance &&
            //       (!mNavMeshAgent.hasPath || mNavMeshAgent.velocity.sqrMagnitude < 1f);
        }
    }
}
