using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatterTracking : MonoBehaviour
{
    public GameObject player;
    public GameObject chatter;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = chatter.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(chatter.GetComponent<ChatterAI>().seesPlayer)
        {
            if((player.transform.position.x - chatter.transform.position.x) < -2)
            {
                if (Mathf.Sign(chatter.transform.localScale.x) == -1)
                {
                    chatter.transform.localScale = new Vector3(
                    Mathf.Abs(chatter.transform.localScale.x),
                    chatter.transform.localScale.y,
                    chatter.transform.localScale.z);
                }
                if(!chatter.GetComponent<ChatterAI>().movingLeft)
                {
                    chatter.GetComponent<ChatterAI>().movingLeft = true;
                }
            }
            else if((player.transform.position.x - chatter.transform.position.x) > 2)
            {
                if (Mathf.Sign(chatter.transform.localScale.x) == 1)
                {
                    chatter.transform.localScale = new Vector3(
                    -Mathf.Abs(chatter.transform.localScale.x),
                    chatter.transform.localScale.y,
                    chatter.transform.localScale.z);
                }
                if (chatter.GetComponent<ChatterAI>().movingLeft)
                {
                    chatter.GetComponent<ChatterAI>().movingLeft = false;
                }
            }
        }

        transform.position = chatter.transform.position - offset;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            chatter.GetComponent<ChatterAI>().seesPlayer = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
