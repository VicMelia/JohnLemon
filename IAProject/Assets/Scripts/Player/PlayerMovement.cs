using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public float moveSpeed = 5f; // Speed at which the player moves

    private Rigidbody m_Rigidbody;
    private Vector3 m_Movement;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get movement input from W, A, S, D keys
        float horizontal = Input.GetAxis("Horizontal"); // A and D
        float vertical = Input.GetAxis("Vertical"); // W and S

        // Create the movement vector
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();
    }

    void FixedUpdate()
    {
        if (m_Movement.magnitude > 0)
        {
            // Move the player using the movement vector multiplied by speed
            Vector3 targetPosition = m_Rigidbody.position + m_Movement * moveSpeed * Time.fixedDeltaTime;
            m_Rigidbody.MovePosition(targetPosition);

            // Calculate the desired forward direction and rotation
            Quaternion targetRotation = Quaternion.LookRotation(m_Movement);
            m_Rigidbody.MoveRotation(Quaternion.Slerp(m_Rigidbody.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));
        }
    }
}
