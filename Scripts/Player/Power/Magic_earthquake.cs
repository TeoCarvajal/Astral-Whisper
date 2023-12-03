using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_earthquake : MonoBehaviour
{    
    [SerializeField] private float earthquakeDuration;
    [SerializeField] private float earthquakeDamageDelay;
    [SerializeField] private float damage;

    private float next_damage;
    private Animator animator;


    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        earthquakeDuration -= Time.deltaTime;
        if(earthquakeDuration <= 0){
            animator.SetTrigger("IsOver");
            float delayBeforeDestroy = 0.65f; 
            Invoke("DestroyObject", delayBeforeDestroy);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            next_damage -= Time.deltaTime;
            if(next_damage <= 0){
                IDamage enemy_object = other.GetComponent<IDamage>();
                if(enemy_object != null){
                    enemy_object.TakeDamage(damage);
                }
            }
        }
    }

    private void DestroyObject(){
            Destroy(gameObject);
    }
}
