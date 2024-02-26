using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface EvilJoeyState
{
    void Init(EvilJoeyAI eja);
    void OnSwap();
    void Update();
}

public class Taunt : EvilJoeyState
{
    EvilJoeyAI ejRef;
    float timer;
    float swapTime = 0f;
    public void Init(EvilJoeyAI eja)
    {
        ejRef = eja;
        timer = 0f;
        swapTime = 15f;
    }

    public void OnSwap()
    {
        ejRef.animatorRef.Play("Dancing");
        if (swapTime != 15) swapTime = Random.Range(1, 5);
        Debug.Log("taunt");
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if(timer >= swapTime)
        {
            ejRef.SwitchTo(2);
        }
    }
}

public class Move : EvilJoeyState
{
    EvilJoeyAI ejRef;

    public void Init(EvilJoeyAI eja)
    {
        ejRef = eja;
        
    }

    public void Update()
    {
        ejRef.WalkTowardTarget();
    }

    public void OnSwap()
    {
        ejRef.animatorRef.Play("Walk");
        Debug.Log("moving");
    }
}

public class Shoot : EvilJoeyState
{
    EvilJoeyAI ejRef;
    float countdown;
    int shotsFired;
    public void Init(EvilJoeyAI eja)
    {
        ejRef = eja;
    }

    public void OnSwap()
    {
        ejRef.animatorRef.Play("Attack");
        countdown = 1f;
        shotsFired = Random.Range(1, 5);
        Debug.Log("gun");
    }

    public void Update()
    {
        if(countdown <= 0f)
        {
            ejRef.Shoot();
            countdown = 1f;
            shotsFired--;
            if(shotsFired == 0)
            {
                ejRef.SwitchTo(0);
            }
        }
        countdown -= Time.deltaTime;
    }
}





public class EvilJoeyAI : MonoBehaviour
{
    public Animator animatorRef;
    public EvilJoeyState[] states = new EvilJoeyState[] { new Taunt(), new Move(), new Shoot() };
    EvilJoeyState currentState;
    public Transform[] targets = new Transform[3];
    Vector3[] positions = new Vector3[4];
    public Transform backWall;
    private int currentTarget = 4;
    private bool moving = false;
    private Vector3 incase;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < states.Length; states[i].Init(this), i++) ;
        currentState = states[0];
        currentState.OnSwap();
        positions = new Vector3[]{ targets[0].position, targets[1].position, targets[2].position, backWall.position};
    }

    // Update is called once per frame
    void Update()
    {
        
        if (moving)
        {
            transform.position = positions[currentTarget];
        }
        incase = transform.position;
        currentState.Update();
    }

    public void SwitchTo(int newState)
    {
        currentState = states[newState];
        currentState.OnSwap();
    }

    public void PickNewTarget()
    {
        int newTarget = Random.Range(0, 3);
        if(newTarget != currentTarget)
        {
            transform.LookAt(positions[newTarget]);
            moving = true;
            SwitchTo(1);
            currentTarget = newTarget;
        }
        else
        {
            SwitchTo(2);
        }
    }

    public void WalkTowardTarget()
    {
        float dist = Vector3.Distance(positions[currentTarget], incase);
        Debug.Log(dist);
        if (dist <= 0.5)
        {
            moving = false;
            transform.LookAt(positions[4]);
            SwitchTo(2);
        }
    }

    public void Shoot()
    {
        gameObject.GetComponent<FlameAttack>().shoot = true;
    }
}
