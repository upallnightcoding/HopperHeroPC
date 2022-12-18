using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jitter
{
    private Transform transform;

    public Jitter Set(Transform transform) 
    {
        this.transform = transform;

        return(this);
    }

    public Jitter Set(GameObject go) {
        this.transform = go.transform;
        return(this);
    }

    public Jitter YRotateRandom(float min, float max) {
        float x = transform.rotation.x;
        float y = Random.Range(min, max);
        float z = transform.rotation.z;

        transform.rotation = Quaternion.Euler(x, y, z);

        return(this);
    }

    public Jitter SetLocal(Vector3 position) {
        transform.localPosition = position;
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
