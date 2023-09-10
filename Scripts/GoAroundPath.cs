using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoAroundPath : MonoBehaviour
{
    [SerializeField] WaveConfigSo waveConfigSo;
    List<Transform> waypoints;
    int waypointIndex;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfigSo.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
  }

    // Update is called once per frame
    void Update()
    {
        TravelPath();
    }

    private void TravelPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfigSo.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else if (waypointIndex == waypoints.Count)
        {
            Vector3 targetPosition2 = waypoints[waypointIndex-1].position;
            Vector3 targetPosition3 = waypoints[waypointIndex-2].position;
            Vector3 targetPosition4 = waypoints[waypointIndex-3].position;
            Vector3 targetPosition5 = waypoints[waypointIndex-4].position;
            Vector3 targetPosition6 = waypoints[waypointIndex-5].position;
            float delta2 = waveConfigSo.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition2, delta2);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition3, delta2);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition4, delta2);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition5, delta2);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition6, delta2);
       if(transform.position == targetPosition6)
            {
                waypointIndex = 0;
            }
        }
        }
}
