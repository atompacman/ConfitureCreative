using UnityEngine;
using System.Collections;

public enum WindAxis { x, y, z }

public class courantAirChaud : MonoBehaviour {

    public GameObject avion;
    public float magnitude = 1.0f;
    public WindAxis windAxis;
    public GameObject windOrigin;

    private PlayerController playerController;
    private Vector3 objectAxis;
    private Vector3 actualVelocityVector;

	// Use this for initialization
	void Start () {
        playerController = avion.GetComponent<PlayerController>();

        if (windAxis == WindAxis.x)
            objectAxis = gameObject.transform.right;
        else if (windAxis == WindAxis.y)
            objectAxis = gameObject.transform.up;
        else
            objectAxis = gameObject.transform.forward;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name != "PaperPlane")
        {
            return;
        }

        Vector3 originToPlane = playerController.transform.position - windOrigin.transform.position;
        actualVelocityVector = (40.0f + magnitude * 20.0f) / Vector3.Project(originToPlane, objectAxis).magnitude * objectAxis;
        playerController.externalVelocity += actualVelocityVector;
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name != "PaperPlane")
        {
            return;
        }
        playerController.externalVelocity -= actualVelocityVector;
    }
}
