using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    public GameObject spawnObject;
    public Transform spawnLocation;
    [HideInInspector]
    public float constantSpawnTimer;
    [HideInInspector]
    public Vector2 randomSpawnTimers;
    [HideInInspector]
    public Vector2 escalateSpawnTimers;
    [HideInInspector]
    public int escalateTotalTime;
    [HideInInspector]
    public SpawnType spawnType;

    public enum SpawnType
    {
        Constant,
        Random,
        Escalate
    }

    private float randomCurrentSpawnTime;
    private float escalateRandomCurrentSpawnTime;
    private float escalateCurrentSpawnTime;
    private float escalteIncramentValue;

    private float currentTime;
    // Start is called before the first frame update
    void Start() {
        if (spawnType.Equals(SpawnType.Random)) {
            SetRandomSpawnTimer();
        }

        if (spawnType.Equals(SpawnType.Escalate)) {
            SetEscalteSpawnTimer();
            escalteIncramentValue = (escalateSpawnTimers.y - escalateSpawnTimers.x) / escalateTotalTime;
            escalateCurrentSpawnTime = escalateTotalTime;
            InvokeRepeating("EscalateReduceSpawnTimer", 0.1f, 1f);
        }
    }

    // Update is called once per frame
    void Update() {
        currentTime += Time.deltaTime;

        if (spawnType.Equals(SpawnType.Constant)) {
            if (currentTime >= constantSpawnTimer) {
                SpawnObject();
                currentTime = 0;
            }
        }

        if (spawnType.Equals(SpawnType.Random)) {
            if (currentTime >= randomCurrentSpawnTime) {
                SpawnObject();
                currentTime = 0;
                SetRandomSpawnTimer();
            }
        }

        if (spawnType.Equals(SpawnType.Escalate)) {
            if(currentTime >= escalateRandomCurrentSpawnTime) {
                currentTime = 0;
                SpawnObject();
                SetEscalteSpawnTimer();
            }
        }
    }

    private void SpawnObject() {
        GameObject currentObject = Instantiate(spawnObject);
        currentObject.transform.position = spawnLocation.position;
    }

    private void SetRandomSpawnTimer() {
        randomCurrentSpawnTime = Random.Range(randomSpawnTimers.x, randomSpawnTimers.y);
        Debug.Log(randomCurrentSpawnTime);
    }

    private void SetEscalteSpawnTimer() {
        escalateRandomCurrentSpawnTime = Random.Range(escalateCurrentSpawnTime * 0.9f, escalateCurrentSpawnTime * 1.1f);
    }

    private void EscalateReduceSpawnTimer() {
        Debug.Log("called");
        escalateCurrentSpawnTime -= escalteIncramentValue;
        Debug.Log(escalateCurrentSpawnTime);
        escalateTotalTime--;
        if(escalateTotalTime <= 0) {
            CancelInvoke();
        }
    }
}

[CustomEditor(typeof(Spawners))]
public class SpawnInspectorEditor : Editor
{
    Spawners spawn;

    private void OnEnable() {
        spawn = (Spawners)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        spawn.spawnType = (Spawners.SpawnType)EditorGUILayout.EnumPopup("SpawnType", spawn.spawnType);

        switch (spawn.spawnType) {
            case Spawners.SpawnType.Constant:
                spawn.constantSpawnTimer = EditorGUILayout.FloatField("Constant Spawn Value", spawn.constantSpawnTimer);
                break;

            case Spawners.SpawnType.Random:
                spawn.randomSpawnTimers = EditorGUILayout.Vector2Field("Random Spawn Value", spawn.randomSpawnTimers);
                break;

            case Spawners.SpawnType.Escalate:
                spawn.escalateSpawnTimers = EditorGUILayout.Vector2Field("Escalate Spawn Value", spawn.escalateSpawnTimers);
                spawn.escalateTotalTime = EditorGUILayout.IntField("Total Escalation Time", spawn.escalateTotalTime);
                break;
        }
    }
}
