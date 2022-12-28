using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreable : MonoBehaviour
{
    [SerializeField] private GameEventType gameEventType;

    public void OnTriggerEnter(Collider other) {
       GameEvent.RaiseOnScoreableEvent(new GameEventCmd(gameEventType, gameObject));
    }

    private void OnCollisionEnter(Collision other) {
        GameEvent.RaiseOnScoreableEvent(new GameEventCmd(gameEventType, gameObject));
    }
}
