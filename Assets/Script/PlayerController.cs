using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {

    public Boundary boundary;
    public float speed = 10.0f;
    public Vector3 tilt;
    public Vector3 externalVelocity = Vector3.zero;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = -Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, -1.0f);
        rb.velocity = speed * movement + externalVelocity;

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax),
            rb.position.z
        );

        rb.rotation = Quaternion.Euler(rb.velocity.y * + tilt.x, rb.velocity.x * -tilt.y, rb.velocity.x * -tilt.z);
    }
}
