using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public enum Status { SUCCESS, RUNNING, FAILURE };
    public Status status;
    public List<Node> children = new List<Node>();
    public int currentChild = 0;
    public string name;
    public int sortOrder;

    public Node() { }

    public Node(string name) {
        this.name = name;
    }

    public Node(string name, int sortOrder) {
        this.name = name;
        this.sortOrder = sortOrder;
    }

    public void Reset() {

        foreach (Node n in children) {

            n.Reset();
        }
        currentChild = 0;
    }

    public virtual Status Process() {
        return children[currentChild].Process();
    }

    public void AddChild(Node n) {
        children.Add(n);
    }
}
