using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsLimits : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject[] limits;
    private int currentLimit = 0;

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(limits[currentLimit].transform.position, transform.position) < 0.1f)
            currentLimit = (currentLimit + 1) % limits.Length;
        transform.position = Vector2.MoveTowards(transform.position, limits[currentLimit].transform.position, speed * Time.deltaTime);
    }
}
