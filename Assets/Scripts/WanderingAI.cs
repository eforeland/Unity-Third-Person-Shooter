using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { wander, chase, dead };

public class WanderingAI : MonoBehaviour
{
    private EnemyStates state;

    [SerializeField] private GameObject enemyBulletPrefab;
    private GameObject enemyBullet;
    private NavMeshAgent agent;
    private Animator anim;
    private GameObject player;
    Rigidbody rigidbody;

    private float fireRate = 2.0f;
    private float nextFire = 0.0f;
    private float enemySpeed = 8.0f;
    private float obstacleRange = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        state = EnemyStates.wander;
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        agent = this.GetComponent<NavMeshAgent>();
        rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < 7.0f)
        {
            state = EnemyStates.chase;
        } else
        {
            state = EnemyStates.wander;
        }

        Ray ray = new Ray(transform.position, transform.forward);
        // spherecst and determine if enemy needs to turn
        RaycastHit hit;

        if (state == EnemyStates.wander)
        {
            // Move enemy and generate Ray
            transform.Translate(0, 0, enemySpeed * Time.deltaTime);

            if (Physics.SphereCast(ray, 1.0f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<Player>())
                {
                    //spherecast hits player, fire laser!
                    if(enemyBullet == null && Time.time > nextFire)
                    {
                        Attack();
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    float turnAngle = Random.Range(-110, 110);
                    transform.Rotate(0, turnAngle, 0);
                } 
            }
        } 
        else if (state == EnemyStates.chase)
        {
            agent.SetDestination(player.transform.position);

            if (Physics.SphereCast(ray, 1.0f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<Player>())
                {
                    //spherecast hits player, fire laser!
                    if (Time.time > nextFire)
                    {
                        Attack();
                    }
                }
            }
        } else
        {
            rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionZ;

        }
    }

    public void ChangeState(EnemyStates state)
    {
        this.state = state;
    }
    
    private void Attack()
    {
        anim.SetTrigger("Attack");

        nextFire = Time.time + fireRate;
        enemyBullet = Instantiate(enemyBulletPrefab) as GameObject;
        enemyBullet.transform.position =
            transform.TransformPoint(0, 1.5f, 1.5f);
        enemyBullet.transform.rotation = transform.rotation;    
    }




    /*
           // spawn bullet prefab at bulletSpawn(in front of player)
           GameObject bullet = Instantiate(bulletPrefab);
           bullet.transform.position = bulletSpawn.transform.position;

           // get the bullet roation
           Vector3 bulletRotation = bullet.transform.rotation.eulerAngles;
           // set y rotation
           bulletRotation.y = transform.eulerAngles.y;
           // assign rotation back to bullet   
           bullet.transform.rotation = Quaternion.Euler(bulletRotation);

           Rigidbody rbody = bullet.GetComponent<Rigidbody>();
           rbody.AddForce(bulletSpawn.transform.forward * speed, ForceMode.Impulse);

           StartCoroutine(DestroyProjectile(bullet)); 
           
        

    private IEnumerator DestroyProjectile(GameObject bullet)
    {
        yield return new WaitForSeconds(1);
        Destroy(bullet);
    }
     
     */

}