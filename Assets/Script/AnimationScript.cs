using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using System.IO;

public class AnimationScript : MonoBehaviour {

    const int lastFrame = 25;
    const int loopFrame = 16;
    const float frameInterval = 0.3f;

    List<Texture> frameImages = new List<Texture>();

    private int curFrame = 0;
    private float lastFrameTime;

    // Use this for initialization
	void Start () {
        string[] allFiles = Directory.GetFiles("Assets/Textures/Animation1");
        float time = 0.0f;
        foreach (var file in allFiles)
        {
            if (Path.GetExtension(file) == ".meta")
                continue;

            var obj = AssetDatabase.LoadAssetAtPath<Texture>(file);
            if (obj != null)
            {
                frameImages.Add(obj as Texture);
                time += 0.3f;
            }
        }

        //addFrame(0.0f, AssetDatabase.LoadAssetAtPath<Texture>("Assets/Textures/Animation1/f0.jpg"));
        //addFrame(1.0f, AssetDatabase.LoadAssetAtPath<Texture>("Assets/Textures/Animation1/f1.jpg"));

        GetComponent<RawImage>().texture = frameImages[curFrame];
        lastFrameTime = Time.timeSinceLevelLoad;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeSinceLevelLoad - lastFrameTime >= frameInterval)
        {
            curFrame++;
            if (curFrame > lastFrame)
            {
                curFrame = loopFrame;
            }

            // Set texture
            GetComponent<RawImage>().texture = frameImages[curFrame];

            lastFrameTime = Time.timeSinceLevelLoad;
        }
	}

    public void Reset()
    {
        curFrame = 0;
        lastFrameTime = Time.timeSinceLevelLoad;
        GetComponent<RawImage>().texture = frameImages[curFrame];
    }
}
