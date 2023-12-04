using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_sphere_behaviour : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float damage;

    private void Update() {
        transform.Translate(Vector2.up * velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            other.GetComponent<Spectre_movement>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
