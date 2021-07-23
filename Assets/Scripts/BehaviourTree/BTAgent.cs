using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTAgent : MonoBehaviour {
    [SerializeField] protected BehaviourTree tree;
    [SerializeField] protected NavMeshAgent agent;

    public enum ActionState { IDLE, WORKING };
    [SerializeField] protected ActionState state = ActionState.IDLE;

    private Node.Status treeStatus = Node.Status.RUNNING;

    [SerializeField] private WaitForSeconds waitForSeconds;
    private Vector3 rememberedLocation;

    // Start is called before the first frame update
    public virtual void Start() {
        agent = GetComponent<NavMeshAgent>();
        tree = new BehaviourTree();
        waitForSeconds = new WaitForSeconds(Random.Range(0.1f, 1f));
        StartCoroutine(Behave());
    }

    public Node.Status CanSee(Vector3 target, string tag, float distance, float maxAngle) {
        Vector3 directionToTarget = target - transform.position;
        float angle = Vector3.Angle(directionToTarget, transform.forward);

        if (angle <= maxAngle || directionToTarget.magnitude <= distance) {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, directionToTarget, out hitInfo)) {
                if (hitInfo.collider.gameObject.CompareTag(tag)) {
                    return Node.Status.SUCCESS;
                }
            }
        }
        return Node.Status.FAILURE;
    }

    public Node.Status IsOpen() {

        if (Blackboard.Instance.TimeOfDay < Blackboard.Instance.OpenTime || Blackboard.Instance.TimeOfDay > Blackboard.Instance.CloseTime) {

            return Node.Status.FAILURE;
        }

        return Node.Status.SUCCESS;
    }

    public Node.Status Flee(Vector3 location, float distance) {
        if (state == ActionState.IDLE) {
            rememberedLocation = transform.position + (transform.position - location).normalized * distance;
        }
        return GoToLocation(rememberedLocation);
    }

    public Node.Status GoToLocation(Vector3 destination) {
        float distanceToTarget = Vector3.Distance(destination, transform.position);
        if (state == ActionState.IDLE) {
            agent.SetDestination(destination);
            state = ActionState.WORKING;
        } else if (Vector3.Distance(agent.pathEndPosition, destination) >= 2) {
            state = ActionState.IDLE;
            return Node.Status.FAILURE;
        } else if (distanceToTarget < 2) {
            state = ActionState.IDLE;
            return Node.Status.SUCCESS;
        }
        return Node.Status.RUNNING;
    }

    public Node.Status GoToDoor(GameObject door) {
        Node.Status s = GoToLocation(door.transform.position);
        if (s == Node.Status.SUCCESS) {
            if (!door.GetComponent<Lock>().IsLocked) {
                door.GetComponent<NavMeshObstacle>().enabled = false;
                return Node.Status.SUCCESS;
            }
            return Node.Status.FAILURE;
        } else
            return s;
    }

    IEnumerator Behave() {
        while (true) {
            treeStatus = tree.Process();
            yield return waitForSeconds;
        }
    }
}
