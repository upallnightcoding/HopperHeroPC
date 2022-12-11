using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data")]
public class GameData : ScriptableObject
{
    [Header("Points System")]
    public int totalPoints;

    public void initialize() {
        totalPoints = 0;
    }
}
