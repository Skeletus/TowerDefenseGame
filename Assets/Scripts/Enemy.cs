using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    Basic,
    Fast,
    None
}

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Center point")]
    [SerializeField] private Transform centerPoint;
    [Header("Waypoints")]
    [SerializeField] private List<Transform> myWaypoints;
    [Header("Move settings")]
    [SerializeField] private float turnSpeed = 1f;
    [Header("Health Settings")]
    [SerializeField] private int healthPoints = 4;
    [Header("Enemy Type")]
    [SerializeField] private EnemyType enemyType;

    private EnemyPortal myPortal;
    private NavMeshAgent agent;
    private GameManager gameManager;

    private int nextWaypointIndex = 0;
    private float totalDistance;
    private int currentWaypointIndex = 0;

    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.avoidancePriority = Mathf.RoundToInt(agent.speed * 10);
    }

    public void SetupEnemy(List<Waypoint> newWaypoints, EnemyPortal myNewPortal)
    {
        myWaypoints = new List<Transform>();

        foreach(var point in  newWaypoints)
        {
            myWaypoints.Add(point.transform);
        }

        CollectTotalDistance();

        myPortal = myNewPortal;
    }

    private void Update()
    {
        FaceTarget(agent.steeringTarget);

        if (ShouldChangeWaypoint())
        {
            agent.SetDestination(GetNextWaypoint());
        }
    }

    private bool ShouldChangeWaypoint()
    {
        if (nextWaypointIndex >= myWaypoints.Count)
        {
            return false;
        }

        if (agent.remainingDistance < .5f)
        {
            return true;
        }

        Vector3 currentWaypoint = myWaypoints[currentWaypointIndex].position;
        Vector3 nextWapoint = myWaypoints[nextWaypointIndex].position;

        float distanceToNextWaypoint = Vector3.Distance(transform.position, nextWapoint);
        float distanceBetweenPoints = Vector3.Distance(currentWaypoint, nextWapoint);

        if (distanceBetweenPoints > distanceToNextWaypoint)
        {
            return true;
        }

        return false;
    }

    public float DistanceToFinishLine()
    {
        return totalDistance + agent.remainingDistance;
    }

    private void FaceTarget(Vector3 newTarget)
    {
        Vector3 directionTarget = newTarget - transform.position;
        directionTarget.y = 0;

        Quaternion newRotation = Quaternion.LookRotation(directionTarget);

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, turnSpeed * Time.deltaTime);
    }

    private void CollectTotalDistance()
    {
        for (int i = 0; i < myWaypoints.Count - 1; i++)
        {
            float distance = Vector3.Distance(myWaypoints[i].position, myWaypoints[i + 1].position);

            totalDistance += distance;
        }
    }

    private Vector3 GetNextWaypoint()
    {
        if(nextWaypointIndex >= myWaypoints.Count)
        {
            //waypointIndex = 0;
            return transform.position;
        }
        Vector3 targetPoint = myWaypoints[nextWaypointIndex].position;

        if(nextWaypointIndex > 0)
        {
            float distance = Vector3.Distance(myWaypoints[nextWaypointIndex].position, myWaypoints[nextWaypointIndex -1].position);
            totalDistance -= distance;
        }

        nextWaypointIndex++;
        currentWaypointIndex = nextWaypointIndex - 1;

        return targetPoint;
    }

    public Vector3 CenterPoint()
    {
        return centerPoint.position;
    }

    public EnemyType GetEnemyType()
    {
        return enemyType;
    }

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;

        if (healthPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        myPortal.RemoveActiveEnemy(gameObject);
        gameManager.UpdateCurrency(1);
        Destroy(gameObject);
    }

    public void DestroyEnemy()
    {
        myPortal.RemoveActiveEnemy(gameObject);
        Destroy(gameObject);
    }
}
