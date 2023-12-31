using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_dash : MonoBehaviour
{

    private Player_status player_status;    
    private Player_movement player;
    private Rigidbody2D rb2D;

    [Header("Dash")]
    [SerializeField] private float dash_time = 0.2f;
    [SerializeField] private float dash_force = 20f;
    [SerializeField] private float dash_freeze_time = 1f;
    [SerializeField] private TrailRenderer trail_renderer;
    [SerializeField] private bool is_dashing;
    [SerializeField] private float stamine_penalty;
    public bool IsDashing => is_dashing;
    [SerializeField] private bool can_dash;

    private void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
        player_status = GetComponent<Player_status>();
        player = GetComponent<Player_movement>();
    }

    void Update()
    {  
        if(Input.GetKeyDown(KeyCode.C)){
            StartCoroutine(Dash());
        }
        
    }

    private IEnumerator Dash(){
        if( player.Direction.sqrMagnitude != 0 && can_dash && (player_status.Stamine >= stamine_penalty)){
            is_dashing = true;
            can_dash = false;
            trail_renderer.emitting = true;
            rb2D.velocity = player.Direction * dash_force;
            player_status.DownStamine(stamine_penalty);
            yield return new WaitForSeconds(dash_time);
            is_dashing = false;
            trail_renderer.emitting = false;
            yield return new WaitForSeconds(dash_freeze_time);
            can_dash = true;
        }
    }
}
