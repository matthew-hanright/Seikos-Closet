using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBoxController : MonoBehaviour
{
    private Vector3 parentOriginalScale;
    private float xScaleFactor;
    private float yScaleFactor;
    // Start is called before the first frame update
    void Start()
    {
        parentOriginalScale = GetComponentInParent<Transform>().localScale;
        xScaleFactor = GetComponentInParent<Transform>().localScale.x / transform.localScale.x;
        yScaleFactor = GetComponentInParent<Transform>().localScale.y / transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponentInParent<Transform>().localScale != parentOriginalScale)
        {
            transform.localScale = new Vector3(
                GetComponentInParent<Transform>().localScale.x * xScaleFactor,
                GetComponentInParent<Transform>().localScale.y * yScaleFactor,
                transform.localScale.z);
        }
    }
}
