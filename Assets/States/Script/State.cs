using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] private Transform[] target;
    [SerializeField] private bool isStateFinal;

    public GameObject NovoEstado()
    {
        if (target == null || target.Length <= 0) 
        {
            return null;
        }
        int random;
        while (true) 
        {
            random = Random.Range(0, 4);
            Debug.Log(random);
            if (target[random]) 
            {
                return target[random].gameObject;
            }
        } 
    }
    

}
