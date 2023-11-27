using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_status : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;

    [SerializeField] private float player_life;
    [SerializeField] private float player_stamine;
    [SerializeField] private float max_stamine;
    [SerializeField] private float stamine_multiplier;

    public float Stamine => player_stamine;

    private void Start() {
        
    }

    private void Update() {
        if(player_stamine <= max_stamine){
            player_stamine += Time.deltaTime*stamine_multiplier;
        }
    }

    public void TakeDamage(float damage){
        player_life -= damage;
        if (player_life <= 0){
            Destroy(gameObject);
        }
    }

    public void DownStamine(float stamine_rest){
        player_stamine -= stamine_rest;
    }
}
