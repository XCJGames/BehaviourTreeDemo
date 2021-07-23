# Behaviour Tree Demo
Based on the H3DLearn course about implementing Artificial Intelligence with Behaviour Trees.

https://www.h3dlearn.com/course/btrees

Classes that are part of the Behaviour Tree are:

- Node: basic class that is extended by every type of node.
- Selector: it is like a logical OR, returns SUCCESS the first time a child returns SUCCESS and FAILURE only if every children return FAILURE.
- PrioritySelector: sorts children by a priority given to each child.
- RandomSelector: shuffles children before processing each.
- Sequence: it is like a logical AND, returns FAILURE the first time a child returns FAILURE and SUCCESS only if every children return SUCCESS.
- DependencySequence: checks a dependency behaviour tree each tick. If the check returns FAILURE, the rest of the sequence is not processed.
- Leaf: node that executes an action passed as a delegate.
- Inverter: returns the opposite value of a node.
- Loop: while a dependency behaviour tree or the node children return SUCCESS it keeps looping.
