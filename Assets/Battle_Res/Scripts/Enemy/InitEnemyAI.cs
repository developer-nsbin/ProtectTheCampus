using UnityEngine;

public class InitEnemyAI : MonoBehaviour
{
    private Enemy enemy;
    private GameObject player;          
    private Animator Anim;          
    private Vector3 initialPosition;            

    public float wanderRadius;          
    public float alertRadius;         
    public float defendRadius;         
    public float chaseRadius;            

    public float attackRange;           
    public float walkSpeed;     
    public float turnSpeed;         

    private enum MonsterState
    {
        IDLE,      
        CHECK,       
        WALK,   
        WARN,       
        CHASE,      
        RETURN      
    }

    private MonsterState currentState = MonsterState.IDLE;          

    public float[] stateWeight = { 3000, 3000, 4000 };        
    public float stateResetTime;         
    private float lastStateTime;     

    private float distanceToPlayer;         
    private float distanceToInitial;         
    private Quaternion targetRotation;         

    private bool is_Warned = false;
    private bool is_Running = false;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player");
        Anim = GetComponent<Animator>();

        initialPosition = gameObject.GetComponent<Transform>().position;

        currentState = MonsterState.IDLE;
        RandomAction();
    }

    void RandomAction()
    {
        lastStateTime = Time.time;

        float number = Random.Range(0, stateWeight[0] + stateWeight[1] + stateWeight[2]);

        if (number <= stateWeight[0])
        {
            currentState = MonsterState.IDLE;
            Anim.SetBool("Walk", false);
        }
        else if (stateWeight[0] < number && number <= stateWeight[0] + stateWeight[1])
        {
            currentState = MonsterState.CHECK;
            Anim.SetBool("Walk", false);
        }

        if (stateWeight[0] + stateWeight[1] < number && number <= stateWeight[0] + stateWeight[1] + stateWeight[2])
        {
            currentState = MonsterState.WALK;
            targetRotation = Quaternion.Euler(0, Random.Range(1, 5) * 90, 0);
            Anim.SetBool("Walk", true);
        }
    }

    void Update()
    {
        if (GameManager.Instance.gameOver)
        {
            Anim.SetBool("Attack", false);
            Anim.SetBool("Walk", false);
            return;
        }

        if (enemy.hp <= 0) return;
        switch (currentState)
        {
            case MonsterState.IDLE:
                if (Time.time - lastStateTime > stateResetTime)
                {
                    RandomAction();     
                }

                EnemyDistanceCheck();
                break;
            case MonsterState.CHECK:
                if (Time.time - lastStateTime > Anim.GetCurrentAnimatorStateInfo(0).length)
                {
                    RandomAction();        
                }
                EnemyDistanceCheck();
                break;
            case MonsterState.WALK:
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

                if (Time.time - lastStateTime > stateResetTime)
                {
                    RandomAction(); 
                }
                WanderRadiusCheck();
                break;
            case MonsterState.WARN:
                if (!is_Warned)
                {
                    is_Warned = true;
                }
                targetRotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
                WarningCheck();
                break;
            case MonsterState.CHASE:
                if (!is_Running)
                {
                    Anim.SetBool("Walk", true);
                    is_Running = true;
                }
                targetRotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
                ChaseRadiusCheck();
                break;
            case MonsterState.RETURN:
                targetRotation = Quaternion.LookRotation(initialPosition - transform.position, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
                ReturnCheck();
                break;
        }
    }

    void EnemyDistanceCheck()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer < attackRange)
        {
            Anim.SetBool("Attack", true);
        }
        else if (distanceToPlayer < defendRadius)
        {
            currentState = MonsterState.CHASE;
        }
        else if (distanceToPlayer < alertRadius)
        {
            currentState = MonsterState.WARN;
        }
    }

    void WarningCheck()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer < defendRadius)
        {
            is_Warned = false;
            currentState = MonsterState.CHASE;
        }

        if (distanceToPlayer > alertRadius)
        {
            is_Warned = false;
            RandomAction();
        }
    }

    void WanderRadiusCheck()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        distanceToInitial = Vector3.Distance(transform.position, initialPosition);

        if (distanceToPlayer < attackRange)
        {
            Anim.SetBool("Attack", true);
        }
        else if (distanceToPlayer < defendRadius)
        {
            currentState = MonsterState.CHASE;
        }
        else if (distanceToPlayer < alertRadius)
        {
            currentState = MonsterState.WARN;
        }

        if (distanceToInitial > wanderRadius)
        {
            targetRotation = Quaternion.LookRotation(initialPosition - transform.position, Vector3.up);
        }
    }

    void ChaseRadiusCheck()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        distanceToInitial = Vector3.Distance(transform.position, initialPosition);

        if (distanceToPlayer < attackRange)
        {
            Anim.SetBool("Attack", true);
        }
        else if (distanceToPlayer < chaseRadius)
        {
            Anim.SetBool("Attack", false);

        }
        if (distanceToInitial > chaseRadius || distanceToPlayer > alertRadius)
        {
            
            currentState = MonsterState.RETURN;
        }
    }

    void ReturnCheck()
    {
        distanceToInitial = Vector3.Distance(transform.position, initialPosition);
        if (distanceToInitial < 0.5f)
        {
            is_Running = false;
            RandomAction();
        }
    }

    public void PlayerTakeDamage()
    {
        enemy.PlayAttackClip();
        player.GetComponent<CharacterAnim>().TakeDamage(Random.Range(1, 3));
    }
}
