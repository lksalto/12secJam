using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 3f;
    [SerializeField] float maxStamina = 100f;
    [SerializeField] float stamina;
    private readonly float staminaDecaySpeed = 20f;
    private bool isRunning;
    private bool outOfBreath;
    private bool canRun = true;
    public Vector2 forceToApply;
    public float forceDamping;
    [SerializeField] private Image staminaSlider;
    private bool isTransitioning;
    Vector2 PlayerInput;
    [SerializeField] Transform lightSource;
    
    [SerializeField] Animator playerAnim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stamina = maxStamina;
        playerAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0f && canRun && rb.velocity.magnitude > 0.1f)
        {
            isRunning = true;
            speed = 4.5f;
            stamina -= Time.deltaTime * staminaDecaySpeed;
            playerAnim.speed = 2f;
            if (stamina <= 0f)
                StartCoroutine(OutOfBreath());
        }
        else if (!outOfBreath)
        {
            isRunning = false;
            if (stamina <= maxStamina)
            {
                stamina += Time.deltaTime * staminaDecaySpeed;
                if (stamina >= 10f)
                    canRun = true;
            }
            else
                stamina = maxStamina;
            speed = 3f;
            playerAnim.speed = 1f;
        }
        staminaSlider.fillAmount = stamina / maxStamina;
        if(!isTransitioning)
            PlayerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        
        Aim();
    }

    private IEnumerator OutOfBreath()
    {
        canRun = false;
        outOfBreath = true;
        speed = 1f;
        playerAnim.speed = 0.5f;
        yield return new WaitForSeconds(1f);
        speed = 3f;
        playerAnim.speed = 1f;
        outOfBreath = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        
        Vector2 moveForce = PlayerInput * speed;
        moveForce += forceToApply;
        forceToApply /= forceDamping;
        if(Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }
        rb.velocity = moveForce;
        if (PlayerInput.x != 0 || PlayerInput.y != 0)
        {
            playerAnim.SetFloat("horizontal", PlayerInput.x);
            playerAnim.SetFloat("vertical", PlayerInput.y);
        }

        if (rb.velocity.magnitude > 0.1f)
            playerAnim.SetBool("isMoving", true);
        else
            playerAnim.SetBool("isMoving", false);
    }

    public void Stop()
    {
        isTransitioning = true;
        PlayerInput = Vector2.zero;
        rb.velocity = Vector2.zero;
        playerAnim.SetBool("isMoving", false);
        this.enabled = false;
    }
    void Aim()
    {
        // Get the mouse position in world space
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction vector from the object to the mouse position
        Vector3 aimDir = (targetPosition - lightSource.position).normalized;

        // Calculate the rotation to face the mouse position
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Apply the rotation to the object
        lightSource.rotation = lookRotation;

        // Optionally, set the aim direction and origin for your field of view logic
        // fieldOfView.SetAimDirection(aimDir);
        // fieldOfView.SetOrigin(transform.position);
        // fieldOfView1.SetAimDirection(aimDir);
        // fieldOfView1.SetOrigin(transform.position);
    }
    
    
}
