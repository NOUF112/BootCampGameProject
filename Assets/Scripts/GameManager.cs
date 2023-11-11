using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI jewelText;
    public TextMeshProUGUI winText;
    public Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

  
    public void updateJewelText(int jewel)
    {
        jewelText.text = "Jewels: "+jewel.ToString();
    }
    public void updateHealthSlider(int currentHealth , int maxHealth)
    {
        float newCurrentHealth = (float) currentHealth / maxHealth;
        healthSlider.value = newCurrentHealth;
    }
    public void showWinText()
    {
        winText.text = "You Won!";
    }

}
