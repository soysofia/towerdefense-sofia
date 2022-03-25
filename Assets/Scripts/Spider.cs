using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField]


    Transform target;
    [SerializeField, Range(0.1f,10f)]
    float moveSpeed = 2f;
    Animator anim;

    [SerializeField, Range(0.1f,7f)]
    float attackDistance = 2f;

    IEnumerator lookingForTarget;

    IEnumerator firing;
    
    [SerializeField]

    float fireRate;

    bool canFire = true;

    [SerializeField]
    GameObject bulletObj;

    [SerializeField]
    Transform bulletSpawnPoint;


    [SerializeField]
    Transform bulletsContainer;

    [SerializeField]
    Queue<Transform> bulletsInGameplay;

        Queue<Transform> bullets;


    void Awake()
    {
        agent = GetComponent <NavMeshAgent>();
        anim = GetComponent<Animator>();
        bullets = new Queue<Transform>();

        bulletsInGameplay = new Queue<Transform>();
       
    }


    void Start()
    {   
        InitBullets();
        agent.speed = moveSpeed;
        agent.destination = target.position;
        StartLookingTarget();
    }


    void InitBullets()
    {
        foreach(Transform bullet in bulletsContainer)
        {
            bullets.Enqueue(bullet.transform);
        }
    }

   void StartLookingTarget()
    {
        lookingForTarget = LookingForTarget();
        StartCoroutine(lookingForTarget);
    }

    IEnumerator LookingForTarget()
    {
        agent.destination = target.position;

        while(true)
        {

            if(!IsMoving)
            {

             agent.destination = transform.position;
             if(canFire)
             {
                StartFiring();
                break;
             }

            }
            yield return null;
        }

    }


  void  StartFiring()
  {
      firing = Firing();
      StartCoroutine(firing);
  }

    IEnumerator Firing()
    {
        if(canFire && bullets.Count > 0)
        {
            anim.SetTrigger("fire");
            canFire =false;

          //  bullets.Enqueue(bullet);
        }

        yield return new WaitForSeconds(fireRate);
        canFire =true;
        StartLookingTarget();

    }


    void LateUpdate()
    {
        anim.SetFloat("move" , MoveValue );

    }

    float MoveValue => Vector3.Distance(transform.position, target.position) > attackDistance ? 1f :0f;
    bool IsMoving => MoveValue > 0f;


   void ShotFire()
   {
      Transform bullet = bullets.Dequeue();
        bullet.SetPositionAndRotation(bulletSpawnPoint.position, transform.rotation);
         Bullet bulletScript = bullet.GetComponent<Bullet>();
        bullet.gameObject.SetActive(true);
      
       bulletScript.SetSpider(this);
   }


    public void AddBullet(Transform bullet ) => bullets.Enqueue(bullet);
    

}
