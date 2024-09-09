using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class StateController : MonoBehaviour
{
    [SerializeField] private State estadoAtual;
    [SerializeField] private PlayerController player;
    [SerializeField] private int cont;
    [SerializeField] private int maxCont;
    [SerializeField] private State estadoFinal;
    [SerializeField] private GameObject containerState;
    [SerializeField] private List<State> allStates;
    [SerializeField] private int epocas;
    private Action action;
    private const float alpha = 0.3f;
    private const float gama = 0.8f;

    private void Start()
    {
        estadoFinal.isStateFinal = true;
        estadoAtual.isStateInicial = true;
        estadoFinal.Reforco = 10;
        allStates = containerState.GetComponentsInChildren<State>().ToList();
        allStates = allStates.Where(s => s.isStateFinal == false).ToList();
        estadoAtual.GetComponent<SpriteRenderer>().color = Color.blue;
        player.Target(estadoAtual.transform);
        action = Wait;
    }

    public void Wait()
    {
        if (player.target != null)
        {
            return;
        }
        cont++;
        if (cont == maxCont)
        {
            action = null;
            cont = 0;
        }
    }

    public void Wait(State state)
    {
        if (player.target != null)
        {
            return;
        }
        cont++;
        if (cont == maxCont)
        {
            estadoAtual = allStates[UnityEngine.Random.Range(0, allStates.Count)]; // [0, 9)
            state.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            estadoAtual.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            player.Target(estadoAtual.transform);
            cont = 0;
            action = Wait;
        }
    }

    private void FixedUpdate()
    {
        if(epocas != 2)
        {
            if (action == null)
            {
                Qlearning();
            }
            else
            {
                action?.Invoke();
            }
        }

    }
    public void Qlearning() 
    {

        if (estadoAtual == null) 
        {
            return;
        }
        cont = 50;
        State state = estadoAtual.NovoEstado();

        float novoValor = alpha * (state.Reforco + (gama * state.MaxReforco()) - estadoAtual.AcaoTomada());
        estadoAtual.AtualizarAcaoTomada(novoValor);

        estadoAtual.GetComponent<SpriteRenderer>().color = Color.white;
        estadoAtual = state;
        estadoAtual.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        player.Target(state.transform);

        if (state.isStateFinal)
        {
            cont = 0;
            epocas++;
            action = () => Wait(state);
        }
        else
        {
            action = Wait;
        }
    }
}
