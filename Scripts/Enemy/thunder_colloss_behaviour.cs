using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunder_colloss_behaviour : MonoBehaviour
{
    [SerializeField] private float radio_hit;
    [SerializeField] private float damage;
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Vector2 position_fixied = new Vector2(transform.position.x, transform.position.y - 1);
        Gizmos.DrawWireSphere(position_fixied, radio_hit);
    }
    
    private void Melee(){    
        Vector2 position_fixied = new Vector2(transform.position.x, transform.position.y - 1);
        Collider2D[] objects = Physics2D.OverlapCircleAll(position_fixied, radio_hit);

        foreach (Collider2D coll in objects){
            if(coll.CompareTag("Player")){
                coll.transform.GetComponent<Player_status>().TakeDamage(damage);
            }
        }
    }
}
