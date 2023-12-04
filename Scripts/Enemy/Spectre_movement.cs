using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spectre_movement : MonoBehaviour, IDamage
{

    [Header("Enemy")]
    private Animator spectre_animator;
    [SerializeField] private float life;
    [SerializeField] private float spectre_damage;
    [SerializeField] private float spectre_time_damage;
    [SerializeField] private float points_for_dead;
    private float next_damage;
    private float maxX, minX, maxY, minY;
    
    [Header("Movement")]
    private GameObject objetive;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        spectre_animator = GetComponent<Animator>();
        objetive = FindObjectOfType<Player_dash>().gameObject;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;    
    }

    void Update()
    {
        Vector2 objetive_position = new Vector2(objetive.transform.position.x, objetive.transform.position.y);
        navMeshAgent.SetDestination(objetive_position);
    }

    private bool IsPositionOnGround(Vector2 position)
    {
        Collider2D[] collider = Physics2D.OverlapPointAll(position);
        
        if (collider != null && collider.Length == 1)
        {
            if(collider[0].CompareTag("Suelo")){
                return true;
            } // La posición está sobre el suelo
        }
        return false; // La posición no está sobre el suelo o no se detectó un colisionador
    }

    public void TakeDamage(float damage){
        life -= damage;
        if (life <= 0){
            spectre_animator.SetTrigger("Dead");
            float delayBeforeDestroy = 0.65f; 
            Invoke("DestroySpectre", delayBeforeDestroy);
        } else {
            spectre_animator.SetTrigger("Teletransportation");
            float delayBeforeMove = 0.65f; 
            Invoke("SpectreTeletransportation", delayBeforeMove);
        }
    }

    private void SpectreTeletransportation(){
        maxX = Camera.main.transform.position.x + 25;
        minX = Camera.main.transform.position.x - 25;
        maxY = Camera.main.transform.position.y + 25;
        minY = Camera.main.transform.position.y - 25;
        Vector2 spawnPoint = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        if(IsPositionOnGround(spawnPoint)){
            gameObject.transform.position = spawnPoint;
        }
    }

    private void DestroySpectre(){
        Player_status player_score = objetive.GetComponent<Player_status>();
        player_score.player_score += points_for_dead;
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            next_damage -= Time.deltaTime;
            if(next_damage <= 0){
                spectre_animator.SetTrigger("Attack");
                other.GetComponent<Player_status>().TakeDamage(spectre_damage);
                next_damage = spectre_time_damage;
            }
        }
    }
    private IEnumerator SpectreAttack(Collider2D other){
        other.GetComponent<Player_status>().TakeDamage(spectre_damage);
        next_damage = spectre_time_damage;
        yield return new WaitForSeconds(0.5f);
        spectre_animator.SetBool("Attack", false);
    }
}
