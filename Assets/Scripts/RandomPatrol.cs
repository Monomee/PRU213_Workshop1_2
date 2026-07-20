using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RandomPatrol : MonoBehaviour
{
    [Header("Waypoints Setup")]
    public Transform[] waypoints;

    private NavMeshAgent agent;
    private int lastIndex = -1;

    private float aiUpdateRate = 0.3f;
    private float nextUpdateTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        MoveToRandomWaypoint();
    }

    void Update()
    {
        if (Time.time >= nextUpdateTime)
        {
            nextUpdateTime = Time.time + aiUpdateRate;

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    MoveToRandomWaypoint();
                }
            }
        }
        
    }

    public void MoveToRandomWaypoint()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            return;
        }

        if (waypoints.Length == 1)
        {
            agent.SetDestination(waypoints[0].position);
            return;
        }

        int randomIndex = lastIndex;
        while (randomIndex == lastIndex)
        {
            randomIndex = Random.Range(0, waypoints.Length);
        }

        lastIndex = randomIndex;
        agent.SetDestination(waypoints[randomIndex].position);
    }
}