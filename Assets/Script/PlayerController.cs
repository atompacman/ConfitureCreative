using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour
{

    public Boundary positionBoundary;
    public float globalSpeed = 10.0f;
    public float gravitySpeed = -0.3f;
    public float inputVerticalSpeed = 0.0f;
    public float defaultPlaneSpeed = 1.0f;
    public float planeSpeedMin = 0.5f;
    public float planeSpeedMax = 2.0f;
    public Vector3 movementTilt = new Vector3(2.0f, 1.0f, 2.0f);
    public Vector3 externalVelocity = new Vector3(0.0f, 0.0f, 0.0f);
    public Material dry;
    public Material wet;

    public float wetLvl = 0.0f;

    private Rigidbody rb;
    private float planeSpeed;
    public Vector3 smoothedExternalVelocity = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        planeSpeed = defaultPlaneSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = -Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // UP
        if (moveVertical > 0)
        {
            moveVertical = 0.0f;
            //   planeSpeed -= inputVerticalSpeed;
        }

        // DOWN
        else if (moveVertical < 0)
        {
            planeSpeed += inputVerticalSpeed;
        }

        // NO VERTICAL INPUT
        else
        {
            planeSpeed = Mathf.Clamp(planeSpeed, planeSpeedMin, defaultPlaneSpeed);
        }

        planeSpeed = Mathf.Clamp(planeSpeed, planeSpeedMin, planeSpeedMax);

        Vector3 movement = new Vector3(moveHorizontal, moveVertical + gravitySpeed - wetLvl, -planeSpeed);

        smoothedExternalVelocity += (externalVelocity - smoothedExternalVelocity) * 2.0f * Time.deltaTime;
        rb.velocity = globalSpeed * movement + smoothedExternalVelocity;

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, positionBoundary.xMin, positionBoundary.xMax),
            Mathf.Clamp(rb.position.y, positionBoundary.yMin, positionBoundary.yMax),
            rb.position.z
        );

        rb.rotation = Quaternion.Euler(rb.velocity.y * movementTilt.x, rb.velocity.x * -movementTilt.y, rb.velocity.x * -movementTilt.z);
    }

    void Update()
    {
        if (wetLvl <= 0.0f)
        {
            wetLvl = 0.0f;
        }
        else
        {
            wetLvl -= Time.deltaTime / 30;
        }

        var model = transform.FindChild("Model");
        model.GetComponent<Renderer>().material.Lerp(dry, wet, wetLvl);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Contains("WaterDrop"))
        {
            if (wetLvl < 1.0f)
            {
                wetLvl += 0.2f;
            }
            else
            {
                wetLvl = 1.0f;
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.name.Contains("courantAirChaud"))
        {
            wetLvl -= 0.01f;
        }
        if (collider.gameObject.name.Contains("Puddle"))
        {
            wetLvl += 0.04f;
        }
    }
}