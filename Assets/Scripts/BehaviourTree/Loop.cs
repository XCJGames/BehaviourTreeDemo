using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : Node {

    BehaviourTree dependency;

    public Loop(string name, BehaviourTree dependency) {

        base.name = name;
        this.dependency = dependency;
    }

    public override Status Process() {

        if (dependency.Process() == Status.FAILURE) {

            return Status.SUCCESS;
        }

        Status childstatus = children[currentChild].Process();
        if (childstatus == Status.RUNNING) return Status.RUNNING;
        if (childstatus == Status.FAILURE) return childstatus;

        currentChild++;
        if (currentChild >= children.Count) {

            currentChild = 0;
        }

        return Status.RUNNING;
    }


}
