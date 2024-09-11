using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class StateController : MonoBehaviour
{
    public static StateController instaceStateController;
    [SerializeField] private State estadoAtual;
    [SerializeField] private PlayerController player;
    [SerializeField] private int cont;
    [SerializeField] private int maxCont;
    [SerializeField] private State estadoFinal;
    [SerializeField] private GameObject containerState;
    [SerializeField] private List<State> allStates;
    [SerializeField] private int epocas;
    [SerializeField] private int estadoTesteInicial;
    [SerializeField] private Button btAprender;
    [SerializeField] private Button btTestar;
    public Action action, inicarQlearningAction, inicarTesteQlearningAction;
    public int Epocas { get => epocas; set => epocas = value; }
    public State EstadoAtual { get => estadoAtual; set => estadoAtual = value; }
    public State EstadoFinal { get => estadoFinal; }
    public PlayerController Player { get => player; }
    public Button BtTestar { get => btTestar; }
    public Button BtAprender { get => btAprender; }
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
        estadoAtual.SetEstadoInicial(true);
        estadoFinal.SetEstadoFinal(true);
        estadoFinal.Reforco = 10;
        allStates = containerState.GetComponentsInChildren<State>().ToList();
        allStates = allStates.Where(s => s.isStateFinal == false).ToList();
        player.Target(estadoAtual.transform);
        action = q.Wait;
    }

    private void Awake()
    {
        instaceStateController = this;
    }

    private void FixedUpdate()
    {
        inicarQlearningAction?.Invoke();
        inicarTesteQlearningAction?.Invoke();
    }

    public void IniciarAprendizado()
    {
        BtAprender.interactable = false;
        BtTestar.interactable = false;
        q.IniciarAprendizado();
    }

    public void Testar()
    {
        player.Target(estadoAtual.transform); 
        inicarTesteQlearningAction = t.Testar;
        BtTestar.interactable = false;
        BtAprender.interactable = false;
    }

}
