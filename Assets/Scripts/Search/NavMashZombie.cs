using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMashZombie : ZombieInput
{
    [SerializeField] private Transform _player;
    [SerializeField] private RockMap _rockMap;
    
    private NavMeshPath path;
    private NavMeshPath path2;
    private float closestDistance;
    private float distance;
    private Vector3 closestRockDirection;
    private Transform _rockTr;
    private bool rocksEx = false;
    private bool found = false;

    private void Awake()
    {
        _player = transform.parent.GetComponent<PlayerContainer>().GetPlayerTransform();
        _rockMap = transform.parent.GetComponent<PlayerContainer>().GetRockMap();
    }

    public override (Vector3 moveDirection, Vector3 distance, Quaternion viewDirection, bool found) CurrentInput()
    {
        found = false;
        path = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, _player.position, NavMesh.AllAreas, path);
        if (path.status == NavMeshPathStatus.PathInvalid)
            return (Vector3.zero, Vector3.zero, Quaternion.identity, found);
        found = true;
        return (path.corners[1]-path.corners[0], path.corners[path.corners.Length-1] - path.corners[0], Quaternion.LookRotation(path.corners[path.corners.Length-1]), found);
    }

    public override (Vector3 moveRockDirection, float clDistance, Transform rock, bool rocksExist) GetRock()
    {
        closestDistance = 100;
        path2 = new NavMeshPath();
        
        if (_rockMap.RockTransforms().Capacity != 0)
        {
            foreach (var rocks in _rockMap.RockTransforms())
            {
                NavMesh.CalculatePath(transform.position, rocks.position, NavMesh.AllAreas, path2);
                distance = calcDistance(path2.corners);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestRockDirection = path2.corners[1] - path2.corners[0];
                    _rockTr = rocks;
                }
            }

            rocksEx = true;
        }
        else
        {
            rocksEx = false;
        }

        return (closestRockDirection, closestDistance, _rockTr, rocksEx);
    }

    private static float calcDistance(Vector3[] list)
    {
        float distance = 0;
        for(int i=0;i<list.Length-1;i++)
        {
            distance += (list[i + 1] - list[i]).magnitude;
        }

        return distance;
    }
}
