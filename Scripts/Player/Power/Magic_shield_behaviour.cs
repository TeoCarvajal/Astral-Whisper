using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_shield_behaviour : MonoBehaviour
{
    [SerializeField] private float shieldDurability;
    [SerializeField] private float shieldReduction;
    [SerializeField] private float currentDuration;
    private bool isActive = false;

    public bool ShieldActive => isActive;

    private void Update() {
        if(isActive){
            currentDuration -= Time.deltaTime * shieldReduction;
            if(currentDuration <= 0){
                DesactivateShield();
            }
        }
    }

    public void ActivateShield(){
        isActive = true;
        gameObject.SetActive(true);
        currentDuration = shieldDurability;
    }


    private void DesactivateShield(){
        isActive = false;
        gameObject.SetActive(false);
        // currentDuration = 0;
    }

    public void ReduceDurability(float damageAmount){
        if (isActive){
            currentDuration -= damageAmount;
            if (currentDuration <= 0){
                DesactivateShield();
            }
        }
    }
}
