using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_status : MonoBehaviour
{
    [Header ("Status")]
    private Rigidbody2D rb2D;
    private Animator animator;

    [SerializeField] private Hp_bar life_bar;
    [SerializeField] private float player_life;
    [SerializeField] private float player_stamine;
    [SerializeField] private float max_stamine;
    [SerializeField] private float stamine_multiplier;
    [SerializeField] public float player_score;

    [Header ("Power")]

    [SerializeField] private GameObject magic_shield;

    public float Stamine => player_stamine;

    private void Start() {
        life_bar.LifeStart(player_life);
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if(player_stamine <= max_stamine){
            player_stamine += Time.deltaTime*stamine_multiplier;
        }
    }

    
    public void TakeDamage(float damage){
        if(!magic_shield.transform.GetComponent<Magic_shield_behaviour>().ShieldActive){
            player_life -= damage;
            animator.SetTrigger("Damaged");
            life_bar.CurrentLife(player_life);
            if (player_life <= 0){
                Destroy(gameObject);
            }
        }else{
            magic_shield.transform.GetComponent<Magic_shield_behaviour>().ReduceDurability(damage);
        }
    }

    private void DamagedAnimationOver(){
        // animator.SetBool("Damaged", false);
    }

    public void DownStamine(float stamine_rest){
        player_stamine -= stamine_rest;
    }
}
