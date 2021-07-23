using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blackboard : MonoBehaviour {

    [SerializeField] private float timeOfDay;
    [SerializeField] private Text clock;
    [SerializeField] private Stack<GameObject> patrons = new Stack<GameObject>();
    [SerializeField] private int openTime = 6;
    [SerializeField] private int closeTime = 20;

    static Blackboard instance;
    public static Blackboard Instance {

        get {

            if (!instance) {

                Blackboard[] blackboards = FindObjectsOfType<Blackboard>();
                if (blackboards != null) {

                    if (blackboards.Length == 1) {

                        instance = blackboards[0];
                        return instance;
                    }
                }
                GameObject go = new GameObject("Blackboard", typeof(Blackboard));
                instance = go.GetComponent<Blackboard>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
        set {

            instance = value;
        }
    }

    public float TimeOfDay { get => timeOfDay; set => timeOfDay = value; }
    public int OpenTime { get => openTime; set => openTime = value; }
    public int CloseTime { get => closeTime; set => closeTime = value; }
    public Stack<GameObject> Patrons { get => patrons; set => patrons = value; }

    void Start() {

        StartCoroutine("UpdateClock");
    }

    IEnumerator UpdateClock() {

        while (true) {

            timeOfDay++;
            if (timeOfDay > 23) timeOfDay = 0;
            clock.text = timeOfDay.ToString("00") + ":00";

            if (timeOfDay == closeTime) {

                patrons.Clear();
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    public bool RegisterPatron(GameObject p) {


        patrons.Push(p);
        return true;
    }

    public void DeristerPatron() {

        // patron = null;
    }
}
