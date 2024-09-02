using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private PlayerController player;

    public void MudarPosicao() 
    {
        GameObject s = target.GetComponent<State>().NovoEstado();
        if (s != null)
        {
            target.GetComponent<SpriteRenderer>().color = Color.white;
            s.GetComponent<SpriteRenderer>().color = Color.blue;
            target = s;
            Debug.Log(s.name);
            player.Target(s.transform);
        }
    }
}
