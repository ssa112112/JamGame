using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var input = Input.GetAxis("Jump");
        var velocity = rb.velocity;
        velocity = new Vector3(input, velocity.y, velocity.z);
        rb.velocity = velocity;
    }
}
