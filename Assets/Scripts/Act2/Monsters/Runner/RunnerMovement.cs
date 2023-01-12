using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunnerMovement : MonoBehaviour
{
    public Transform CurrentTarget;
    public Transform SoundPlace;

    [SerializeField] private List<Transform> Waypoints;

    [SerializeField] private FieldOfView FOV;

    [SerializeField] private Transform Player;

    [SerializeField] private float NormalSpeed = 3.5f;
    [SerializeField] private float ChaseSpeed = 5.25f;

    private float toSoundPlaceSpeed;

    private bool chasingPlayer = false;
    private bool goingToSoundPlace = false;

    private int currentWaypointIndex = 0;

    private NavMeshAgent meshAgent;

    private void Start()
    {
        toSoundPlaceSpeed = NormalSpeed - 1;

        meshAgent = GetComponent<NavMeshAgent>();

        CurrentTarget = Waypoints[currentWaypointIndex];
    }

    private void Update()
    {
        TryChangeWaypointIndex();

        ControlIndex();

        GetTarget();

        Move();

        if (goingToSoundPlace && SoundPlace != null)
        {
            CheckForSoundplaceDistance();
        }
    }
    #region Public Methods
    public void StartChase()
    {
        chasingPlayer = true;
    }
    public void ResetTarget()
    {
        chasingPlayer = false;

        float lowestDistance = float.MaxValue;

        Transform nearestPoint = Waypoints[0];

        foreach(var point in Waypoints)
        {
            Vector3 offset = point.position - transform.position;

            float distance = offset.sqrMagnitude;

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
        SoundPlace = soundPlace;

        goingToSoundPlace = true;
    }
    #endregion
    #region PrivateMethods
    private void ResetSoundPlace()
    {
        goingToSoundPlace = false;

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
        if (chasingPlayer)
        {
            CurrentTarget = Player;

            meshAgent.speed = ChaseSpeed;

            return;
        }
        else if(goingToSoundPlace && SoundPlace != null)
        {
            CurrentTarget = SoundPlace;
        }
        else
        {
            CurrentTarget = Waypoints[currentWaypointIndex];

            meshAgent.speed = NormalSpeed;
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
        return Mathf.Approximately(transform.position.x, Waypoints[currentWaypointIndex].position.x) && Mathf.Approximately(transform.position.z, Waypoints[currentWaypointIndex].position.z);
    }
    #endregion
}
