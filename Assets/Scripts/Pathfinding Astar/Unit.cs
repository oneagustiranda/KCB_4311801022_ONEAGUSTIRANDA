﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Transform target;
    public float speed = 2;

    Vector2[] path;
    int targetIndex;

    void Start()
    {
        StartCoroutine(RefreshPath());
    }

    IEnumerator RefreshPath()
    {
        Vector2 targetPositionold = (Vector2)target.position + Vector2.up;

        while (true)
        {
            if (targetPositionold != (Vector2)target.position)
            {
                targetPositionold = (Vector2)target.position;

                path = Pathfinding.RequestPath(transform.position, target.position);

                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
            yield return new WaitForSeconds(.25f);
        }
    }

    IEnumerator FollowPath()
    {
        if (path.Length > 0)
        {
            targetIndex = 0;
            Vector2 currentWaypoint = path[0];

            while (true)
            {
                if ((Vector2)transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }
                transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.yellow;

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                } else
                {
                    Gizmos.DrawLine(path[i - 1], path[1]);
                }
            }
        }
    }
}
