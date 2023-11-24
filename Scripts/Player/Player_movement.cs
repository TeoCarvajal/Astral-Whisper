using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{

    private Player_dash player_dash;
    [Header("Movement")]
    [SerializeField] private float _VelocidadMovimiento;
    [SerializeField] private float _VelocidadSprint;
    [SerializeField] private Vector2 direccion;
    [SerializeField] private bool sprint;
    public Vector2 Direction => direccion;
    public float VelocidadMovimiento => _VelocidadMovimiento;
    private Rigidbody2D rb2D;

    private void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        player_dash = GetComponent<Player_dash>();
    }

    private void Update() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        sprint = Input.GetKey(KeyCode.LeftShift);
        direccion = new Vector2(moveX, moveY).normalized;
    }
    
    private void FixedUpdate() {
        if(!player_dash.IsDashing){
            if(sprint){
                rb2D.MovePosition(rb2D.position + direccion * _VelocidadSprint * _VelocidadMovimiento * Time.fixedDeltaTime);
            }else{
                rb2D.MovePosition(rb2D.position + direccion * _VelocidadMovimiento * Time.fixedDeltaTime);
            }
            
        }
    }
}
