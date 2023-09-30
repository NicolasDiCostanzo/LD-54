using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    private bool spinning;
    // Start is called before the first frame update
    void Start()
    {
        spinning = false;
    }

    // Update is called once per frame
    void Update()
    {
        // RectTransform tr = GetComponent<RectTransform>();
        // float rotation = Time.time; 
        // if(spinning) tr.Rotate(0,0,rotation);
        // else tr.localRotation.z = 0;
    }

    public void Switch(){
        spinning = !spinning;
    }
}
