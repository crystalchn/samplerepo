using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem ; 

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody rb ; // private to distinguish which object can access this (like limiting
    // the velocity to player)
    private bool isGrounded = false;

    [SerializeField] private float speed = 5f; // so you can still edit it in unity, good for testing
    [SerializeField] private float jumpHeight = 5f ; 

    private Vector2 direction = Vector2.zero ; 

    void Start ()
    {
        rb = GetComponent<Rigidbody>() ; // retrieves the component that u assigned on unity 
    }


    void OnMove(InputValue value) 
    {
        Vector2 direction = value.Get<Vector2>() ;  // takes input value, stores as 2D vector
        Debug.Log(direction) ; 
        this.direction = direction ; // direction of object is saved 
    }

    void Update () 
    {
        Move(direction.x, direction.y) ;  // updates every frame, even if you're not moving 

    }
    
    private void Move(float x, float z) 
    {
        rb.velocity = new Vector3(x * speed, rb.velocity.y, z * speed) ; // assigning velocity to the rigid body, 
        // updates everytime move function is called, moving in xz plane, jump in y 
    }

    void OnJump() // if you are on the ground then jump
    {
        if (isGrounded) // executes code or not 
        {
            Jump() ; // to register OnJump function
        }
            
    }

    private void Jump() 
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z) ; // dot is accessing a property
        // of the object, x and z stay, but changes for y
    }

    void OnCollisionExit(Collision collision) 
    {
        isGrounded = false ; // when you leave a collision this updates
    }

    void OnCollisionStay(Collision collision) // (data type, name of variable), can be the same or diff
    { 
        if (Vector3.Angle(collision.GetContact(0).normal, Vector3.up) < 45f) 
        // checking if angle difference is less than 45 degrees 
        {
            isGrounded = true ; // on the ground
        }
        else 
        {
            isGrounded = false ; // not on the ground
        }
    }
}
