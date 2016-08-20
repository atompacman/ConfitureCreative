using UnityEngine;
using System.Collections;

public class followCam : MonoBehaviour {

    public GameObject target;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var targetPos = target.transform.position;
        targetPos += offset;
        gameObject.transform.position = targetPos;
        //Debug.Log(gameObject.transform.position);
    }
}
