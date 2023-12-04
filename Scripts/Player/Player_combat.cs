using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_combat : MonoBehaviour
{
    [SerializeField] private Transform punchController;
    [SerializeField] private float punchRadio;
    [SerializeField] private float damage;

    [Header ("Power")]

    [SerializeField] private GameObject ranged_sphere;

    [SerializeField] private bool canShield;
    [SerializeField] private GameObject magic_shield;

    [SerializeField] private GameObject potion;

    [SerializeField] private GameObject earthquake;

    private void Start() {
    }

    private void Update() {
        if(Input.GetButtonDown("Fire1")){
            Melee();
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            RangedAttack();
        }

        if(Input.GetKeyDown(KeyCode.F)){
            PotionAttack();
        }

        if(Input.GetKeyDown(KeyCode.Q) && !magic_shield.transform.GetComponent<Magic_shield_behaviour>().ShieldActive && canShield){
            ShieldActivate();
        }

        if(Input.GetKeyDown(KeyCode.R)){
            EarthquakeSkill();
        }
    }

    private void Melee(){    
        Collider2D[] objects = Physics2D.OverlapCircleAll(punchController.position, punchRadio);

        foreach (Collider2D coll in objects){
            // if(coll.CompareTag("Enemy")){
            //     coll.transform.GetComponent<Spectre_movement>().TakeDamage(damage);
            // }
            IDamage enemy_object = coll.GetComponent<IDamage>();
            if(enemy_object != null){
                enemy_object.TakeDamage(damage);
            }
        }
    }

    private void RangedAttack(){
        Instantiate(ranged_sphere, punchController.position, punchController.rotation);
    }

    private void PotionAttack(){
        Instantiate(potion, punchController.position, punchController.rotation);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(punchController.position, punchRadio);
    }

    private void ShieldActivate(){
        magic_shield.transform.GetComponent<Magic_shield_behaviour>().ActivateShield();
    }

    private void EarthquakeSkill(){
        Instantiate(earthquake, punchController.position, punchController.rotation);
    }
}
