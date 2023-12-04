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
    private float maxX, minX, maxY, minY;

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
        if(Input.GetKeyDown(KeyCode.E)){
            PlayerTeletransportation();
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

    private bool IsPositionOnGround(Vector2 position)
    {
        Collider2D[] collider = Physics2D.OverlapPointAll(position);
        
        if (collider != null && collider.Length == 1)
        {
            if(collider[0].CompareTag("Suelo")){
                return true;
            } // La posición está sobre el suelo
        }
        return false; // La posición no está sobre el suelo o no se detectó un colisionador
    }

    private void PlayerTeletransportation(){
        maxX = Camera.main.transform.position.x + 25;
        minX = Camera.main.transform.position.x - 25;
        maxY = Camera.main.transform.position.y + 25;
        minY = Camera.main.transform.position.y - 25;
        Vector2 spawnPoint = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        if(IsPositionOnGround(spawnPoint)){
            gameObject.transform.position = spawnPoint;
        }
    }
}
