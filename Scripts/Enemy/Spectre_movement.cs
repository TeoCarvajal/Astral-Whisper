using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spectre_movement : MonoBehaviour
{

    [Header("Enemy")]
    private Animator spectre_animator;
    [SerializeField] private float life;
    [SerializeField] private float spectre_damage;
    [SerializeField] private float spectre_time_damage;
    private float next_damage;
    [SerializeField] private float maxX, minX, maxY, minY;
    
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
        Collider2D collider = Physics2D.OverlapPoint(position);
        
        if (collider != null && collider.CompareTag("Suelo"))
        {
            return true; // La posición está sobre el suelo
        }

        return false; // La posición no está sobre el suelo o no se detectó un colisionador
    }

    public void TakeDamage(float damage){
        life -= damage;
        if (life <= 0){
            spectre_animator.SetBool("Dead", true);
            float delayBeforeDestroy = 0.65f; 
            Invoke("DestroySpectre", delayBeforeDestroy);
        } else {
            spectre_animator.SetBool("Teletransportation", true);
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
        spectre_animator.SetBool("Teletransportation", false);
    }

    private void DestroySpectre(){
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            next_damage -= Time.deltaTime;
            if(next_damage <= 0){
                other.GetComponent<Player_status>().TakeDamage(spectre_damage);
                next_damage = spectre_time_damage;
            }
        }
    }
}
