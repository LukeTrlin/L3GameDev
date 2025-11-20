using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;

public class SpawnLocation : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Vector3 positionToPlace;

    void Start()
    {
        agent.Warp(positionToPlace);
    }
}