using UnityEngine;
using System.Collections;

public class courantAirChaud : MonoBehaviour {

    public GameObject avion;

    public Vector3 appliedVelocity;

    private PlayerController playerController;

	// Use this for initialization
	void Start () {
        Debug.Log("bob");
        playerController = avion.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter ()
    {
        Debug.Log("enter");
        playerController.externalVelocity += appliedVelocity;
    }

    void OnTriggerExit()
    {
        Debug.Log("leave");
        playerController.externalVelocity -= appliedVelocity;
    }
}
