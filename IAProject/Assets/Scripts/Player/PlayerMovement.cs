using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Base movement
    public float turnSpeed = 20f;
    public float moveSpeed = 3f; // Speed at which the player moves

    public float runSpeed = 5f; // Sprint speed

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

    //Sound detection
    public SphereCollider m_SoundSphere;

    private float minSoundRadius = 4f;

    private float maxSoundRadius = 8f;

    private float actualRadius;

    private float soundSpeed = 10f; //Sound radius increases/decreases over time

    //Interaction
    private Transform holeTarget;
    public Transform gallinaTarget;

    private MeshRenderer m_MeshRenderer;

    private Vector3 lastPosition;

    //private bool canKill;

    public enum Status { //State machine
        Active, Interacting, Hidden, Leaving, Dead
    }

    public Status status;

    

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_MeshRenderer = GetComponent<MeshRenderer>();
        InitializeVariables();
    }

    void Update()
    {

        if(status == Status.Active){

            CalculateMovement();
            ActiveMovement();
        }

        else if(status == Status.Interacting){ //Jumps to the hole
            InteractingMovement();
        }

        else if(status == Status.Hidden){

            HiddenMovement();

        }

        else{
            LeavingMovement();
        }

        
    }

    void FixedUpdate()
    {
        if (status == Status.Active && m_Movement.magnitude > 0)
        {
            Vector3 targetPosition = m_Rigidbody.position + m_Movement * currentSpeed * Time.fixedDeltaTime;
            //m_Rigidbody.velocity = new Vector3(m_Movement.x, 0f, m_Movement.z);
            m_Rigidbody.MovePosition(targetPosition);

            Quaternion targetRotation = Quaternion.LookRotation(m_Movement);
            m_Rigidbody.MoveRotation(Quaternion.Slerp(m_Rigidbody.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));
        }
    }

    void CalculateMovement(){

        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical"); 

        //Movement Vector
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        //Jump
        if(!grounded) grounded = CheckGrounded();
        else if (grounded && canJump && Input.GetButtonDown("Jump")) Jump();

        //Run and sound detection
        if(Input.GetKey(KeyCode.LeftShift)){

            currentSpeed = runSpeed;
            if(actualRadius < maxSoundRadius) SetSoundRadius(actualRadius + soundSpeed * Time.deltaTime);

        } 

        else{
            currentSpeed = moveSpeed;
            if(actualRadius > minSoundRadius) SetSoundRadius(actualRadius - soundSpeed * Time.deltaTime);
        }

        

    }

    void ActiveMovement(){
        if(holeTarget!= null){
            float d = Vector3.Distance(transform.position,holeTarget.position);
            if(d < 5f && grounded && Input.GetKey(KeyCode.E) && holeTarget != null){ //Jumps to the hole
                holeTarget.GetChild(0).GetComponent<BoxCollider>().enabled = false;
                status = Status.Interacting;
                float jumpBoost = 1.5f;
                lastPosition = transform.position;
                m_Rigidbody.AddForce(Vector3.up * jumpForce* jumpBoost, ForceMode.Impulse);
                
            }
            
        }

        if(gallinaTarget!=null){
            float d = Vector3.Distance(transform.position, gallinaTarget.position);
            if(d < 3 && grounded && Input.GetButtonDown("Fire1")/* && canKill*/){
                //canKill = false;
                Debug.Log("Se muere");
                Vector3 punchDirection = gallinaTarget.position - transform.position;
                gallinaTarget.GetComponent<Rigidbody>().AddForce(punchDirection * 3f * 2f, ForceMode.Impulse);
                Destroy(gallinaTarget.gameObject, 2f);
                GameManager.Instance.liveChickens -= 1;
                GameManager.Instance.statCheck(); //Checks if all chickens are dead
                
            }
        }
        
    }

    void InteractingMovement(){ //Jumping to the hole (move plus rotation)


        holeTarget.position = new Vector3(holeTarget.position.x, holeTarget.position.y, holeTarget.position.z);
        Vector3 targetDirection = holeTarget.position - transform.position;
        float singleStep = 5f * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        transform.position = Vector3.MoveTowards(transform.position, holeTarget.position, 6f * Time.deltaTime);
        float d = Vector3.Distance(transform.position, holeTarget.position);
        if(d < 0.1f){
            status = Status.Hidden;
            m_MeshRenderer.enabled = false;
            //transform.GetChild(0).gameObject.SetActive(false);
                
        }
    }

    void HiddenMovement(){ //Hidden movement (zero)

        if(Input.GetKey(KeyCode.E)){ //Exits from the hole
            
            status = Status.Leaving;
            m_MeshRenderer.enabled=true;
            //transform.GetChild(0).gameObject.SetActive(true);
            //transform.forward = holeTarget.forward;
            Vector3 newDir = lastPosition - transform.position;
            transform.forward = newDir;
            float jumpBoost = 1.5f;
            m_Rigidbody.AddForce(Vector3.up * jumpForce* jumpBoost, ForceMode.Impulse);
        }
    }

    void LeavingMovement(){ //Leaving the hole

        transform.position = Vector3.MoveTowards(transform.position, lastPosition, 4f * Time.deltaTime);
        float d = Vector3.Distance(transform.position, lastPosition);
        if(d < 0.1f){
            //holeTarget = null;
            lastPosition = Vector3.zero;
            status = Status.Active;
            holeTarget.GetChild(0).GetComponent<BoxCollider>().enabled = true;
            
        }
    }

    void Jump(){
        grounded = false;
        canJump = false;
        m_Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        StartCoroutine(JumpCooldown(jumpCooldownTime));

    }

    IEnumerator JumpCooldown(float c){ //This fixes double jump bug after jumping first frame

        yield return new WaitForSeconds(c);
        canJump = true;

    }

    bool CheckGrounded(){ //Raycast downwards the player

        RaycastHit hit;
        m_RaycastOrigin = transform.position;
        m_RaycastOrigin.y += m_RaycastOffset;
        return Physics.SphereCast(m_RaycastOrigin, 0.5f, Vector3.down, out hit, 1f, groundLayer);
    }

    void InitializeVariables(){

        grounded = true;
        canJump = true;
        //canKill = true;
        m_SoundSphere.radius = minSoundRadius;
        actualRadius = minSoundRadius;
        status = Status.Active; //Starts active
    }

    void SetSoundRadius(float r){
        m_SoundSphere.radius = r;
        actualRadius = r;
    }

    private void OnTriggerEnter(Collider other){

        if(other.CompareTag("Hole")) holeTarget = other.gameObject.transform;

        if(other.CompareTag("Gallina")) gallinaTarget = other.gameObject.transform;

    }

    private void OnTriggerExit(Collider other){

        if(status == Status.Active && other.CompareTag("Hole")) holeTarget = null;
        if(other.CompareTag("Gallina")) gallinaTarget = null;


    }
}
