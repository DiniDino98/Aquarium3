using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGate : MonoBehaviour
{
    
    public MeshRenderer rend;

    public void show()
    {
        
        rend = GetComponent<MeshRenderer>();
        rend.enabled = true;
    }

    public void notshow()
    {
        
        rend = GetComponent<MeshRenderer>();
        rend.enabled = false;
    }
}
