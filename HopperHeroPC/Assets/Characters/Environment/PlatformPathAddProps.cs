using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPathAddProps : MonoBehaviour
{
    [SerializeField] private GameObject[] trees;

    [SerializeField] private GameObject[] mushrooms;

    // Start is called before the first frame update
    void Start()
    {
        DropProps();
    }

    private void DropProps() {
        Jitter jitter = new Jitter();
        int nTrees = trees.Length;

        for (int i = 0; i < 5; i++) 
        {
            float x = Random.Range(2.0f, 5.0f);
            float y = 0.0f;
            float z = Random.Range(-5.0f, 5.0f);
            Vector3 position = new Vector3(x, y, z);

            int choice = Random.Range(0, nTrees);

            GameObject go = Instantiate(trees[choice], gameObject.transform, false);
            //go.transform.localPosition = position;

            jitter.Set(go).SetLocal(position).YRotateRandom(0.0f, 180.0f).ScaleRandom(25.0f);
        }

        for (int i = 0; i < 5; i++) 
        {
            float x = Random.Range(-5.0f, -2.0f);
            float y = 0.0f;
            float z = Random.Range(-5.0f, 5.0f);
            Vector3 position = new Vector3(x, y, z);

            int choice = Random.Range(0, nTrees);

            GameObject go = Instantiate(trees[choice], gameObject.transform, false);
            //go.transform.localPosition = position;

            jitter.Set(go).SetLocal(position).YRotateRandom(0.0f, 180.0f).ScaleRandom(25.0f);
        }

        int nMushRooms = mushrooms.Length;

        for (int i = 0; i < 3; i++) 
        {
            float x = Random.Range(2.0f, 5.0f);
            float y = 0.0f;
            float z = Random.Range(-5.0f, 5.0f);

            for (int j = 0; j < 10; j++) 
            {
                Vector2 place = Random.insideUnitCircle * 0.5f;
                Vector3 position = new Vector3(x + place.x, y, z + place.y);

                int choice = Random.Range(0, nMushRooms);

                GameObject go = Instantiate(mushrooms[choice], gameObject.transform, false);
                go.transform.localPosition = position;

                jitter.Set(go.transform).YRotateRandom(0.0f, 180.0f);
            }
        }

        for (int i = 0; i < 3; i++) 
        {
            float x = Random.Range(-5.0f, -2.0f);
            float y = 0.0f;
            float z = Random.Range(-5.0f, 5.0f);

            for (int j = 0; j < 10; j++) 
            {
                Vector2 place = Random.insideUnitCircle * 0.5f;
                Vector3 position = new Vector3(x + place.x, y, z + place.y);

                int choice = Random.Range(0, nMushRooms);

                GameObject go = Instantiate(mushrooms[choice], gameObject.transform, false);
                go.transform.localPosition = position;

                jitter.
                    Set(go.transform).
                    YRotateRandom(0.0f, 180.0f).
                    ScaleRandom(75.0f);
            }
        }

    }
}
