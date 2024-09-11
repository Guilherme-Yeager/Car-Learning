using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Qlearning
{
    private readonly StateController controller;
    private const float alpha = 0.3f;
    private const float gama = 0.8f;
    private const int maxEpocas = 12;

    public Qlearning(StateController controller)
    {
        this.controller = controller;
    }

    public void Wait()
    {
        if (controller.Player.target != null)
        {
            return;
        }
        controller.Cont++;
        if (controller.Cont >= controller.MaxCont)
        {
            controller.action = null;
            controller.Cont = 0;
        }
    }

    public void Wait(State state)
    {
        if (controller.Player.target != null)
        {
            return;
        }
        controller.Cont++;
        if (controller.Cont >= controller.MaxCont)
        {
            controller.EstadoAtual = controller.AllStates[UnityEngine.Random.Range(0, controller.AllStates.Count)]; // [0, 9)
            state.SetEstadoAtual(false);
            controller.EstadoAtual.SetEstadoAtual(true);
            controller.Player.Target(controller.EstadoAtual.transform);
            controller.Cont = 0;
            controller.action = Wait;
        }
    }

    public void IniciarAprendizado()
    {
        controller.inicarQlearningAction = IniciarQlearning;
        controller.Epocas = 0;
    }

    public void IniciarQlearning()
    {
        if (controller.Epocas != maxEpocas)
        {
            if (controller.action == null)
            {
                Aprender();
            }
            else
            {
                controller.action?.Invoke();
            }
        }
        else
        {
            controller.inicarQlearningAction = null;
        }
    }

    public void Aprender()
    {

        if (controller.EstadoAtual == null)
        {
            return;
        }
        controller.Cont = 50;
        State state = controller.EstadoAtual.NovoEstadoAleatorio();
        float novoValor = alpha * (state.Reforco + (gama * state.MaxAcao()) - controller.EstadoAtual.AcaoTomada());
        controller.EstadoAtual.AtualizarAcaoTomada(novoValor);
        controller.EstadoAtual.SetEstadoAtual(false);
        controller.EstadoAtual = state;
        controller.EstadoAtual.SetEstadoAtual(true);
        controller.Player.Target(state.transform);
        if (state.isStateFinal)
        {
            controller.Cont = 0;
            controller.Epocas++;
            if (controller.Epocas == maxEpocas)
            {
                Xml xml = new Xml();
                xml.SalvarTabelaQ(controller.ContainerState.GetComponentsInChildren<State>().ToList());
                controller.EstadoAtual = controller.AllStates.Where(a => a.isStateInicial).First();
                controller.BtTestar.interactable = true;
                controller.BtAprender.interactable = true;
            }
            controller.action = () => Wait(state);
        }
        else
        {
            controller.action = Wait;
        }
    }



}
