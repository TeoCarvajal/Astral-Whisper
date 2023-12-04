using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Can_infernus_movement : MonoBehaviour, IDamage
{

    [Header("Enemy Stats")]
    [SerializeField] private float life;
    [SerializeField] private float points_for_dead;
    
    [Header("Enemy Movement")]

    [SerializeField] private float radio_persecute;
    [SerializeField] private float time_running_away;
    [SerializeField] private float runningDistance;
    
    [Header("Enemy Attack")]
    [SerializeField] private GameObject can_fire_ball;
    [SerializeField] private GameObject hitController;

    [SerializeField] private float damage_delay;
    private float can_time_next_damage;
    private Animator can_animator;
    private float next_damage;
    private float time_running;
    private bool isRunningAway;
    private bool look_right;
    private float maxX, minX, maxY, minY;

    private Vector2 spawnDebugger;
    
    [Header("Movement")]
    private GameObject objetive;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        can_animator = GetComponent<Animator>();
        objetive = FindObjectOfType<Player_dash>().gameObject;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;    
        isRunningAway = false;
    }

    void Update()
    {
        CanMovement();
        SetCanDestination();
        time_running -= Time.deltaTime;
        can_time_next_damage -= Time.deltaTime;
        if(isRunningAway && time_running <= 0){
            isRunningAway = false;
        }

    }

    private void CanMovement(){
        can_animator.SetFloat("Speed", navMeshAgent.velocity.sqrMagnitude);
        if((navMeshAgent.velocity.x > 0 && !look_right) || (navMeshAgent.velocity.x < 0 && look_right)){
            look_right = !look_right;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    private void SetCanDestination(){
        Vector2 objetive_position = new Vector2(objetive.transform.position.x, objetive.transform.position.y);
        float distance = Vector2.Distance(transform.position, objetive_position);
        if(distance < radio_persecute){
            CanAttack();
            objetive_position = transform.position;
        }
        if(!isRunningAway){
            navMeshAgent.SetDestination(objetive_position);
        }
    }

    private void CanAttack(){
        if(can_time_next_damage < 0){
            Instantiate(can_fire_ball, hitController.transform.position, Quaternion.identity);
            can_time_next_damage = damage_delay;
        }
    }


    public void TakeDamage(float damage){
        life -= damage;
        if (life <= 0){
            DestroySpectre();
        } else {
            RunningAway();
        }
    }

    private void RunningAway(){
        float random_angle = Random.Range(0, (2*Mathf.PI));
        Vector2 spawnPoint = new Vector2(transform.position.x + runningDistance * Mathf.Cos(random_angle), transform.position.y + runningDistance * Mathf.Sin(random_angle));
        spawnDebugger = spawnPoint;
        navMeshAgent.SetDestination(spawnPoint);
        isRunningAway = true;
        time_running = time_running_away;
    }

    private void DestroySpectre(){
        Player_status player_score = objetive.GetComponent<Player_status>();
        player_score.player_score += points_for_dead;
        Destroy(gameObject);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnDebugger, 1);
    }

}
