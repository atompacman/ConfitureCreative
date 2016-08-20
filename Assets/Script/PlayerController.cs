using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour
{
    private const int WALLS_LAYER = 8;

    public Boundary positionBoundary;
    public float globalSpeed = 10.0f;
    public float gravitySpeed = -0.3f;
    public float inputVerticalSpeed = 0.0f;
    public float defaultPlaneSpeed = 1.0f;
    public float planeSpeedMin = 0.5f;
    public float planeSpeedMax = 2.0f;
    // 0 : raw, 1 : smooth
    public float horizontalMoveSmoothing = 0.05f;
    public Vector3 movementTilt = new Vector3(2.0f, 1.0f, 2.0f);
    public Vector3 externalVelocity = new Vector3(0.0f, 0.0f, 0.0f);
    public Material dry;
    public Material wet;
    public GameObject gameStateObject;
    public Vector3 smoothedExternalVelocity = Vector3.zero;

    public float wetLvl = 0.0f;

    private Rigidbody rb;
    private float planeSpeed;
    private GameState gameState;
    private Vector3 initPos;

    private float prevMoveHorizontal;
    private float prevHorizontalRotation;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        planeSpeed = defaultPlaneSpeed;
        gameState = gameStateObject.GetComponent<GameState>();
        initPos = rb.position;
    }

    public void Reset()
    {
        gameObject.transform.position = initPos;
        wetLvl = 0.0f;
        
        rb.position = initPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // DON'T MOVE IF NOT IN "FLYING" GAME STATE
        if (gameState.currentState != GameStateEnum.Flying)
            return;

        float moveHorizontal = -Input.GetAxis("Horizontal");
        moveHorizontal = Mathf.Lerp(moveHorizontal, prevMoveHorizontal, horizontalMoveSmoothing);
        prevMoveHorizontal = moveHorizontal;

        float moveVertical = Input.GetAxis("Vertical");

        // UP
        if (moveVertical > 0)
        {
            moveVertical = 0.0f;
            //planeSpeed -= inputVerticalSpeed;
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

        if (rb.position.y < 0.5f)
        {
            gameState.GameOver();
        }

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, positionBoundary.xMin, positionBoundary.xMax),
            Mathf.Clamp(rb.position.y, positionBoundary.yMin, positionBoundary.yMax),
            rb.position.z
        );
        Debug.Log(Mathf.Clamp(rb.velocity.y * movementTilt.x, -26, float.PositiveInfinity));
        rb.rotation = Quaternion.Euler(Mathf.Clamp(rb.velocity.y * movementTilt.x, -26, float.PositiveInfinity), rb.velocity.x * -movementTilt.y, rb.velocity.x * -movementTilt.z);
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

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.name.Contains("courantAirChaud"))
        {
            wetLvl -= 0.01f;
        }
        if (collider.gameObject.name.Contains("waterFall"))
        {
            wetLvl += 0.015f;
        }
        if (collider.gameObject.name.Contains("Puddle"))
        {
            wetLvl += 0.04f;
        }
    }
}