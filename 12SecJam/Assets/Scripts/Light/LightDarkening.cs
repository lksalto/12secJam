using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightDarkening : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private Light2D globalLight;

    [SerializeField]private float distanceToPlayer;
    float globalLightIntensity;
    private float maxDistance;
    // Start is called before the first frame update
    void Start()
    {
        maxDistance = Vector3.Distance(player.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //Calculates distance to player
        distanceToPlayer = transform.position.x - player.position.x;
        globalLightIntensity =  distanceToPlayer > 0f ? (distanceToPlayer / maxDistance)*0.5f : 0f;
        if(globalLightIntensity is < 0.5f and > 0f)
            globalLight.intensity = globalLightIntensity;
        else if(globalLightIntensity >= 0.5f)
        {
            globalLight.intensity = 0.5f;
        }
        else
        {
            globalLight.intensity = 0f;
        }
    }
}
