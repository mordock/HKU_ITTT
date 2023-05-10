using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerLogic : MonoBehaviour
{
    public int health = 3;
    public float invincibleTime;
    public TextMeshProUGUI healthUI;

    private bool invincible;
    private float currentInvincibleTime;

    private TestConnection connection;
    // Start is called before the first frame update
    void Start() {
        connection = GameObject.Find("GameManager").GetComponent<TestConnection>();
    }

    // Update is called once per frame
    void Update() {
        healthUI.text = health.ToString();

        if (invincible) {
            currentInvincibleTime += Time.deltaTime;
            if (currentInvincibleTime >= invincibleTime) {
                invincible = false;
                currentInvincibleTime = 0;
            }
        }

        if(health <= 0) {
            connection.SendValueToArduino(health.ToString());
            connection.CloseStream();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.transform.tag.Equals("Obstacle")) {
            if (!invincible) {
                connection.SendValueToArduino(health.ToString());
                health--;
                invincible = true;
            }
        }
    }
}
