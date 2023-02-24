using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    AI[] AIs;
    // Start is called before the first frame update
    void Start()
    {
        AIs = GameObject.FindObjectsOfType<AI>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (AI ai in AIs)
        {
            if (Input.GetKeyDown(KeyCode.V))
                ai.TakeDamage(0);
            else if (Input.GetKeyDown(KeyCode.B))
                ai.TakeDamage(100000);
            else if (Input.GetKeyDown(KeyCode.C))
                ai.SetCourageLevel(10000);
        }
        
    }
}
