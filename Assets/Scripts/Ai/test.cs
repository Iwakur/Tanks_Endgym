using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BotAI : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    [Header("AI Settings")]
    public float updateRate = 0.2f; // seconds between path updates
    private float nextUpdate;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (!agent.isOnNavMesh)
        {
            Debug.LogError($"{name} is NOT on NavMesh!");
        }
    }

    void Update()
    {
        if (target == null || !agent.isOnNavMesh) return;

        // Update only every "updateRate" seconds (avoids spam + performance)
        if (Time.time >= nextUpdate)
        {
            nextUpdate = Time.time + updateRate;
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        // Snap target to NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(target.position, out hit, 5f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            Debug.Log($"{name} → Updating destination {hit.position} | Remaining {agent.remainingDistance:F2}");
        }
        else
        {
            Debug.LogWarning($"{name}: Target not on NavMesh! {target.position}");
        }
    }

    void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.position);
            Gizmos.DrawSphere(target.position, 0.3f);
        }
    }
}
