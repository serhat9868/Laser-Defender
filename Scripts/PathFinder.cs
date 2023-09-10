using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] WaveConfigSo WaveConfig;
 
    List<Transform> waypoints;
    int waypointIndex = 0;
    private void Start()
    {
        waypoints = WaveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }
    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
      if(waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = WaveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if(transform.position == targetPosition)
            {
                waypointIndex++;
                
            }
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
