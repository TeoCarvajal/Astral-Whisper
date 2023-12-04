using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel1Monitor : MonoBehaviour
{
    [SerializeField] private GameObject enemy_generator;
    [SerializeField] private GameObject final_boss_lv1;
    [SerializeField] private GameObject player;
    private bool isFinalBossAlive;
    private Player_status player_status;
    private Enemy_generator enemy_generator_controller;

    private void Start() {
        isFinalBossAlive = false;
        player_status = player.GetComponent<Player_status>();
        enemy_generator_controller = enemy_generator.GetComponent<Enemy_generator>();
        enemy_generator_controller.maxEnemies = 1;
    } 

    private void Update() {
        if(player_status.player_score == 50 && !isFinalBossAlive){
            isFinalBossAlive = true;
            Instantiate(final_boss_lv1, transform.position, Quaternion.identity);
        }
    }
    
}
