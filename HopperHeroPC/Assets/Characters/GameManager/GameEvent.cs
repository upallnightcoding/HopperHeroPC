using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvent : MonoBehaviour
{
    public static event Action<GameEventType> OnScoreableEvent;

    public static void RaiseOnScoreableEvent(GameEventType gameEventType) 
    {
        OnScoreableEvent?.Invoke(gameEventType);
    }
}

public enum GameEventType 
{
    COIN_GOLD
}
