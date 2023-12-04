using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Coloss_spectre_movement : MonoBehaviour, IDamage
{

    [Header("Coloso stats")]
    private Animator coloss_animator;
    [SerializeField] private float life;
    [SerializeField] private float coloss_damage;
    [SerializeField] private GameObject range_controller;
    [SerializeField] private float radio_persecute;
    [SerializeField] private float next_damage;
    [SerializeField] private float points_for_dead;
    
    [Header("Ataque del coloso")]
    [SerializeField] private GameObject hitController;
    [SerializeField] private float radio_hit;
    [SerializeField] private float enemiesTimeGenerator;
    [SerializeField] private GameObject thunder_attack;
    private bool look_right;
    private bool isAttacking;
    private float time_next_enemy;
    private float next_damage_attack;
    private float thunder_cont;
    private GameObject objetive;
    [SerializeField] private GameObject spectre_child;
    [SerializeField] private GameObject spectre_spawn_object;

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        coloss_animator = GetComponent<Animator>();
        objetive = FindObjectOfType<Player_dash>().gameObject;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;    
    }

    void Update()
    {
        MirarJugador();
        Vector2 objetive_position = new Vector2(objetive.transform.position.x, objetive.transform.position.y);
        float distance = Vector2.Distance(range_controller.transform.position, objetive_position);
        next_damage_attack -= Time.deltaTime;
        if(distance < radio_persecute){
            if(!isAttacking && next_damage_attack <= 0){
                coloss_animator.SetTrigger("Attack");
                next_damage_attack = next_damage;
            }
            objetive_position = transform.position;
        }
        navMeshAgent.SetDestination(objetive_position);

        time_next_enemy += Time.deltaTime;

        if(time_next_enemy >= enemiesTimeGenerator){
            time_next_enemy = 0;
            Instantiate(thunder_attack, objetive.transform.position, Quaternion.identity);
            thunder_cont += 1;
        }

        if(thunder_cont == 2){
            SpawnEnemy();
            thunder_cont = 0;
        }
    }

    private void MirarJugador(){
        if((objetive.transform.position.x > transform.position.x && !look_right) || (objetive.transform.position.x < transform.position.x && look_right)){
            look_right = !look_right;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    private void SpawnEnemy(){
        Vector2 spawnPoint = new Vector2(transform.position.x, transform.position.y);
        Instantiate(spectre_spawn_object, spawnPoint, Quaternion.identity);
        Instantiate(spectre_child, spawnPoint, Quaternion.identity);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitController.transform.position, radio_hit);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(range_controller.transform.position, radio_persecute);
    }
    
    private void Melee(){    
        Collider2D[] objects = Physics2D.OverlapCircleAll(hitController.transform.position, radio_hit);

        foreach (Collider2D coll in objects){
            if(coll.CompareTag("Player")){
                coll.transform.GetComponent<Player_status>().TakeDamage(coloss_damage);
            }
        }
    }

    private void AttackTrigger(){
        SpriteRenderer spriteRenderer = hitController.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        isAttacking = true;
        Melee();
    }

    private void AttackOver(){
        SpriteRenderer spriteRenderer = hitController.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        isAttacking = false;
    }

    public void TakeDamage(float damage){
        life -= damage;
        if (life <= 0){
            DestroyColoss();
        }
    }

    // private void SpectreTeletransportation(){
    //     maxX = Camera.main.transform.position.x + 25;
    //     minX = Camera.main.transform.position.x - 25;
    //     maxY = Camera.main.transform.position.y + 25;
    //     minY = Camera.main.transform.position.y - 25;
    //     Vector2 spawnPoint = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    //     if(IsPositionOnGround(spawnPoint)){
    //         gameObject.transform.position = spawnPoint;
    //     }
    // }

    private void DestroyColoss(){
        Player_status player_score = objetive.GetComponent<Player_status>();
        player_score.player_score += points_for_dead;
        Destroy(gameObject);
    }

    // private void OnTriggerStay2D(Collider2D other) {
    //     if(other.CompareTag("Player")){
    //         next_damage -= Time.deltaTime;
    //         if(next_damage <= 0){
    //             spectre_animator.SetTrigger("Attack");
    //             other.GetComponent<Player_status>().TakeDamage(spectre_damage);
    //             next_damage = spectre_time_damage;
    //         }
    //     }
    // }
}
