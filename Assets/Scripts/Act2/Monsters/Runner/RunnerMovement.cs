using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterState
{ 
    Chase,
    Patrol,
    Distraction,
    Idle,
    Checking,
    PreparedToCheck,
}
public class RunnerMovement : MonoBehaviour
{
    public Transform CurrentTarget;
    public Transform SoundPlace;
    public Transform CheckingTarget;

    public MonsterState State;

    [SerializeField] private List<Transform> Waypoints;

    [SerializeField] private FieldOfView FOV;

    [SerializeField] private Transform PlayerLastKnownPosition;
    [SerializeField] private Transform PlayerPosition;

    [SerializeField] private float NormalSpeed = 3.5f;
    [SerializeField] private float ChaseSpeed = 5.25f;

    private float toSoundPlaceSpeed;

    private int currentWaypointIndex = 0;

    private NavMeshAgent meshAgent;


    private void Start()
    {
        toSoundPlaceSpeed = NormalSpeed - 1;

        meshAgent = GetComponent<NavMeshAgent>();

        CurrentTarget = Waypoints[currentWaypointIndex];

        CheckingTarget = transform;
    }
    private void FixedUpdate()
    {
        TryChangeWaypointIndex();

        ControlIndex();

        GetTarget();

        Move();

        if (State == MonsterState.Distraction && SoundPlace != null)
        {
            CheckForSoundplaceDistance();
        }
    }
    #region Public Methods
    public void StartChase()
    {
        meshAgent.speed = ChaseSpeed;

        SetState(MonsterState.Chase);
    }
    public void PrepareToCheck()
    {
        SetState(MonsterState.PreparedToCheck);
    }
    private void SetState(MonsterState newState)
    {
        State = newState;
    }
    public bool IsChecking()
    {
        return State is MonsterState.Checking or MonsterState.Checking;
    }
    public void StopMoving()
    {
        SetState(MonsterState.Idle);
    }
    public void ResetCheckingTarget()
    {
        CheckingTarget = transform;
    }
    public void ContinuePatrolling()
    {
        SetState(MonsterState.Patrol);
    }
    public void StartChecking()
    {
        SetState(MonsterState.Checking);
    }
    public void ResetTarget()
    {
        float lowestDistance = float.MaxValue;

        Transform nearestPoint = Waypoints[0];

        foreach(var point in Waypoints)
        {
            float distance = FindDistanceToTarget(point);

            if(distance < lowestDistance)
            {
                nearestPoint = point;

                lowestDistance = distance;
            }
        }

        CurrentTarget = nearestPoint;
    }
    public void SetSoundPlace(Transform soundPlace)
    {
        if (State == MonsterState.Chase)
        {
            return;
        }

        SoundPlace = soundPlace;

        SetState(MonsterState.Distraction);
    }
    public NavMeshPath FindPathToTarget(Transform target)
    {
        NavMeshPath Path = new NavMeshPath();

        NavMeshPath ShortestPath = null;

        float distance;

        if (NavMesh.CalculatePath(transform.position, target.position, meshAgent.areaMask, Path))
        {
            distance = Vector3.Distance(transform.position, Path.corners[0]);
        }
        else
        {
            Debug.LogError("Path is unaccessable!");

            return null;
        }

        for (int j = 1; j < Path.corners.Length; j++)
        {
            distance += Vector3.Distance(Path.corners[j - 1], Path.corners[j]);
        }

        if (ShortestPath != null)
        {
            return ShortestPath;
        }
        else
        {
            return Path;
        }
    }
    public float FindDistanceToTarget(Transform target)
    {
        NavMeshPath Path = new NavMeshPath();

        float distance;

        if (NavMesh.CalculatePath(transform.position, target.position, meshAgent.areaMask, Path))
        {
            distance = Vector3.Distance(transform.position, Path.corners[0]);
        }
        else
        {
            Debug.LogError("Path is unaccessable!");

            return float.MaxValue;
        }

        for (int j = 1; j < Path.corners.Length; j++)
        {
            distance += Vector3.Distance(Path.corners[j - 1], Path.corners[j]);
        }


        return distance;
    }
    #endregion

    #region PrivateMethods
    private void ResetSoundPlace()
    {
        SetState(MonsterState.Patrol);

        SoundPlace = null;
    }
    private void CheckForSoundplaceDistance()
    {
        if(Vector3.Distance(transform.position, SoundPlace.position) < 5f) 
        {
            meshAgent.destination = transform.position;

            Invoke(nameof(DisableSoundbaitMovement), 8f);
        }
    }
    private void DisableSoundbaitMovement()
    {
        ResetSoundPlace();

        ResetTarget();
    }
    private void GetTarget()
    {
        switch (State)
        {
            case MonsterState.Chase:
                if(FOV.canSeePlayer)
                {
                    CurrentTarget = PlayerPosition;

                    meshAgent.speed = ChaseSpeed;

                    ResetSoundPlace();
                }
                else
                {
                    CurrentTarget = PlayerLastKnownPosition;

                    meshAgent.speed = ChaseSpeed;

                    ResetSoundPlace();
                }
                break;
            case MonsterState.Distraction:
                if(SoundPlace != null)
                {
                    CurrentTarget = SoundPlace;
                }
                break;
            case MonsterState.Patrol:
                CurrentTarget = Waypoints[currentWaypointIndex];

                meshAgent.speed = NormalSpeed;
                break;
            case MonsterState.Idle:
                CurrentTarget = transform;
                break;
            case MonsterState.Checking:
                CurrentTarget = CheckingTarget;
                break;
        }
    }
    private void Move()
    {
        meshAgent.destination = CurrentTarget.position;
    }
    private void ControlIndex()
    {
        if (currentWaypointIndex > Waypoints.Count - 1)
        {
            currentWaypointIndex = 0;
        }
    }
    private void TryChangeWaypointIndex()
    {
        if(IsNearPoint())
        {
            currentWaypointIndex++;
        }
    }
    private bool IsNearPoint()
    {
        Vector3 distance = Waypoints[currentWaypointIndex].position - transform.position;

        float sqrDist = distance.sqrMagnitude;

        return sqrDist < 4f;
    }
    #endregion
}
