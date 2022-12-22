using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jitter
{
    private Transform transform;

    private Vector3 position;

    private int patchCount = 0;

    /************************/
    /** Setting Functions ***/
    /************************/

    public Jitter Set(Transform transform) 
    {
        this.transform = transform;
        return(this);
    }

    public Jitter Set(GameObject go) {
        this.transform = go.transform;
        return(this);
    }

     public Jitter SetLocal(float Xmin, float Xmax, float Zmin, float Zmax) {
        float x = Random.Range(Xmin, Xmax);
        float y = 0.0f;
        float z = Random.Range(Zmin, Zmax);

        transform.localPosition = new Vector3(x, y, z);

        return(this);
    }

    public Jitter SetLocal(Vector3 position) {

        transform.localPosition = position;

        return(this);
    }

    public Jitter YRotateRandom(float min, float max) {
        float x = transform.rotation.x;
        float y = Random.Range(min, max);
        float z = transform.rotation.z;

        transform.rotation = Quaternion.Euler(x, y, z);

        return(this);
    }

    public Jitter Patch(int patchSize, GameObject go, float radius) {

        if (patchCount++ <= patchSize) {
            Vector2 place = Random.insideUnitCircle * radius;
            Vector3 localPosition = new Vector3(position.x + place.x, 0.0f, position.z + place.y);
            this.transform = go.transform;
            SetLocal(localPosition);
        }

        if (patchCount >= patchSize) {
            patchCount = 0;
        }

        return(this);
    }

    public Jitter PatchCenter(float Xmin, float Xmax, float Zmin, float Zmax) {
        float x = Random.Range(Xmin, Xmax);
        float y = 0.0f;
        float z = Random.Range(Zmin, Zmax);

        if (patchCount == 0) {
            position = new Vector3(x, y, z);
        }

        return(this);
    }

    public Jitter ScaleRandom(float percent) {

        transform.localScale = 
            transform.localScale + 
            transform.localScale * 
            (Random.Range(0.0f, percent) / 100.0f) * 
            flip();

        return(this);
    }

    private float flip() {
        return(Random.Range(0, 2) == 0 ? -1.0f : 1.0f);
    }
}
