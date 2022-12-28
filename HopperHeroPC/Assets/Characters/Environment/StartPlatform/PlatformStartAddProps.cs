using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStartAddProps : MonoBehaviour
{
    [SerializeField] private GameObject[] mushrooms;

    [SerializeField] private int numberPatches;
    [SerializeField] private int sizeOfPatches;

    // Start is called before the first frame update
    void Start()
    {
        DispersePatches(-5.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DispersePatches(float Xmin, float Xmax) 
    {
        Jitter jitter = new Jitter();

        for (int i = 0; i < numberPatches; i++) {

            for (int j = 0; j < sizeOfPatches; j++) {
                int choice = Random.Range(0, mushrooms.Length);

                GameObject go = Instantiate(mushrooms[choice], gameObject.transform, false);

                jitter.
                    PatchCenter(Xmin, Xmax, -5.0f, 5.0f).
                    Patch(sizeOfPatches, go, 0.5f).
                    YRotateRandom(0.0f, 180.0f).
                    ScaleRandom(75.0f);
            }

        }
    }

}
