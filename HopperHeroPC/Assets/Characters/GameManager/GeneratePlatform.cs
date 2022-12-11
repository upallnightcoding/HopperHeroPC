using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlatform : MonoBehaviour
{
    private float PLATFORM_DISTANCE = 10.0f;

    [SerializeField] private GameObject hero;
    [SerializeField] private int nLookForeward;
    [SerializeField] private float nLookBackward;

    [Header("PreFab Objects")]
    [SerializeField] private GameObject platformPreFab;
    [SerializeField] private GameObject skeletonPreFab;
    [SerializeField] private GameObject coinPreFab;

    [SerializeField] private GameObject[] cloudsPreFab;

    private float lastPlatformPos = 0.0f;
    private float lookForewardDis = 0.0f;
    private float lookBackwardDis = 0.0f;

    private Queue<GameObject> deleteQueue = null;

    void Awake() 
    {
        deleteQueue  = new Queue<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializePlatforms();
    }

    // Update is called once per frame
    void Update()
    {          
        if ((hero.transform.position.z + lookForewardDis) > (lastPlatformPos + PLATFORM_DISTANCE)) 
        {
            lastPlatformPos += PLATFORM_DISTANCE;

            Vector3 position = new Vector3(0.0f, 0.0f, lastPlatformPos);

            CreatePlateform(position);
        }

        if ((hero.transform.position.z - lookBackwardDis) > (deleteQueue.Peek().transform.position.z))
        {
            Destroy(deleteQueue.Dequeue());
        }
    }

    private void InitializePlatforms() 
    {
        Vector3 position = new Vector3(0.0f, 0.0f, lastPlatformPos);

        for (int i = 0; i < nLookForeward; i++) 
        {
            position.z = i * PLATFORM_DISTANCE;

            CreatePlateform(position);
        }

        lastPlatformPos = position.z;
        lookForewardDis = lastPlatformPos;
        lookBackwardDis = nLookBackward * PLATFORM_DISTANCE;
    }

    private void CreatePlateform(Vector3 position) 
    {
        GameObject platform = Instantiate(platformPreFab, position, Quaternion.identity);
        deleteQueue.Enqueue(platform);

        GameObject skeleton = Instantiate(skeletonPreFab, platform.transform, false);
        //skeleton.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        GameObject coin = Instantiate(coinPreFab, platform.transform, false);
        coin.transform.localPosition = new Vector3(0.0f, 1.0f, 0.0f);
    }
}
