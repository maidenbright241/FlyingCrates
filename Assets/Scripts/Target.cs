using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour

{   private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;

    public ParticleSystem explosionParticle;
    public int pointValue; //assigned point for each one prefab

    // Start is called before the first frame update
    public void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse); //adding force for the RB in range 12 to 16, with impulse mode
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);  //Torque gives power for rotation of the object
        transform.position = RandomSpawnPos();  //definition of the position of the target
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    public float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    public Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnMouseDown() // if the mouse-button-clicked on the curr GO, the logics in its blockof code will be executed
    {
        if(this.gameManager.isGameActive)
        {
            Destroy(this.gameObject);
            gameManager.UpdateScore(pointValue);  //through the scipt, we give parameter current pointValue to method UpdateScore. So, when the GO will be destroyed,the score will be increased
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }   
    }

    private void OnTriggerEnter(Collider other) //we've empty GO Sensor, which is have box collider with trigger, so if our collider connect with the other, GO will be destroyed 
    {
        if(!this.gameObject.CompareTag("Bad"))
        { 
            this.gameManager.GameOver(); //if target tag is not bad, the method gameover will be called
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
