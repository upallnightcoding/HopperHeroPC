using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPathAddProps : MonoBehaviour
{
    [SerializeField] private GameObject[] trees;

    [SerializeField] private GameObject[] mushrooms;

    [SerializeField] private GameObject[] props;

    [SerializeField] private GameObject[] buildings;

    [SerializeField] private GameObject[] grass;

    [SerializeField] private GameObject coinGold;

    [SerializeField] private int numberPatches;
    [SerializeField] private int sizeOfPatches;

    [SerializeField] private int nGrass;

    // Start is called before the first frame update
    void Start()
    {
        DropProps();
        CreateCoin();
    }

    private void CreateCoin() 
    {
        GameObject go = Instantiate(coinGold, gameObject.transform, false);
    }

    private void DropProps() 
    {
        Jitter jitter = new Jitter();

        DisperseGrass(2.0f, 5.0f);
        DisperseGrass(-5.0f, -2.0f);

        DisperseTrees(2.0f, 5.0f);
        DisperseTrees(-5.0f, -2.0f);

        DispersePatches(2.0f, 5.0f);
        DispersePatches(-5.0f, -2.0f);

        for (int i = 0; i < 3; i++) {
            float x = Random.Range(-5.0f, -2.0f);
            float y = 0.0f;
            float z = Random.Range(-5.0f, 5.0f);

            int choice = Random.Range(0, props.Length);

            GameObject go = Instantiate(props[choice], gameObject.transform, false);

            jitter.
                Set(go).
                SetLocal(new Vector3(x, y, z)).
                YRotateRandom(0.0f, 180.0f);
        }

        for (int i = 0; i < 3; i++) {
            float x = Random.Range(2.0f, 5.0f);
            float y = 0.0f;
            float z = Random.Range(-5.0f, 5.0f);

            int choice = Random.Range(0, props.Length);

            GameObject go = Instantiate(props[choice], gameObject.transform, false);

            jitter.
                Set(go).
                SetLocal(new Vector3(x, y, z)).
                YRotateRandom(0.0f, 180.0f);
        }

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

    private void DisperseTrees(float Xmin, float Xmax) 
    {
        Jitter jitter = new Jitter();

        for (int i = 0; i < 5; i++) 
        {
            int choice = Random.Range(0, trees.Length);

            GameObject go = Instantiate(trees[choice], gameObject.transform, false);

            jitter.
                Set(go).
                SetLocal(Xmin, Xmax, -5.0f, 5.0f).
                YRotateRandom(0.0f, 180.0f).
                ScaleRandom(25.0f);
        }
    }

    private void DisperseGrass(float Xmin, float Xmax) 
    {
        Jitter jitter = new Jitter();

        for (int i = 0; i < nGrass; i++) 
        {
            int choice = Random.Range(0, grass.Length);

            GameObject go = Instantiate(grass[choice], gameObject.transform, false);

            jitter.
                Set(go).
                SetLocal(Xmin, Xmax, -5.0f, 5.0f).
                YRotateRandom(0.0f, 180.0f).
                ScaleRandom(25.0f);
        }
    }
}
