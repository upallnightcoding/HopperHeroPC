using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeAddProps : MonoBehaviour
{
    [SerializeField] private GameObject[] props;

    // Start is called before the first frame update
    void Start()
    {
        DisperseProps(2.0f, 5.0f);
        DisperseProps(-5.0f, -2.0f);

        DispersePatches(2.0f, 5.0f);
        DispersePatches(-5.0f, -2.0f);
    }

    private void DisperseProps(float Xmin, float Xmax) 
    {
        Jitter jitter = new Jitter();

        for (int i = 0; i < 15; i++) 
        {
            int choice = Random.Range(0, props.Length);

            GameObject go = Instantiate(props[choice], gameObject.transform, false);

            jitter.
                Set(go).
                SetLocal(Xmin, Xmax, -5.0f, 5.0f).
                YRotateRandom(0.0f, 180.0f).
                ScaleRandom(25.0f);
        }
    }

    private void DispersePatches(float Xmin, float Xmax) 
    {
        Jitter jitter = new Jitter();

        for (int i = 0; i < 3; i++) {

            for (int j = 0; j < 5; j++) {
                int choice = Random.Range(0, props.Length);

                GameObject go = Instantiate(props[choice], gameObject.transform, false);

                jitter.
                    PatchCenter(Xmin, Xmax, -5.0f, 5.0f).
                    Patch(5, go, 0.5f).
                    YRotateRandom(0.0f, 180.0f).
                    ScaleRandom(75.0f);
            }

        }
    }
}
