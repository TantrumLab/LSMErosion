﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Material m_highlightMaterial;

    public bool m_pickup;
    [SerializeField]
    private bool repickup;
    [HideInInspector]
    public bool m_repickup { get { return repickup; } }

    public UnityEngine.Events.UnityEvent m_onHighnight;
    public UnityEngine.Events.UnityEvent m_offHighnight;
    public UnityEngine.Events.UnityEvent m_onInteract;
    public UnityEngine.Events.UnityEvent m_offInteract;

    private Mouledoux.Components.Mediator.Subscriptions m_subscriptions = new Mouledoux.Components.Mediator.Subscriptions();

    protected Mouledoux.Callback.Callback onHighlight;
    protected Mouledoux.Callback.Callback offHighlight;
    protected Mouledoux.Callback.Callback onInteract;
    protected Mouledoux.Callback.Callback offInteract;

    private Renderer m_renderer;

    protected void Initialize(GameObject self)
    {
        m_renderer = GetComponentInChildren<Renderer>();

        onHighlight = OnHighlight;
        offHighlight = OffHighlight;

        onInteract = OnInteract;
        offInteract = OffInteract;

        m_subscriptions.Subscribe(self.GetInstanceID().ToString() + "->onhighlight", onHighlight);
        m_subscriptions.Subscribe(self.GetInstanceID().ToString() + "->offhighlight", offHighlight);

        m_subscriptions.Subscribe(self.GetInstanceID().ToString() + "->oninteract", onInteract);
        m_subscriptions.Subscribe(self.GetInstanceID().ToString() + "->offinteract", offInteract);
    }



    protected virtual void OnHighlight(Mouledoux.Callback.Packet packet)
    {
        m_onHighnight.Invoke();

        m_renderer.materials = new Material[] { m_renderer.materials[0], m_highlightMaterial };
    }

    protected virtual void OffHighlight(Mouledoux.Callback.Packet packet)
    {
        m_offHighnight.Invoke();

        m_renderer.materials = new Material[] { m_renderer.materials[0] };
    }


    protected virtual void OnInteract(Mouledoux.Callback.Packet packet)
    {
        m_onInteract.Invoke();
    }

    protected virtual void OffInteract(Mouledoux.Callback.Packet packet)
    {
        m_offInteract.Invoke();
    }
}