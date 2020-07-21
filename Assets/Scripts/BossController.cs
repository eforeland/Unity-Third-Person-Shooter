using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BossController : MonoBehaviour
{
    private GameObject player;
    //NavMeshAgent agent;
    private Animator anim;
    private bool pause = true;
   
        
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        //agent = this.GetComponent<NavMeshAgent>();
         StartCoroutine(Pause(10.5f));

    }

    //  Update is called once per frame
    void Update()
    {
        
        if(player != null)
        {
            var distance = Vector3.Distance(transform.position, player.transform.position);

            //if (!pause && agent != null)
            //{
            //  //agent.SetDestination(player.transform.position);
            //    if (distance == 3)
            //    {
            //        anim.SetTrigger("Attack");
            //    }
            //}
        }   
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            Player body = other.gameObject.GetComponent<Player>();
            body.Hit();
            StartCoroutine(Pause(3.0f));
        }
    }

    IEnumerator Pause(float time)
    {
        Debug.Log("Pause called");
        //pause = true;
        yield return new WaitForSeconds(time);
        pause = false;
    }
}
