using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static StateController;

public class State : MonoBehaviour
{
    
    [SerializeField] private Transform[] target;
    [SerializeField] public bool isStateInicial;
    [SerializeField] public bool isStateFinal;
    [SerializeField] public float up; // 0
    [SerializeField] public float bottom; // 1
    [SerializeField] public float left; // 2
    [SerializeField] public float right; // 3
    [SerializeField] private SpriteRenderer spriteRenderer;

    public int Aleatorio { get; set; }
    public float Reforco { get; set; }
    public Transform[] Target { get => target; set => target = value; }

    private void Start()
    {
        Target = target;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetEstadoInicial(bool isStateInicial)
    {
        this.isStateInicial = isStateInicial;
        if (isStateInicial)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    public void SetEstadoFinal(bool isStateFinal)
    {
        this.isStateFinal = isStateFinal;
        if (isStateFinal)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    public void SetEstadoAtual(bool isEstadoAtual)
    {
        if (isEstadoAtual)
        {
            spriteRenderer.color = Color.blue;
        }
        else
        {
            spriteRenderer.color = Color.white;
            if (isStateFinal) 
            {
                SetEstadoFinal(true);
            }
            if (isStateInicial)
            {
                SetEstadoInicial(true);
            }
        }
    }

    private void OnMouseDown()
    {
        if (!instaceStateController.BtTestar.interactable) 
        {
            return;
        }
        if (!isStateFinal) 
        {
            instaceStateController.AllStates.Where(a => a.isStateInicial).First().SetEstadoInicial(false);
            SetEstadoInicial(true);
            instaceStateController.EstadoAtual = this;
        }
    }

    public State NovoEstadoAleatorio()
    {
        if (target == null || target.Length <= 0) 
        {
            return null;
        }
        int cont = 0;
        while (true) 
        {
            if(cont > 10000)
            {
                Debug.Log("LOOP INFITO");
                return null;
            }
            cont++;
            Aleatorio = UnityEngine.Random.Range(0, 4);
            if (target[Aleatorio]) 
            {
                return target[Aleatorio].GetComponent<State>();
            }
        } 
    }

    public float MaxAcao()
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
    
    public State NovoEstadoInteligente()
    {
        float[] array = new float[4] { up, bottom, left, right };
        float max = array.Max();
        State s;
        if (max == 0)
        {
            s = NovoEstadoAleatorio();
        }
        else
        {
            s = Target[Array.IndexOf(array, max)].GetComponent<State>();
        } 
        return s;
    }
}
