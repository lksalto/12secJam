using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] GameObject wpPath;
    List<GameObject> waypoints;
    int waypointIndex = 0;
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    bool reachedEndOfPath = false;

    Path path;
    int currentWaypoint = 0;
    Seeker seeker;
    Rigidbody2D rb;
    SpriteRenderer sr;

    enum SeekerState
    {
        ZANZANDO = 0,
        SEGUINDO = 1
    };
    SeekerState state;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        InvokeRepeating("UpdatePath", 0f, 0.3f);
        state = SeekerState.ZANZANDO;
        waypoints = new List<GameObject>();
        for (int i = 0; i < wpPath.transform.childCount - 1; i++)
        {
            Debug.Log(wpPath.transform.GetChild(i).name);
            waypoints.Add(wpPath.transform.GetChild(i).gameObject);
        }
        target = waypoints[0].transform;
    }
    private void Update()
    {
        if(target != null) 
        {
            sr.flipX = target.transform.position.x < transform.position.x;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(state == SeekerState.SEGUINDO)
        {
            FollowPlayer();
        }
        else
        {
            Zanzar();
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

    void FollowPlayer()
    {
        if (path == null)
        {
            return;

        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;

        }
        else
        {
            float distance2 = Vector2.Distance(transform.position, (Vector2)path.vectorPath[currentWaypoint]);
            transform.position = Vector2.Lerp(transform.position, (Vector2)path.vectorPath[currentWaypoint + 1], speed * Time.fixedDeltaTime);

            reachedEndOfPath = false;
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    public void Zanzar()
    {
       
        if(Vector2.Distance(target.transform.position, transform.position) < 0.3f)
        {
            if (waypointIndex == waypoints.Count - 1 )
            {
                waypointIndex = 0;
            }
            else
            {
                waypointIndex++;
            }
            target = waypoints[waypointIndex].transform;
        }
        else
        {
            FollowPlayer();
        }
    }

    public void MoveToTarget()
    {

    }

}
