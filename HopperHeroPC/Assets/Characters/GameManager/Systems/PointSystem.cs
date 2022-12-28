using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    [SerializeField] UISystem uiSystem;

    private int totalPoints = 0;

    private void GameEventFunc(GameEventCmd command)
    {
        switch(command.AType) {
            case GameEventType.COLLECT_GOLD_COIN:
                updatePoints(1);
                break;
            case GameEventType.COIN_RED:
            case GameEventType.COIN_WHITE:
            case GameEventType.COIN_BLUE:
                break;
            case GameEventType.DESTROY_SKELETON:
                updatePoints(10);
                break;
        }
    }

    private void updatePoints(int points) 
    {
        totalPoints += points;

        uiSystem.display(totalPoints);
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
