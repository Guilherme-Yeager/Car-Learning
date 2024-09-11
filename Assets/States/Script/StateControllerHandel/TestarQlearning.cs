using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static StateController;
using static PlayerController;

public class TestarQlearning
{
    private readonly StateController controller;
    public bool podeTestar;
    public TestarQlearning(StateController controller)
    {
        this.controller = controller;
    }

    public void Testar()
    {
        if (instacePlayer.target)
        {
            return;
        }
        State s = instaceStateController.EstadoAtual.NovoEstadoInteligente();
        instacePlayer.Target(s.transform);
        instaceStateController.EstadoAtual = s;
        if (instaceStateController.EstadoAtual.isStateFinal)
        {
            instaceStateController.inicarTesteQlearningAction = null;
            controller.EstadoAtual = controller.AllStates.Where(a => a.isStateInicial).First();
            controller.BtTestar.interactable = true;
            controller.BtAprender.interactable = true;
        }
    }
}
