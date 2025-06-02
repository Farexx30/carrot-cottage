using Godot;
using System;
using System.Collections.Generic;

namespace CarrotCottage.Scripts.StateMachine;

public partial class NodeStateMachine : Node
{
    [Export]
    public NodeState? InitialNodeState { get; set; }

    private readonly Dictionary<StringName, NodeState> _nodeStates = [];

    private NodeState? _currentNodeState;
    private StringName? _currentNodeStateName;

    private StringName _parentNodeName = default!;

    public override void _Ready()
    {
        _parentNodeName = GetParent().Name;

        foreach(var child in GetChildren())
        {
            if (child is NodeState nodeState)
            {
                _nodeStates[nodeState.Name] = nodeState;
                nodeState.Transition += TransitionTo;
            }
        }

        if (InitialNodeState is not null)
        {
            InitialNodeState.OnEnter();

            _currentNodeState = InitialNodeState;
            _currentNodeStateName = InitialNodeState.Name;
        }
    }

    public override void _Process(double delta)
    {
        _currentNodeState?.OnProcess(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        _currentNodeState?.OnPhysicsProcess(delta);
        _currentNodeState?.OnNextTransition();

        //GD.Print($"{_parentNodeName} current state name: {_currentNodeStateName}");
    }

    private void TransitionTo(StringName nodeStateName)
    {
        if (nodeStateName == _currentNodeStateName)
        {
            return;
        }

        var newNodeState = _nodeStates.GetValueOrDefault(nodeStateName);

        if (newNodeState is null)
        {
            return;
        }

        _currentNodeState?.OnExit();

        newNodeState.OnEnter();

        _currentNodeState = newNodeState;
        _currentNodeStateName = nodeStateName;

        //GD.Print($"Transitioned to state: {nodeStateName}");
    }
}
