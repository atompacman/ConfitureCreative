using UnityEngine;

public class Piping : MonoBehaviour
{
    public GameObject MainPipeSection;
    public GameObject SecondaryPipeSection;
    public GameObject Sprinkler;

    public float MainPipeLength;
    public float MainPipeSectionSize;
    public uint MinDistBetweenSecondaryPipes;
    public float SecondaryPipeProbability;
    public float SecondaryPipeSize;

	private void Start()
    {
        int i = 0;
        for (float currPosZ = 0; currPosZ < MainPipeLength; currPosZ += MainPipeSectionSize, ++i)
        {
            CreatePipeObject(MainPipeSection, currPosZ);

            if (i % MinDistBetweenSecondaryPipes == 0 && Random.value < SecondaryPipeProbability)
            {
                var obj = CreatePipeObject(SecondaryPipeSection, currPosZ);
                obj.transform.localScale = new Vector3(1, 1, SecondaryPipeSize);
                
                obj = CreatePipeObject(Sprinkler, currPosZ);
                var pos = obj.transform.position;
                pos.x = Random.value * SecondaryPipeSize - SecondaryPipeSize / 2;
                obj.transform.position = pos;
            }
        }
	}

    private GameObject CreatePipeObject(GameObject prefab, float posZ)
    {
        var obj = Instantiate(prefab);
        var pos = transform.position;
        pos.z = posZ - MainPipeLength / 2;
        obj.transform.position = pos;
        obj.transform.parent = transform;
        return obj;
    }
}
