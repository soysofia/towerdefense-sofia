using System.Collections;
using System.Collections.Generic;
using UnityEngine;



   UnityEngine.AI.NavMeshAgent agent;
    [SerializeField]


    Transform target;
    [SerializeField, Range(0.1f,10f)]
    float moveSpeed = 2f;
    Animator anim;

    [SerializeField, Range(0.1f,7f)]
    float attackDistance = 2f;

    IEnumerator lookingForTarget;

    IEnumerator firing;




public class Turret : MonoBehaviour
{

    [SerializeField]
    Transform cannion;



    private void OnTriggerStay(Collider other)
    {

        if(other.CompareTag("Enemy"))
        {
            cannion.LookAt(other.transform);
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



    // Start is called before the first frame update
    void Start()
    {
      StartLookingTarget();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
