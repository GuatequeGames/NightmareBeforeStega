using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;

    [Header("Movement")]
    public int moveSpeed;
    public LayerMask layerFloor;
    public LayerMask layerWalls;
    float horizontal, vertical;
    public float distanceToPosition; //Margen para que el personaje llegue al punto indicado con point and click

    public bool movingToPoint; // Controla si el personaje se está moviendo hacia un punto clickado

    Ray ray; //Raycast para la camara
    RaycastHit hit;

    Ray rayMovement; //Raycast para el point and click
    RaycastHit hitMovement;

    Vector3 whereToGo;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0)) movingToPoint = true; //Click izquierdo: vamos al punto designado
        if (Input.GetMouseButtonDown(1)) movingToPoint = false; //Click derecho: desactivamos el point and click

    }

    private void FixedUpdate()
    {
        Move();
        Animate();
        Turn();
        MoveToPoint();
    }


    private void Move()
    {
        // Dificultad fácil: exactamente igual que en el Survival Shooter
        /*
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        if(direction.magnitude>1)
            direction = direction.normalized;

        rb.MovePosition(transform.position+(direction*moveSpeed*Time.deltaTime));
        //*/

        /*//Dificultad media: igual que en el Survival Shooter pero que el personaje se mueve siempre hacia donde está mirando cuando pulsamos W
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        if (direction.magnitude > 1)
            direction = direction.normalized;

        rb.MovePosition(transform.position + (transform.right* direction.x + transform.forward * direction.z )*moveSpeed*Time.deltaTime); // **** horizontal****
    
        //*/


        //Dificultad pesadilla: Point and click


    }
    void Animate()
    {
        /*//¿Se está moviendo el personaje?/*
        if (horizontal != 0 || vertical != 0)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }*/

        //Hacemos animacion en base a velocidad del rigidbody.

        if (movingToPoint)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }

    void Turn()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        if(Physics.Raycast(ray,out hit, Mathf.Infinity, layerFloor))
        {
            Vector3 playerToMouse = hit.point- transform.position;
            playerToMouse.y = 0;
            //Creo una rotación donde el eje Z del personaje se alinea con el vector playerToMouse
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            rb.MoveRotation(newRotation);
            if(movingToPoint == false)
            {
                whereToGo = hit.point;
                whereToGo.y = 0;
            } // Si no se mueve, almacenamos la posicion a la que apunta, para no sobreescribir el valor
                

            
        }

    }

    void MoveToPoint()
    {
        if (movingToPoint)
        {
            rayMovement.origin = transform.position;
            rayMovement.direction = whereToGo-transform.position;
            Debug.DrawRay(rayMovement.origin, rayMovement.direction * 100, Color.green);
            if (Physics.Raycast(rayMovement, out hitMovement, Mathf.Infinity, layerWalls))
            {
                movingToPoint = false;
                return;

            }
            else
            {
                Vector3 direction = whereToGo - transform.position;
                if ((direction).magnitude > distanceToPosition)
                {
                    rb.MovePosition(transform.position + direction.normalized * moveSpeed * Time.deltaTime);

                }
                
                else
                {
                    movingToPoint = false;
                }
            }
        }
        
    }
}
