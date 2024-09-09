using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] private Transform[] target;
    [SerializeField] public bool isStateInicial;
    [SerializeField] public bool isStateFinal;
    [SerializeField] public float up; // 0
    [SerializeField] public float bottom; // 1
    [SerializeField] public float left; // 2
    [SerializeField] public float right; // 3
    public int Aleatorio { get; set; }
    public float Reforco { get; set; }

    public State NovoEstado()
    {
        if (target == null || target.Length <= 0) 
        {
            return null;
        }
        while (true) 
        {

            Aleatorio = Random.Range(0, 4);
            if (target[Aleatorio]) 
            {
                return target[Aleatorio].GetComponent<State>();
            }
        } 
    }

    public float MaxReforco()
    {
        return Mathf.Max(up, bottom, left, right);
    }

    public float AcaoTomada()
    {
        float acao;
        if (Aleatorio == 0)
        {
            acao = up;
        }
        else if (Aleatorio == 1)
        {
            acao = bottom;
        }else if(Aleatorio == 2)
        {
            acao = left;
        }
        else
        {
            acao = right;
        }
        return acao;
    }

    public void AtualizarAcaoTomada(float novoValor)
    {
        if (Aleatorio == 0)
        {
            up = novoValor;
        }
        else if (Aleatorio == 1)
        {
            bottom = novoValor;
        }
        else if (Aleatorio == 2)
        {
            left = novoValor;
        }
        else
        {
            right = novoValor;
        }
    }


}
