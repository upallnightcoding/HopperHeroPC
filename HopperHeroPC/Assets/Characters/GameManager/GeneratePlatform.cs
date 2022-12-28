using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlatform : MonoBehaviour
{
    private float PLATFORM_DISTANCE = 10.0f;

    [SerializeField] private GameObject hero;
    [SerializeField] private int nLookForeward;
    [SerializeField] private float nLookBackward;

    [Header("Platform PreFab Objects")]
    [SerializeField] private PlatformSO platformData;
    [SerializeField] private PlatformSO platformPathData;
    [SerializeField] private PlatformSO bridgeData;
    [SerializeField] private PlatformSO waterData;
    [SerializeField] private PlatformSO startPlatformData;

    [SerializeField] private GameObject skeletonPreFab;
    [SerializeField] private GameObject coinPreFab;

    [SerializeField] private GameObject[] cloudsPreFab;

    private PlatformType currentPlatform = PlatformType.START;

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
        PlatformSO platformData = GetPlatform(currentPlatform);

        GameObject platform = Instantiate(platformData.platform, position, Quaternion.identity);
        deleteQueue.Enqueue(platform);

        currentPlatform = GetNextPlatform(currentPlatform);
        
        //for (int i = 0; i < platformData.nVillians; i++) {
          //  GameObject skeleton = Instantiate(skeletonPreFab, platform.transform, false);
       // }

        //GameObject coin = Instantiate(coinPreFab, platform.transform, false);
        //coin.transform.localPosition = new Vector3(0.0f, 1.0f, 0.0f);
    }

    private PlatformSO GetPlatform(PlatformType type) {
        PlatformSO platform = null;

        switch(type) {
            case PlatformType.PLATFORM:
                platform = platformData;
                break;
            case PlatformType.BRIDGE:
                platform = bridgeData;
                break;
            case PlatformType.WATER:
                platform = waterData;
                break;
            case PlatformType.PATH:
                platform = platformPathData;
                break;
            case PlatformType.START:
                platform = startPlatformData;
                break;
        }

        return(platform);
    }

    private PlatformType GetNextPlatform(PlatformType type) {
        PlatformType choice = PlatformType.UNKNOWN;

        switch(type) {
            case PlatformType.PLATFORM:
                choice = Choose(PlatformType.BRIDGE, PlatformType.PATH, PlatformType.WATER);
                break;
            case PlatformType.BRIDGE:
                choice = PlatformType.PATH;
                break;
            case PlatformType.WATER:
                choice = Choose(PlatformType.WATER, PlatformType.PATH, PlatformType.PATH);
                break;
            case PlatformType.PATH:
                choice = Choose(PlatformType.BRIDGE, PlatformType.WATER, PlatformType.PATH, PlatformType.PLATFORM);
                break;
            case PlatformType.START:
                choice = PlatformType.PLATFORM;
                break;
        }

        return(choice);
    }

    private PlatformType Choose(params PlatformType[] types) {
        return(types[Random.Range(0, types.Length)]);
    }
}

public enum PlatformType 
{
    UNKNOWN,
    START,
    PLATFORM,
    BRIDGE,
    WATER,
    PATH
}
