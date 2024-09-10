using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;



public class StateController : MonoBehaviour
{
    public static StateController instace;
    [SerializeField] private State estadoAtual;
    [SerializeField] private PlayerController player;
    [SerializeField] private int cont;
    [SerializeField] private int maxCont;
    [SerializeField] private State estadoFinal;
    [SerializeField] private GameObject containerState;
    [SerializeField] private List<State> allStates;
    [SerializeField] private int epocas;
    [SerializeField] private int estadoTesteInicial;

    public Action action, inicarQlearningAction;
    public bool podeClicar;

    public int Epocas { get => epocas; set => epocas = value; }
    public State EstadoAtual { get => estadoAtual; set => estadoAtual = value; }
    public State EstadoFinal { get => estadoFinal; }
    public PlayerController Player { get => player; }
    public int Cont { get => cont; set => cont = value; }
    public int MaxCont { get => maxCont; set => maxCont = value; }
    public GameObject ContainerState { get => containerState; }
    public List<State> AllStates { get => allStates; }
    private Qlearning q;
    private TestarQlearning t;



    private void Start()
    {
        q = new Qlearning(this);
        t = new TestarQlearning(this);
        instace = this;
        estadoFinal.isStateFinal = true;
        estadoAtual.isStateInicial = true;
        estadoFinal.Reforco = 10;
        allStates = containerState.GetComponentsInChildren<State>().ToList();
        allStates = allStates.Where(s => s.isStateFinal == false).ToList();
        estadoAtual.GetComponent<SpriteRenderer>().color = Color.blue;
        player.Target(estadoAtual.transform);
        action = q.Wait;
    }

    
    private void FixedUpdate()
    {
        inicarQlearningAction?.Invoke();
    }

    public void IniciarAprendizado()
    {
        q.IniciarAprendizado();
    }

    public void Testar()
    {

    }

}
