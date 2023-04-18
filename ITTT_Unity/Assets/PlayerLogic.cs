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
    // Start is called before the first frame update
    void Start() {
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.transform.tag.Equals("Obstacle")) {
            if (!invincible) {
                health--;
                invincible = true;
            }
        }
    }
}
