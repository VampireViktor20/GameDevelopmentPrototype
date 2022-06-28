﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Enemy2 : MonoBehaviour
{

    public GameObject player;
    public EnemyHealth enemyHealth;


    public Animator anim;

   
    public NavMeshAgent navMeshAgent;              
    public float startWaitTime = 4;                
    public float timeToRotate = 1f;                  
    public float speedWalk = 6;                   
    public float speedRun = 9;                 

    public float viewRadius = 15;                  
    public float viewAngle = 90;                  
    public LayerMask playerMask;                  
    public LayerMask obstacleMask;                
    public float meshResolution = 1.0f;            
    public int edgeIterations = 4;                  
    public float edgeDistance = 0.5f;


    public GameObject enemyBulletBarrel;
    public GameObject enemyBullet;

    public Enemy2 enemy2;


    public Transform[] waypoints;                 
    int m_CurrentWaypointIndex;                   

    Vector3 playerLastPosition = Vector3.zero;     
    Vector3 m_PlayerPosition;                      

    float m_WaitTime;                            
    float m_TimeToRotate;                           
    bool m_playerInRange;                         
    bool m_PlayerNear;                             
    bool m_IsPatrol;                               
    bool isShooting;

    void Start()
    {
    
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;

        m_playerInRange = false;
        m_PlayerNear = false;
        m_WaitTime = startWaitTime;               
        m_TimeToRotate = timeToRotate;

       
        

        m_CurrentWaypointIndex = 0;                 
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;           
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);    


    }

    private void FixedUpdate()
    {
        EnviromentView();                      

        if (!m_IsPatrol)
        {
            transform.LookAt(player.transform.position);
            if (isShooting) return;
            StartCoroutine(Shooting());
            anim.SetBool("IsAiming", true);


        }
        else
        {
            Patrolling();
            anim.SetBool("IsWalking", true);
        }



    }
    void Update()
    {
        if (enemyHealth.health == 0)
        {

            enemy2.GetComponent<Enemy2>().enabled = false;

            StartCoroutine(Death());
        }
    }

    public IEnumerator Death()
        {

            anim.SetBool("IsDead2", true);
            yield return new WaitForSeconds(0f);
        }


 

    public IEnumerator Shooting()
    {

            
            Stop();
            isShooting = true;
            Instantiate(enemyBullet, enemyBulletBarrel.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.75f);
            isShooting = false;
        

    }

    public void Patrolling()
    {
        if (m_PlayerNear)
        {
          
            if (m_TimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
         
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;          
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);    
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                
                if (m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }

  

    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }


    void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3)
        {
            if (m_WaitTime <= 0)
            {
                m_PlayerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);   

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);         
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_playerInRange = true;             
                    m_IsPatrol = false;                
                }
                else
                {
                   
                    m_playerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                
                m_playerInRange = false;             
            }
            if (m_playerInRange)
            {
              
                m_PlayerPosition = player.transform.position;     
            }
        }
    }





}
