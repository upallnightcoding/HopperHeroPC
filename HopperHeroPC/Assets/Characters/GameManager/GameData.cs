using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data")]
public class GameData : ScriptableObject
{
    [Header("Hero Attributes")]
    public float heroMovementSpeed;
}
