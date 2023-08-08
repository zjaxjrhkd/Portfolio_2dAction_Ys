using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsgoreDumy2 : MonoBehaviour
{
    public AsgoreSlash body;
    public AsgoreSlash slash;

    public float time;

    private float phaseTime;

    private float nextPhaseTime;

    void Start()
    {
        body = body.GetComponent<AsgoreSlash>();
        slash = slash.GetComponent<AsgoreSlash>();
        time = 0;
        phaseTime = 0;
        nextPhaseTime = 20;
    }

    // Update is called once per frame
    void Update()
    {
        phaseTime += Time.deltaTime;

        time += Time.deltaTime;
        if (time >0.6)
        {
            Move();
            AttackOn();
            time = 0;
        }
    }

    private void Move()
    {
        int random = Random.Range(0, 5);
        if(random == 0)
        {
            transform.position = new Vector3(-5.0f, -0.6f, 0.0f);
        }
        else if (random == 1)
        {
            transform.position = new Vector3(0.0f, -0.6f, 0.0f);
        }
        else if (random == 2)
        {
            transform.position = new Vector3(5.0f, -0.6f, 0.0f);

        }
    }
    private void AttackOn()
    {
        body.SlashOn();
        slash.SlashOn();
    }

}
