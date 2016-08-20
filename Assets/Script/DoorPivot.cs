using UnityEngine;
using System.Collections;

public class DoorPivotScript : MonoBehaviour {
    
    [Range(0,100)]
    public float doorSpeed = 1.0f;

    bool animate = false;
    bool Opened = false;

    public void Open()
    {
        animate = true;
    }

	// Use this for initialization
	void Start () {
        gameObject.transform.rotation = Quaternion.identity;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            //animate = true;
        }
        if( animate && !Opened)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        if( gameObject.transform.rotation.eulerAngles.y < 90.0f || gameObject.transform.rotation.eulerAngles.y > 180.0f )
        {
            gameObject.transform.rotation = Quaternion.AngleAxis(Time.deltaTime * doorSpeed, new Vector3(0, 1, 0)) * gameObject.transform.rotation;
        }
        else if (gameObject.transform.rotation.y >= 90.0f )
        {
            Opened = false;
        }
    }
}
