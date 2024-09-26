using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Base movement
    public float turnSpeed = 20f;
    public float moveSpeed = 5f; // Speed at which the player moves

    public float runSpeed = 7f; // Sprint speed

    private float currentSpeed; //Final speed (normal or sprint)
    private Rigidbody m_Rigidbody;
    private Vector3 m_Movement;


    //Ground detection
    private float m_RaycastOffset = 0.5f;

    private Vector3 m_RaycastOrigin; 
    public LayerMask groundLayer;
    private bool grounded;

    //Jump
    private bool canJump; //Fixes double jump bug

    public float jumpForce = 5f;

    private float jumpCooldownTime = 0.5f;

    void Start()
    {
        InitializeVariables();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
    
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical"); 

        //Movement Vector
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        //Jump
        if(!grounded) grounded = CheckGrounded();
        else if (grounded && canJump && Input.GetButtonDown("Jump")) Jump();

        //Run
        currentSpeed = Input.GetKey(KeyCode.LeftShift)? runSpeed : moveSpeed;
            
    }

    void FixedUpdate()
    {
        if (m_Movement.magnitude > 0)
        {
            // Move the player using the movement vector multiplied by speed
            Vector3 targetPosition = m_Rigidbody.position + m_Movement * currentSpeed * Time.fixedDeltaTime;
            //m_Rigidbody.velocity = new Vector3(m_Movement.x, 0f, m_Movement.z);
            m_Rigidbody.MovePosition(targetPosition);

            // Calculate the desired forward direction and rotation
            Quaternion targetRotation = Quaternion.LookRotation(m_Movement);
            m_Rigidbody.MoveRotation(Quaternion.Slerp(m_Rigidbody.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));
        }
    }

    void Jump(){
        grounded = false;
        canJump = false;
        m_Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        StartCoroutine(JumpCooldown(jumpCooldownTime));

    }

    IEnumerator JumpCooldown(float c){

        yield return new WaitForSeconds(c);
        canJump = true;

    }

    bool CheckGrounded(){

        RaycastHit hit;
        m_RaycastOrigin = transform.position;
        m_RaycastOrigin.y += m_RaycastOffset;
        return Physics.SphereCast(m_RaycastOrigin, 0.5f, Vector3.down, out hit, 1f, groundLayer);
    }

    void InitializeVariables(){

        grounded = true;
        canJump = true;
    }
}
