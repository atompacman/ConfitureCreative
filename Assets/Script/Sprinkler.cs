using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Sprinkler : MonoBehaviour
{

    [Range(0, 100)]
    public int agressivity = 30;
    [Range(0, 200)]
    public int scale = 100;
    [Range(0, 100)]
    public float strenght = 35f;

    public float yOffset = 0.5f;

    public GameObject projectile;

    System.Random pseudo;
    Dictionary<float, GameObject> balls;
    float lifeTime = 2.0f;

    public float MinActiationDistance = 15;

    // Use this for initialization
    void Start()
    {
        pseudo = new System.Random(Time.time.GetHashCode());
        balls = new Dictionary<float, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(GameObject.Find("PaperPlane").transform.position, transform.position) > MinActiationDistance)
        {
            return;
        }

        if (pseudo.Next(50) < pseudo.Next(agressivity))
        {
            Throw();
        }

        List<float> keyToRemove = new List<float>();
        foreach (KeyValuePair<float, GameObject> ballItem in balls)
        {
            if (Time.time > ballItem.Key + lifeTime)
            {
                Destroy(ballItem.Value);
                keyToRemove.Add(ballItem.Key);
            }
        }
        foreach (float key in keyToRemove)
        {
            balls.Remove(key);
        }
    }

    void Throw()
    {
        var ball = Instantiate(projectile);
        ball.transform.parent = this.transform;

        var startPos = gameObject.transform.position + new Vector3(0,yOffset,0);
        ball.transform.position = startPos;

        var delX = pseudo.Next(100) / 100.0f - 0.5f;
        var delZ = pseudo.Next(100) / 100.0f - 0.5f;
        var deviation = new Vector3(delX, 0.0f, delZ);

        var target = deviation;
        target.Normalize();

        ball.GetComponent<Rigidbody>().AddForce(target * (strenght / 10.0f), ForceMode.VelocityChange);

        var scaleMod = scale / 100.0f;
        ball.transform.localScale = new Vector3(scaleMod, scaleMod, scaleMod);

        balls.Add(Time.time, ball);
    }
}
