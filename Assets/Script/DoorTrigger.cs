using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {

    public DoorPivot DoorPivot;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter()
    {
        DoorPivot.Open();
    }
}
