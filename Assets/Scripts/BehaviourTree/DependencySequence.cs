using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DependencySequence : Node {
    BehaviourTree dependency;
    NavMeshAgent agent;
    public DependencySequence(string name, BehaviourTree dependency, NavMeshAgent agent) {
        base.name = name;
        this.dependency = dependency;
        this.agent = agent;
    }

    public override Status Process() {
        if (dependency.Process() == Status.FAILURE) {
            agent.ResetPath();
            // Reset all children
            foreach (Node n in children) {

                n.Reset();
            }
            return Status.FAILURE;
        }

        Status childStatus = children[currentChild].Process();
        if (childStatus == Status.RUNNING) return Status.RUNNING;
        if (childStatus == Status.FAILURE)
            return childStatus;

        currentChild++;
        if (currentChild >= children.Count) {
            currentChild = 0;
            return Status.SUCCESS;
        }

        return Status.RUNNING;
    }


}
