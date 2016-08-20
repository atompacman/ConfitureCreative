using UnityEngine;
using System.Collections;

public enum WindAxis { x, y, z }

public class courantAirChaud : MonoBehaviour {

    public GameObject avion;
    public Vector3 velocityVector;
    public float magnitude = 1.0f;
    public bool useVelocityVector = false;
    public WindAxis windAxis;

    private PlayerController playerController;
    private Vector3 objectAxis;
    private Vector3 actualVelocityVector;

	// Use this for initialization
	void Start () {
        Debug.Log("bob");
        playerController = avion.GetComponent<PlayerController>();

        if (useVelocityVector)
        {
            actualVelocityVector = velocityVector;
        }
        else
        {
            if (windAxis == WindAxis.x)
                objectAxis = gameObject.transform.right;
            else if (windAxis == WindAxis.y)
                objectAxis = gameObject.transform.up;
            else
                objectAxis = gameObject.transform.forward;

            actualVelocityVector = magnitude * objectAxis;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter ()
    {
        Debug.Log("enter");
        playerController.externalVelocity += actualVelocityVector;
    }

    void OnTriggerExit()
    {
        Debug.Log("leave");
        playerController.externalVelocity -= actualVelocityVector;
    }
}
