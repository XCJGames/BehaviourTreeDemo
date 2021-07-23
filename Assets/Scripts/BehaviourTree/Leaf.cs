using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
    public delegate Status Tick();
    public Tick ProcessMethod;

    public delegate Status TickMultiple(int val);
    public TickMultiple ProcessMethodMultiple;

    public int index;

    public Leaf() { }

    public Leaf(string name, Tick processMethod)
    {
        base.name = name;
        ProcessMethod = processMethod;
    }

    public Leaf(string name, int index, TickMultiple processMethod)
    {
        base.name = name;
        ProcessMethodMultiple = processMethod;
        this.index = index;
    }

    public Leaf(string name, Tick processMethod, int sortOrder)
    {
        base.name = name;
        ProcessMethod = processMethod;
        base.sortOrder = sortOrder;
    }

    public override Status Process()
    {
        Node.Status s;
        if(ProcessMethod != null)
            s = ProcessMethod();
        else if (ProcessMethodMultiple != null)
            s = ProcessMethodMultiple(index);
        else
            s = Status.FAILURE;

        Debug.Log(name + " " + s);
        return s;
    }

}
