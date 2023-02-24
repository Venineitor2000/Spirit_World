using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueBasico : MonoBehaviour
{
    int range = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetRange()
    {
        return range;
    }

    public bool GetIsAvaliable()
    {

        return false;
    }

    public virtual void Execute()
    {

    }
}
