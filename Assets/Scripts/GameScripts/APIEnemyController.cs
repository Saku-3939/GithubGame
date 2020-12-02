using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class APIEnemyController : MonoBehaviour
{
    public bool moveEnabled = true;

    //カスタム可能
    [SerializeField]
    int maxHp = 3;
    [SerializeField]
    int ammoDamage = 5;
    [SerializeField]
    int attackInterval = 1;
    [SerializeField]
    int score = 100;


    [SerializeField]
    string targetTag = "Player";
    [SerializeField]
    float deadTime = 3;
    [SerializeField]
    float attackDistance = 10f;

    bool attacking = false;
    int hp;
    float moveSpeed;
    Animator animator;
    CapsuleCollider capsuleCollider;
    Rigidbody rigidbody;
    NavMeshAgent agent;
    Transform target;
    GameManager gameManager;
    FPSGunController player;

    public int Hp
    {
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp);

            if(hp <= 0)
            {
                StartCoroutine(Dead());
            }
        }
        get
        {
            return hp;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();

        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<FPSGunController>();

        InitCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveEnabled)
        {
            Move();
        }
        else
        {
            Stop();
        }
        
    }

    void InitCharacter()
    {
        hp = maxHp;
        moveSpeed = agent.speed;
    }

    void Move()
    {
        agent.speed = moveSpeed;
        animator.SetFloat("MoveSpeed", agent.speed, 0.1f, Time.deltaTime);

        agent.SetDestination(target.position);
        rigidbody.velocity = agent.desiredVelocity;

        float distance = Vector3.Distance(target.position, transform.position);
        if(distance < attackDistance)
        {
            StartCoroutine(AttackTimer());
        }
    }

    void Stop()
    {
        agent.speed = 0;
        animator.SetFloat("MoveSpeed", agent.speed, 0.1f, Time.deltaTime);
    }

    IEnumerator Dead()
    {
        moveEnabled = false;
        Stop();
        gameManager.Score += score;
        animator.SetTrigger("Death");
        capsuleCollider.enabled = false;
        rigidbody.isKinematic = true;
        yield return new WaitForSeconds(deadTime);
        Destroy(gameObject);
    }

    IEnumerator AttackTimer()
    {
        if (!attacking)
        {
            attacking = true;
            moveEnabled = false;

            transform.LookAt(target.position);
            animator.SetTrigger("Attack");
            player.Ammo -= ammoDamage;
            yield return new WaitForSeconds(attackInterval);

            attacking = false;
            moveEnabled = true;

        }
        yield return null; 
    }

}
