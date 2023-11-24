using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_generator : MonoBehaviour
{   
    [SerializeField] private float maxX, minX, maxY, minY;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float enemiesTime;
    [SerializeField] private float maxEnemies;
    private float time_next_enemy;
    private float contador = 0;
    void Start()
    {
        maxX = Camera.main.transform.position.x + 25;
        minX = Camera.main.transform.position.x - 25;
        maxY = Camera.main.transform.position.y + 25;
        minY = Camera.main.transform.position.y - 25;
    }
    // Update is called once per frame
    void Update()
    {
        maxX = Camera.main.transform.position.x + 25;
        minX = Camera.main.transform.position.x - 25;
        maxY = Camera.main.transform.position.y + 25;
        minY = Camera.main.transform.position.y - 25;

        time_next_enemy += Time.deltaTime;

        if(time_next_enemy >= enemiesTime){
            time_next_enemy = 0;
            SpawnEnemy();
        }
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


    private void SpawnEnemy(){
        int enemySelected = Random.Range(0, enemies.Length);
        Vector2 spawnPoint = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        
        if(IsPositionOnGround(spawnPoint) && contador <= maxEnemies){
            Instantiate(enemies[enemySelected], spawnPoint, Quaternion.identity);
            contador += 1;
        }
    }
}
