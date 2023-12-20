using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    public LayerMask targetLayers;
    public int rayCount = 20;
    public float rayLength = 5f;
    float raySpreadAngle = 360f;

    [SerializeField] GameObject wpPath;
    List<GameObject> waypoints;
    int waypointIndex = 0;
    public Transform target;
    public float speed = 200f;
    public float followTime = 5f;
    float startFollowTime;
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
    [SerializeField] SeekerState state;

    void Start()
    {
        startFollowTime = followTime;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        InvokeRepeating("UpdatePath", 0f, 0.3f);
        state = SeekerState.ZANZANDO;
        waypoints = new List<GameObject>();
        for (int i = 0; i < wpPath.transform.childCount; i++)
        {
            waypoints.Add(wpPath.transform.GetChild(i).gameObject);
        }
        target = waypoints[0].transform;
    }
    private void Update()
    {
        CastRaycasts();
        if (target != null) 
        {
            sr.flipX = target.transform.position.x < transform.position.x;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(state == SeekerState.SEGUINDO)
        {
            GoToTarget();
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

    void GoToTarget()
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
            if (waypointIndex >= waypoints.Count-1)
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
            GoToTarget();
        }
    }

    void CastRaycasts()
    {
        float angleStep = raySpreadAngle / rayCount;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.right;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, targetLayers);

            if (hit.collider != null)
            {
                if(hit.collider.gameObject.CompareTag("Player"))
                {
                    state = SeekerState.SEGUINDO;
                    target = hit.collider.gameObject.transform;
                    followTime = startFollowTime;
                }
                else
                {
                    followTime -= Time.deltaTime/rayCount;
                    if (followTime < 0 && state == SeekerState.SEGUINDO)
                    {
                        state = SeekerState.ZANZANDO;
                        target = waypoints[Random.Range(0, waypoints.Count-1)].transform;
                    }
                }
                //Acertou
                Debug.DrawRay(transform.position, direction * hit.distance, Color.red);
            }
            else
            {
                //Não acertou

                Debug.DrawRay(transform.position, direction * rayLength, Color.green);
                
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }

}
