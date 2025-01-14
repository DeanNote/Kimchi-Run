using System;
using TMPro;
using UnityEngine;

public enum GameState{
    Intro,
    Playing,
    Dead
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instatnce;

    public GameState State = GameState.Intro;

    public float PlayStartTime;

    public int Lives = 3;

    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldSpawner;

    public Player PlayerScript;

    public TMP_Text scoreText;

    void Awake(){
        if(Instatnce == null){
            Instatnce = this;
        }
    }
    void Start()
    {
        IntroUI.SetActive(true);
    }

    float CalculateScore(){
        return Time.time - PlayStartTime;
    }

  void SaveHighScore() // 현재까지의 최고 점수를 저장하는 함수.
    {
        int score = Mathf.FloorToInt(CalculateScore()); // 경과된 시간을 정수로 변환.
        //PlayerPrefs: 유니티에서 제공하는 데이터 저장소. (사용자의 컴퓨터 디스크에 데이터를 저장해주는 역할을 하는 클래스)
        int currentHighScore = PlayerPrefs.GetInt("highScore"); // 현재까지의 최고 점수를 디스크에서 가져옴.
        if(score > currentHighScore) // 현재 점수가 현재까지의 최고 점수보다 높으면
        {
            PlayerPrefs.SetInt("highScore", score); // 최고 점수를 현재까지의 점수로 변경.
            PlayerPrefs.Save(); // 변경된 최고 점수를 디스크에 저장.
        }
    }

    int GetHighScore() // 현재까지의 최고 점수를 반환하는 함수.
    {
        return PlayerPrefs.GetInt("highScore"); // 현재까지의 최고 점수를 반환.
    }
    public float CalculateGameSpeed(){
        if(State != GameState.Playing){
            return 5f;
        }
        float speed = 8f + (0.5f * Mathf.Floor(CalculateScore() / 10f));
        return Mathf.Min(speed, 30f);
    }

    void Update()
    {
        if(State == GameState.Playing){
            scoreText.text = "Score : " + Mathf.FloorToInt(CalculateScore());
        }else if(State == GameState.Dead){
            scoreText.text = "High Score : " + GetHighScore();
        }
        if(State == GameState.Intro && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Tab))){
            State = GameState.Playing;
            IntroUI.SetActive(false);
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldSpawner.SetActive(true);
            PlayStartTime = Time.time;
        }

        if(State == GameState.Playing && Lives == 0){
            PlayerScript.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldSpawner.SetActive(false);
            State = GameState.Dead;
            DeadUI.SetActive(true);
            SaveHighScore();
        }
        if(State == GameState.Dead && Input.GetKeyDown(KeyCode.Space)){
            UnityEngine.SceneManagement.SceneManager.LoadScene("main");
        }
    }
}
