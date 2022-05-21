using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Patroling : MonoBehaviour
{
    public Transform pointA,pointB; //Puntos de la ruta
    Transform actualPoint; // Punto al que se dirige
    NavMeshAgent agent;
    Animator anim;
    bool arrivedToPoint; //variable que controla si ha llegado a un punto, para ir al siguiente

    public float waitTime; // Tiempo que espera para ir de un punto a otro
    float time;

    Ray ray; //Raycast que apuntará siempre al player, para controlar la distancia y si está alineado con el forward del fantasma
    RaycastHit hit;
    public LayerMask layerWalls, layerPlayer;
    public float distanceToDetect; //Distancia a partir de la cual detectamos al player
    public float angleToDetect; //Angulo de vision del enemigo

    GameManager gameManager;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        actualPoint = pointA;
        agent.stoppingDistance = 1;
        GoToPoint();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();


    }

    void FixedUpdate()
    {
        ArrivedToPoint();
        IsDetectingPlayer();
    }
    

    void GoToPoint()
    {
        arrivedToPoint=false;
        agent.SetDestination(actualPoint.position);
    }

    void ArrivedToPoint()
    {
        time += Time.deltaTime;
        if (agent.velocity.magnitude == 0 && !arrivedToPoint && time > waitTime)
        {
            if (actualPoint == pointA) actualPoint = pointB;
            else actualPoint = pointA;
            arrivedToPoint = true;
            Debug.Log("Cambiando la ruta");
            time = 0;
            GoToPoint();

        }
    }

    void IsDetectingPlayer()
    {
        ray.origin = transform.position;
        ray.direction = player.transform.position-transform.position;
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerWalls)) // Si detecta un muro, no pasa nada
        {
            if (hit.collider.tag == "Player")
            {
                if (hit.distance < distanceToDetect)
                {
                    //Angulo entre la direccion del personaje y el forward del enemigo
                    float angleOfVision = Vector3.Angle(hit.point - transform.position, transform.forward);
                    Debug.Log("Angulo de vision actual: " + angleOfVision);
                    if (angleOfVision < angleToDetect)
                    {
                        gameManager.GameOver(false);

                    }
                }
            }
        }
        
    }
}
