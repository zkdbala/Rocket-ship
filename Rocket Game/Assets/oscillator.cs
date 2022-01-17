using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movement = new Vector3(10f,10f,10f);
    [SerializeField] float period = 2f;
    float movementfactor;
    Vector3 startingpos;
    // Start is called before the first frame update
    void Start()
    {
        startingpos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; //grows continually from 0
        const float tau = Mathf.PI*2;
        float sinewave = Mathf.Sin(cycles * tau);
        movementfactor = sinewave / 2f + 0.5f ;
        Vector3 offset = movement * movementfactor;
        transform.position = offset + startingpos;
        
    }
}
