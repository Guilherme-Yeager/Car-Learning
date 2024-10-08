using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instacePlayer;
    [SerializeField] private float speed;
    private Action action;
    public Transform target;
    private bool up;
    private bool bottom;
    private bool left;
    private bool right;
    private readonly float raio = 0.1f;

    private void Awake()
    {
        instacePlayer = this;
    }

    private void FixedUpdate()
    {
        Entrada();
        MovimentarCarroAutonomo();
        MovimentarCarro();
    }

    public void Target(Transform t)
    {
        target = t;
    }

    public void MovimentarCarroAutonomo()
    {
        
        if (target == null)
        {
            return;
        }
        if (Math.Round(Vector3.Distance(target.position, transform.position), 1) <= raio)
        {
            target = null;
            return;
        }
        if (Math.Abs(target.position.x - transform.position.x) >= raio)
        {
            if (target.position.x > transform.position.x)
            {
                right = true;
            }
            else
            {
                left = true;
            }
        }
        if (Math.Abs(target.position.y - transform.position.y) >= raio)
        {
            if (target.position.y > transform.position.y)
            {
                up = true;
            }
            else
            {
                bottom = true;
            }
        }
    }
    public void MovimentarCarro()
    {
        // Position - Ariana Grande?
        if (left && !right)
        {
            transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, 0);
        }
        if (right && !left)
        {
            transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, 0);
        }
        if (up && !bottom)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (speed * Time.deltaTime), 0);
        }
        if (bottom && !up)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (speed * Time.deltaTime), 0);
        }
        // Euler
        if (left && (!up && !right && !bottom))
        {
            transform.eulerAngles = new Vector3(0, 0, 90f);
        }
        else if (right && (!up && !left && !bottom))
        {
            transform.eulerAngles = new Vector3(0, 0, 270f);
        }
        else if (up && (!right && !left && !bottom))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (bottom && (!up && !left && !right))
        {
            transform.eulerAngles = new Vector3(0, 0, 180f);
        }
        else if (up && left && (!bottom && !right))
        {
            transform.eulerAngles = new Vector3(0, 0, 45f);
        }
        else if (up && right && (!left && !bottom))
        {
            transform.eulerAngles = new Vector3(0, 0, 315f);
        }
        else if (bottom && left && (!up && !right))
        {
            transform.eulerAngles = new Vector3(0, 0, 135f);
        }
        else if (bottom && right && (!left && !up))
        {
            transform.eulerAngles = new Vector3(0, 0, 225f);
        }
        up = false;
        bottom = false;
        left = false;
        right = false;
    }

    public void Entrada()
    {
        // Position - Ariana Grande?
        if (Input.GetKey(KeyCode.A))
        {
            left = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            right = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            up = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            bottom = true;
        }
    }

}
