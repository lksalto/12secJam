using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 15f;
    public Vector2 forceToApply;
    public float forceDamping;

    [SerializeField] FieldOfView fieldOfView;
    [SerializeField] FieldOfView fieldOfView1;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Aim();
    }

    void Move()
    {
        Vector2 PlayerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;
        Vector2 moveForce = PlayerInput;
        moveForce += forceToApply;
        forceToApply /= forceDamping;
        if(Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }
        rb.velocity = moveForce;
    }
    void Aim()
    {
        // Get the mouse position in world space
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction vector from the object to the mouse position
        Vector3 aimDir = (targetPosition - transform.position).normalized;

        // Calculate the rotation to face the mouse position
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Apply the rotation to the object
        transform.rotation = lookRotation;

        // Optionally, set the aim direction and origin for your field of view logic
        fieldOfView.SetAimDirection(aimDir);
        fieldOfView.SetOrigin(transform.position);
        fieldOfView1.SetAimDirection(aimDir);
        fieldOfView1.SetOrigin(transform.position);
    }
}
