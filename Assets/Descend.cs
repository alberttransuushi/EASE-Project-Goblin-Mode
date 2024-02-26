using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Descend : MonoBehaviour
{
    Vector3 start;
    public Vector3 finish;
    public float dist = 0f;
    public float toTravel;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        toTravel = start.y - finish.y;
    }

    // Update is called once per frame
    void Update()
    {
        dist += 0.002f;
        
        transform.position = Vector3.Lerp(start, finish, dist);
    }
}
