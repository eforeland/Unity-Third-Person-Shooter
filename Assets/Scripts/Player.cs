using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    public float gravity = -1.5f;
    private CharacterController charController;
    private float turnSpeed = 360f;
    public float yVelocity = 0.0f;
    public float jumpForce = 20.0f;
    private int jumpCount = 0;
    private int maxJumps = 2;
    private int health = 5;
    private int maxHealth = 5;
    private Animator anim;
    private float pushForce = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        gravity = jumpForce * -4.0f;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        //does it have a rigidbody and is physics enabled?
        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");
        //Vector3 rotation = new Vector3(0, mouseX, 0) * turnSpeed * Time.deltaTime;
        anim.SetFloat("mouseX", mouseX);
        //transform.Rotate(rotation);

        float deltaX = Input.GetAxis("Horizontal");
        float deltaZ = Input.GetAxis("Vertical");
        anim.SetFloat("xVel", deltaX);
        anim.SetFloat("zVel", deltaZ);
        Vector3 movement = new Vector3(deltaX, 0, deltaZ) * speed;
        movement = transform.TransformDirection(movement);


        if (charController.isGrounded)
        {
            yVelocity = 0f;
            jumpCount = 0;
            anim.ResetTrigger("jump");
        }

        if (jumpCount < maxJumps)
        {

            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = jumpForce;
                jumpCount += 1;
                if (jumpCount == 1)
                {
                    anim.SetTrigger("jump");
                } else if (jumpCount == 2)
                {
                    anim.SetTrigger("flip");
                }
                
            }
        }


        if (deltaX != 0 || deltaZ != 0 || yVelocity > 0.01)
        {
            anim.SetBool("isMoving", true);
            //Debug.Log("is moving set to true");
        }
        else
        {
            anim.SetBool("isMoving", false);
            //Debug.Log("is moving set to false");
        }

        yVelocity += gravity * Time.deltaTime;
        movement.y = yVelocity;
        movement *= Time.deltaTime;


        charController.Move(movement);

    }

    public void FirstAid(int healthAdded)
    {
        if (health < maxHealth)
        {
            health += healthAdded;
            Messenger<float>.Broadcast(GameEvent.HEALTH_CHANGED, health);
        } 
    }


    public void Hit()
    {
        health -= 1;
        //Debug.Log("Health: " + health);
        Messenger<float>.Broadcast(GameEvent.HEALTH_CHANGED, health);
        if (health == 0)
        {
            //Debug.Break();
            Messenger.Broadcast(GameEvent.PLAYER_DEAD);
        }
    }

}