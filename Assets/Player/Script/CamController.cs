using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        transform.position = target.position + new Vector3(0, 0, -15f);
    }
}
