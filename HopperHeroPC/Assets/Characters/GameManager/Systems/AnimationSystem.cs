using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
    [SerializeField] private GameObject coinPuff;

    [SerializeField] private GameObject skeletonSkullPreFab;
    [SerializeField] private GameObject skeletonBonePreFab;
    [SerializeField] private GameObject skeletonExplosionPreFab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GameEventFunc(GameEventCmd command)
    {
        switch(command.AType) {
            case GameEventType.COLLECT_GOLD_COIN:
            case GameEventType.COIN_RED:
            case GameEventType.COIN_WHITE:
            case GameEventType.COIN_BLUE:
                CoinAnimation(command);
                break;
            case GameEventType.DESTROY_SKELETON:
                SkeletonAnimation(command);
                break;
        }
    }

    private void SkeletonAnimation(GameEventCmd command) 
    {
        Transform bonesTransform = command.AGameObject.transform;

        Instantiate(skeletonSkullPreFab, bonesTransform.position, bonesTransform.rotation);
        for (int i = 0; i < 10; i++) {
            Instantiate(skeletonBonePreFab, bonesTransform.position, bonesTransform.rotation);
        }
        Instantiate(skeletonExplosionPreFab, bonesTransform.position, bonesTransform.rotation);
        
        Destroy(command.AGameObject);
    }

    private void CoinAnimation(GameEventCmd command) 
    {
        Transform coinTransform = command.AGameObject.transform;
        GameObject puff = Instantiate(coinPuff, coinTransform.position, coinTransform.rotation);
        Destroy(puff, 1.0f);

        Destroy(command.AGameObject);
    }

    private void OnEnable() 
    {
        GameEvent.OnScoreableEvent += GameEventFunc;
    }

    private void OnDisable() 
    {
        GameEvent.OnScoreableEvent -= GameEventFunc;
    }
}
