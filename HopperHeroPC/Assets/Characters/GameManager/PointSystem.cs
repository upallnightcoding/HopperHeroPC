using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GameEventFunc(GameEventType gameEventTYpe)
    {
        switch(gameEventTYpe) {
            case GameEventType.COIN_GOLD:
                Debug.Log("PointSystem Event ...");
                break;
        }
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
