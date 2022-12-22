using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCntrl : MonoBehaviour
{
    [SerializeField] private float degressPerSec;

    private Vector3 rotateAmount;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateAmount * Time.deltaTime);
    }

    private void Initialize() 
    {
        rotateAmount = new Vector3(0.0f, 0.0f, degressPerSec);

        Vector2 p = Random.insideUnitCircle * 5;

        gameObject.transform.localPosition = new Vector3(p.x, 1.0f, p.y);
    }
}
