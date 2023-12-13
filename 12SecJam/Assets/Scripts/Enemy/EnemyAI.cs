using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAI : MonoBehaviour
{

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    bool reachedEndOfPath = false;

    Path path;
    int currentWaypoint = 0;
    Seeker seeker;
    Rigidbody2D rb;




    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.3f);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(path == null)
        {
            return;

        }
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;

        }
        else
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint+1] - (Vector2)path.vectorPath[currentWaypoint]).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
            rb.AddForce(force);
            reachedEndOfPath = false;
        }
        
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);

        }
        
    }
}
