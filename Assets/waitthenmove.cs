using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitthenmove : MonoBehaviour
{
    public Transform newPos;
    public Transform toMove;
    public float timeToWait;
    public bool moved = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!moved)
        {
            timeToWait -= 0.02f;
            if (timeToWait <= 0f)
            {
                toMove.position = newPos.position;
                toMove.rotation = newPos.rotation;
                moved = true;
            }
        }
    }
}
