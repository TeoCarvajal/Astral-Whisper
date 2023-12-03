using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infernus_special_attack : MonoBehaviour
{
    [SerializeField] private float specialAttackDuration;

    private void Update() {
        specialAttackDuration -= Time.deltaTime;
        if(specialAttackDuration < 0){
            Destroy(gameObject);
        }
    }
}
