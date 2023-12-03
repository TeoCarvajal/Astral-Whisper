using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim_rotation : MonoBehaviour
{
    private Vector3 objetive;
    [SerializeField] private Camera camera;

    private void Update() {
        if(Input.GetButton("Fire2")){
            Aim();
        }
    }

    private void Aim(){
        objetive = camera.ScreenToWorldPoint(Input.mousePosition);

        float radian_angle = Mathf.Atan2(objetive.y - transform.position.y, objetive.x - transform.position.x);
        float degree_angle = (180 / Mathf.PI) * radian_angle - 90;
        transform.rotation = Quaternion.Euler(0, 0, degree_angle);
    }
}
