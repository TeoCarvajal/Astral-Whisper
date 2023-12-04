using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_potion_behaviour : MonoBehaviour
{
    [SerializeField] private float potionDistance;
    [SerializeField] private float potionDuration;
    [SerializeField] private float potionDamageDelay;
    [SerializeField] private float velocity;
    [SerializeField] private float damage;

    private Collider2D collider;
    private float next_damage;
    private float initx;
    private float inity;

    private Vector2 initialPosition;

    private Animator animator;


    private void Start() {
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        initx = transform.position.x;
        inity = transform.position.y;    
        initialPosition = new Vector2(initx, inity);
    }
    private void Update() {
        float distance = Vector2.Distance(transform.position, initialPosition);
        if(distance < potionDistance){
            transform.Translate(Vector2.up * velocity * Time.deltaTime);
        }else{
            potionDuration -= Time.deltaTime;
            animator.SetTrigger("Dropped");
            collider.enabled = true;
        }
        if(potionDuration <= 0){
            animator.SetTrigger("IsOver");
            float delayBeforeDestroy = 0.65f; 
            Invoke("DestroyPotion", delayBeforeDestroy);
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
                next_damage = potionDamageDelay;
            }
        }
    }

    private void DestroyPotion(){
        Destroy(gameObject);
    }
}
