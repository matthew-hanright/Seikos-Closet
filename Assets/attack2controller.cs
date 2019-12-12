using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack2controller : MonoBehaviour
{
    public GameObject[] spikes;
    public float startTime;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int spikesDone = 0;
        int i = 0;
        for(i = 0; i < spikes.Length; i++)
        {
            if(spikes[i].GetComponent<Animator>().GetBool("endAnimation") == true)
            {
                spikesDone++;
            }
        }
        if(spikesDone == spikes.Length)
        {
            FindObjectOfType<Boss1>().endAttack2();
        }
        else if(spikesDone == 0 && Time.time > startTime + delay)
        {
            for(i = 0; i < spikes.Length; i++)
            {
                spikes[i].GetComponent<Animator>().SetBool("expandStart", true);
            }
        }
    }

    public void OnEnable()
    {
        int i = 0;
        for(i = 0; i < spikes.Length; i++)
        {
            spikes[i].GetComponent<Animator>().SetBool("expandStart", false);
            spikes[i].GetComponent<Animator>().SetBool("endAnimation", false);
        }
    }

    public void OnDisable()
    {
        int i = 0;
        for (i = 0; i < spikes.Length; i++)
        {
            spikes[i].GetComponent<Animator>().SetBool("expandStart", false);
        }
    }
}
