using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    public float lifespan = 15f;
    // Start is called before the first frame update
    void Start()
    {
         
    }


    // Update is called once per frame
    void Update()
    {
       
        UpdateLifespan();
    }

    void UpdateLifespan()
    {
        if (isActiveAndEnabled)
        {
            lifespan -= 0.02f;
            if(lifespan <= 0f)
            {
                Destroy(gameObject);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if (other.gameObject.name == "JoeyPrefab")
        {
            SceneManager.LoadScene(0);

        }else
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
