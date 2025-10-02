using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class Enemy : MonoBehaviour
{
    public enum EnemyState { Idle, Trace, Attack, RunAway }

    public EnemyState state = EnemyState.Idle;


    public float moveSpeed = 2f;
    public float traceRange = 12f;
    public float attackRange = 6f;
    public float attackCooldown = 1.5f;


    public GameObject projectilePrefab;
    private float lastAttackTime;
    public int maxHP = 5;
    private int currentHP;
    public float FleeHPPercentage = 0.3f;

    public Transform firePoint;
    public Slider hpEnemySlider;

    private Transform player; 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = -attackCooldown;  //
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        if (currentHP < 3 && state != EnemyState.Idle)
        {
            state = EnemyState.RunAway;
        }


        switch (state)
        {


            case EnemyState.Idle:
                if (dist < traceRange)
                    state = EnemyState.Trace;
                break;

           case EnemyState.Trace:
                if (dist < attackRange)
                    state = EnemyState.Attack;
                else if (dist > traceRange)
                    state = EnemyState.Idle;
                else
                    TracePlayer();
                break;

                case EnemyState.Attack:
                if (dist > attackRange)
                    state = EnemyState.Trace;
                else
                    AttackPlayer();
                break;

                case EnemyState.RunAway:
                if (dist < traceRange)
                    RunAway();
                else
                    state = EnemyState.Idle;
                break;

        }
    }

    void TracePlayer()
    {
        Debug.Log("Trace");
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    void RunAway ()
    {
        if(currentHP < 3)
        {
         Vector3 dir = (transform.position - player.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        }
        
    }
    

    void AttackPlayer()
        {
if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            ShootingProjectile();
        }
    }
    void ShootingProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            transform.LookAt(player.position);
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();
            if (ep != null)
            {
                Vector3 dir = (player.position - firePoint.position).normalized;
                ep.SetDirection(dir);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        hpEnemySlider.value = (float)currentHP / maxHP;
        if (currentHP <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        Destroy(gameObject);
    }
}
