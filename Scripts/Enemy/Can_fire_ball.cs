using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can_fire_ball : MonoBehaviour
{
    [SerializeField] private float velocity;    
    [SerializeField] private float damage;
    [SerializeField] private float time_ball;
    private GameObject objetive;

    private void Start() {
        objetive = FindObjectOfType<Player_dash>().gameObject;
    } 

    private void Update() {
        float radian_angle = Mathf.Atan2(objetive.transform.position.y - 1 - transform.position.y, objetive.transform.position.x - transform.position.x);
        float degree_angle = (180 / Mathf.PI) * radian_angle - 90;
        transform.rotation = Quaternion.Euler(0, 0, degree_angle);
        transform.Translate(Vector2.up * velocity * Time.deltaTime);
        time_ball -= Time.deltaTime;
        if(time_ball < 0){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
           other.transform.GetComponent<Player_status>().TakeDamage(damage);
        }
        if(!other.CompareTag("Enemy")){
            Destroy(gameObject);
        }
    }
}
