using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAttack : MonoBehaviour
{
    public GameObject atkPrefab;
    public bool shoot = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("fr?");
    }

    // Update is called once per frame
    void Update()
    {
        if (shoot)
        {
            GameObject g = Instantiate(atkPrefab, transform.position + (transform.forward * 0.4f) + new Vector3(0, 0.8f, 0f), (this.transform.rotation));
            g.GetComponent<Rigidbody>().AddForce(this.transform.forward * 5f, ForceMode.Impulse);
            shoot = false;
        }
    }
}
