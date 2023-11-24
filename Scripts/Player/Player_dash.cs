using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_dash : MonoBehaviour
{
    
    private Player_movement player;
    private Rigidbody2D rb2D;

    [Header("Dash")]
    [SerializeField] private float dash_time = 0.2f;
    [SerializeField] private float dash_force = 20f;
    [SerializeField] private float dash_freeze_time = 1f;
    [SerializeField] private bool is_dashing;
    public bool IsDashing => is_dashing;
    [SerializeField] private bool can_dash;

    private void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
        player = GetComponent<Player_movement>();
    }

    void Update()
    {  
        if(Input.GetKeyDown(KeyCode.C)){
            StartCoroutine(Dash());
        }
        
    }

    private IEnumerator Dash(){
        if( player.Direction.sqrMagnitude != 0 && can_dash){
            is_dashing = true;
            can_dash = false;
            rb2D.velocity = player.Direction * dash_force;
            yield return new WaitForSeconds(dash_time);
            is_dashing = false;
            yield return new WaitForSeconds(dash_freeze_time);
            can_dash = true;
        }
    }
}
