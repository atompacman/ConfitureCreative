using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NonUniformSoundSource : MonoBehaviour {

    public GameObject Player;

    public float VolumeFactor = 1;

    void Start()
    {
        if (Player == null)
        {
            Player = GameObject.Find("PaperPlane");
        }
    }

	void Update()
    {
        var delta = Player.transform.position.z - transform.position.z;

        // Volume curve is different if the player sees the object or not
        float distFactor = delta > 0 ? 0.001f : 0.1f;

        GetComponent<AudioSource>().volume = Mathf.Exp(-distFactor * delta * delta) * VolumeFactor;
    }
}
