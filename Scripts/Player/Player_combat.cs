using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_combat : MonoBehaviour
{
    [SerializeField] private Transform punchController;
    [SerializeField] private float punchRadio;
    [SerializeField] private float damage;

    private void Update() {
        if(Input.GetButtonDown("Fire1")){
            Melee();
        }
    }

    private void Melee(){    
        Collider2D[] objects = Physics2D.OverlapCircleAll(punchController.position, punchRadio);

        foreach (Collider2D coll in objects){
            if(coll.CompareTag("Enemy")){
                coll.transform.GetComponent<Spectre_movement>().TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(punchController.position, punchRadio);
    }
}
