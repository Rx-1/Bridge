using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    static int goal;
    static int whalesSaved = 0;
    static bool crashed = false;
    static bool gameOver = false;

    static GameObject WCounter;

    public string victorySceneName;
    public string crashedSceneName;
    public int whalesToSave;
    public float restartTime = 10;

    float timer = 0;

    public GameObject whaleCounter;

    public static int WhalesSaved() {
        return whalesSaved;
    }

    public static int Goal() {
        return goal;
    }

    void Awake() {
        goal = whalesToSave = 10;
        whalesSaved = 0;
        crashed = false;
        gameOver = false;
        Time.timeScale = 1;
        WCounter = whaleCounter;
    }

    public static void SavedWhales() {
        whalesSaved++;
        if(whalesSaved >= goal) {
            gameOver = true;
            WCounter.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public static void Crashed() {
        crashed = true;
        gameOver = true;
        WCounter.SetActive(false);
        Time.timeScale = 0;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(gameOver) {
            timer += Time.unscaledDeltaTime;
            if (timer > restartTime) {
                if (crashed) {
                    Application.LoadLevel(crashedSceneName);
                } else {
                    Application.LoadLevel(victorySceneName);
                }
            }
        }
	}
}
