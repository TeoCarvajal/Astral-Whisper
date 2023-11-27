using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{

    private Player_status player_Status;
    private Player_dash player_dash;
    [Header("Movement")]
    [SerializeField] private float _VelocidadMovimiento;
    [SerializeField] private float _VelocidadSprint;
    [SerializeField] private Vector2 direccion;
    [SerializeField] private bool sprint;
    [SerializeField] private float stamine_penalty;
    public Vector2 Direction => direccion;
    public float VelocidadMovimiento => _VelocidadMovimiento;
    private Rigidbody2D rb2D;
    private Animator animator;

    private void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        player_dash = GetComponent<Player_dash>();
        player_Status = GetComponent<Player_status>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        sprint = Input.GetKey(KeyCode.LeftShift);
        direccion = new Vector2(moveX, moveY).normalized;
        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
        animator.SetFloat("Speed", direccion.sqrMagnitude);
        if(direccion.sqrMagnitude > 0 && sprint && (player_Status.Stamine >= -(stamine_penalty*2))){
            player_Status.DownStamine(Time.fixedDeltaTime*stamine_penalty);
        }
    }
    
    private void FixedUpdate() {
        if(!player_dash.IsDashing){
            if(sprint && (player_Status.Stamine >= 0)){
                rb2D.MovePosition(rb2D.position + direccion * _VelocidadSprint * _VelocidadMovimiento * Time.fixedDeltaTime);
            }else{
                rb2D.MovePosition(rb2D.position + direccion * _VelocidadMovimiento * Time.fixedDeltaTime);
            }
            
        }
    }
}
