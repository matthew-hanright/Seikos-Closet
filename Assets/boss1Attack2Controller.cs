using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1Attack2Controller : MonoBehaviour
{
    public GameObject[] spikes;
    public int spikesDone = 0;
    public bool spikesSet = false;
    public float startTime;
    private float delay = 0.7f;

    private void Update()
    {
        if(Time.time > startTime + delay && !spikesSet)
        {
            spikesSet = true;
            int i = 0;
            for (i = 0; i < spikes.Length; i++)
            {
                spikes[i].GetComponent<Animator>().SetBool("expandStart", true);
            }
        }
        if(spikesDone == spikes.Length)
        {
            FindObjectOfType<Boss1>().endAttack2();
        }
    }
    
}
