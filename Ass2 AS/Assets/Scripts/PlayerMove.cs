using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public float playerSpeed;
    public float playerRotation;

    public Transform rayPivot;

    public LayerMask groundLayer;

    private float groundDistance = 0.5f;
    private float playerDrag;
    private float rotationInput;
    private float speedInput;

    private Rigidbody playerRigidbody;

    private Vector3 playerCenterMass;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerDrag = playerRigidbody.drag;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is on the ground to get both speed and rotation inputs
        if (IsOnGround()) {
            rotationInput = Input.GetAxisRaw(InputAxes.Horizontal);
            speedInput = Input.GetAxis(InputAxes.Vertical);
        }

        // Rotate the car based on the players inputs
        transform.Rotate(0, speedInput * rotationInput * playerRotation * Time.deltaTime, 0);

        // Set the center of mass to a point which is on the ground to allow the player to not flip over
        playerRigidbody.centerOfMass = playerCenterMass;
    }

    void FixedUpdate()
    {
        // Check if the player is on the ground and apply force if they are
        if (IsOnGround())
        {
            playerRigidbody.AddForce(transform.forward * speedInput * playerSpeed);
            playerRigidbody.drag = playerDrag;
        }
        else
        {
            // The player is in the air so reduce the drag property of the player
            playerRigidbody.drag = 0.1f;
        }
    }

    // Check if the player is on the ground based of a raycast
    private bool IsOnGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayPivot.position, Vector3.down, out hit, groundDistance, groundLayer))
        {
            return true;

        }
        else
        {
            return false;
        }
    }
}
