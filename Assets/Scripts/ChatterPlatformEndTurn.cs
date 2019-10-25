using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatterPlatformEndTurn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Solid")
        {
            if (!(collision.gameObject.layer == 11))
            {
                if (GetComponentInParent<ChatterAI>().movingLeft &&
                    (collision.gameObject.transform.position.x > transform.position.x))
                {
                    GetComponentInParent<ChatterAI>().turn(true);
                }
                else if ((collision.gameObject.transform.position.x < transform.position.x))
                {
                    GetComponentInParent<ChatterAI>().turn(false);
                }
            }
        }
    }
}
