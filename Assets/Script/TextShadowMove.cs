using UnityEngine;
using System.Collections;

public class TextShadowMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.localPosition = new Vector3(
            Mathf.Cos(Time.timeSinceLevelLoad * 2.0f) * 4.0f,
            Mathf.Sin(Time.timeSinceLevelLoad * 2.0f) * 4.0f,
            0.0f
            );
	}
}
