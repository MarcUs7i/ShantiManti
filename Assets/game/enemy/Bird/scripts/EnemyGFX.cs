using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;
    
    private void Update()
    {
        transform.localScale = aiPath.desiredVelocity.x switch
        {
            >= 0.01f => new Vector3(-1f, 1f, 1f),
            <= -0.01f => new Vector3(1f, 1f, 1f),
            _ => transform.localScale
        };
    }
}
