using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvent : MonoBehaviour
{
    public static event Action<GameEventCmd> OnScoreableEvent;

    public static void RaiseOnScoreableEvent(GameEventCmd command) 
    {
        OnScoreableEvent?.Invoke(command);
    }
}

public class GameEventCmd
{
    public GameEventType AType { get; set;}
    public GameObject AGameObject { get; set; }

    public GameEventCmd(GameEventType type, GameObject gameObject) {
        this.AType = type;
        this.AGameObject = gameObject;
    }
}

public enum GameEventType 
{
    DESTROY_SKELETON,
    COLLECT_GOLD_COIN,

    COIN_RED,
    COIN_WHITE,
    COIN_BLUE
}
