using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public float mass;
    public float engineForce;
    public float engineForceAdd;
    public float speedAdd;
    public float brakeForce;
    public float drag;
    public float rollingResistance;
    public float turnSpeed;
    public Text speedIndicator;

    private float speed;
    private Vector3 acceleration;
    private Vector3 velocity;
    private float hInput;
    private float vInput;

    private Rigidbody rbSphere;
    private CarCollision coll;

    private void Start()
    {
        rbSphere = GetComponentInChildren<Rigidbody>();
        coll = GetComponentInChildren<CarCollision>();
        rbSphere.transform.parent = null;
        rbSphere.drag = 0;
        rbSphere.angularDrag = 0;
        rbSphere.mass = 1;
    }

    private void Update()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        transform.position = rbSphere.transform.position;

        speed = (int)velocity.magnitude/10;
        speedIndicator.text = "Speed : " + speed.ToString();

        var turnRotation = turnSpeed * hInput * Time.deltaTime * speed/100;
        transform.Rotate(0, turnRotation, 0);

    }

    private void FixedUpdate()
    {
        Vector3 tractionForce = Vector3.zero;
        if(vInput > 0)
        {
            tractionForce = transform.forward * (engineForce + (speed >= speedAdd ? engineForceAdd : 0));
        }
        if(vInput < 0)
        {
            tractionForce = -transform.forward * brakeForce;
        }
        Vector3 dragForce = -drag * velocity * velocity.magnitude;
        Vector3 rrForce = -rollingResistance * velocity;
        Vector3 force = tractionForce + dragForce + rrForce;

        acceleration = force / mass;
        velocity += Time.fixedDeltaTime * acceleration;

        velocity = velocity.magnitude < 0.1f ? Vector3.zero : velocity;

        rbSphere.velocity = velocity * Time.fixedDeltaTime;

        if (coll.isCollided)
        {
            velocity = Vector3.zero;
            coll.isCollided = false;
        }
    }
}
