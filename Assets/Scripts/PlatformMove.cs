using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public Vector2 startPoint;
    public Vector2 endPoint;
    public float speed; //Number of times to subdivide the distance, higher is slower
    public bool currentGoalPoint; //True for moving to start, false for moving to end
    private float intervalX, intervalY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(currentGoalPoint)
        {
            float xDiscrepancy = Mathf.Abs(transform.position.x - startPoint.x);
            float yDiscrepancy = Mathf.Abs(transform.position.y - startPoint.y);
            if (xDiscrepancy < 1.0f && yDiscrepancy < 1.0f)
            {
                currentGoalPoint = false;
                intervalX = (endPoint.x - startPoint.x) / speed;
                intervalY = (endPoint.y - startPoint.y) / speed;
            }
            else
            {
                intervalX = (startPoint.x - endPoint.x) / speed;
                intervalY = (startPoint.y - endPoint.y) / speed;
            }
        }
        else
        {
            float xDiscrepancy = Mathf.Abs(transform.position.x - endPoint.x);
            float yDiscrepancy = Mathf.Abs(transform.position.y - endPoint.y);
            if(xDiscrepancy < 1.0f && yDiscrepancy < 1.0f)
            {
                currentGoalPoint = true;
                intervalX = (startPoint.x - endPoint.x) / speed;
                intervalY = (startPoint.y - endPoint.y) / speed;
            }
            else
            {
                intervalX = (endPoint.x - startPoint.x) / speed;
                intervalY = (endPoint.y - startPoint.y) / speed;
            }
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(intervalX, intervalY);
        GetComponent<SurfaceEffector2D>().speed = intervalX;
    }

}
