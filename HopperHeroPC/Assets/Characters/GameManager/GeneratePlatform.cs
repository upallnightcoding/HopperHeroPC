using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlatform : MonoBehaviour
{
    [SerializeField] private GameObject hero;
    [SerializeField] private float lookForeward;
    [SerializeField] private float lookBackward;

    [SerializeField] private GameObject platformPreFab;
    [SerializeField] private GameObject skeletonPreFab;
    [SerializeField] private int numberStartPlatforms;

    private float lastPlatformPosition = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Generate Platform ...");
        InitializePlatforms();
    }

    // Update is called once per frame
    void Update()
    {          
        
    }

    private void InitializePlatforms() 
    {
        Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);

        for (int plateform = 0; plateform < numberStartPlatforms; plateform++) 
        {
            GameObject platform = Instantiate(platformPreFab, position, Quaternion.identity);

            GameObject skeleton = Instantiate(skeletonPreFab, platform.transform, false);
            skeleton.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            
            position += new Vector3(0.0f, 0.0f, 10.0f);
        }
    }
}
