using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Infernus_beast : MonoBehaviour, IDamage
{
    [Header("Enemy")]
    private Animator infernus_animator;
    [SerializeField] private float life;
    [SerializeField] private float infernus_damage;
    [SerializeField] private float infernus_radius_attack;
    [SerializeField] private float attack_time;
    [SerializeField] private float points_for_dead;
    [SerializeField] private GameObject infernus_special_attack;
    private float next_damage;
    private bool isAttacking;
    private float attack_cont;
    private float current_attack_time;
    private float special_attack_time;
    
    [Header("Movement")]
    private GameObject objetive;
    private NavMeshAgent navMeshAgent;


    void Start()
    {
        infernus_animator = GetComponent<Animator>();
        objetive = FindObjectOfType<Player_dash>().gameObject;
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;    
    }

    void Update()
    {
        special_attack_time -= Time.deltaTime;
        current_attack_time -= Time.deltaTime;
        Vector2 objetive_position = new Vector2(objetive.transform.position.x, objetive.transform.position.y);
        float distance = Vector2.Distance(transform.position, objetive_position);
        if(distance < infernus_radius_attack && !isAttacking && current_attack_time < 0){
            if(attack_cont == 2){
                if(special_attack_time < 0){
                    SpecialInfernusAttack();
                }
            }else{
                InfernusAttack();
                attack_cont += 1;
            }
        }else{
            if(!isAttacking){
                navMeshAgent.SetDestination(objetive_position);
            }
        }
    }

    private void SpecialInfernusAttack(){
        isAttacking = true;
        navMeshAgent.SetDestination(transform.position);
        infernus_animator.SetTrigger("SpecialAttack");
    }

    private void InfernusAttack(){
        isAttacking = true;
        navMeshAgent.SetDestination(transform.position);
        infernus_animator.SetTrigger("Attack");
    }

    private void SpecialAttackStart(){
        Vector2 position_fixed = new Vector2(transform.position.x, transform.position.y - 2);
        Instantiate(infernus_special_attack, position_fixed, Quaternion.identity);
        special_attack_time = 15;
        attack_cont = 0;
    }

    private void SpecialAttackEnd(){
        isAttacking = false;
        current_attack_time = attack_time; 
    }
    
    private void MeleeStart(){
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, infernus_radius_attack);

        foreach (Collider2D coll in objects){
            if(coll.CompareTag("Player")){
                coll.transform.GetComponent<Player_status>().TakeDamage(infernus_damage);
            }
        }
    }

    private void MeleeEnd(){
        isAttacking = false;
        current_attack_time = attack_time; 
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, infernus_radius_attack);
    }
    
    public void TakeDamage(float damage){
        life -= damage;
        if (life <= 0){
            DestroyBeast();
        }
    }

    private void DestroyBeast(){
        Player_status player_score = objetive.GetComponent<Player_status>();
        player_score.player_score += points_for_dead;
        Destroy(gameObject);
    }
}
