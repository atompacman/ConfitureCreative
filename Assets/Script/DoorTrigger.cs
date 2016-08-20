using UnityEngine;
using System.Collections;

public class TriggerDoorScript : MonoBehaviour {

    public DoorPivotScript DoorPivot;

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
