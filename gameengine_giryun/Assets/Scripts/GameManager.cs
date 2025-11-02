using UnityEngine;
using TMPro;  // TextMeshPro 사용을 위한 네임스페이스

public class GameManager : MonoBehaviour
{
    [Header("UI 참조")]
    public GameObject titleScreenPanel;
    public TextMeshProUGUI scoreText;  // 점수 텍스트 추가
    
    [Header("게임 상태")]
    private int score = 0;  // 현재 점수
    
    void Start()
    {
        ShowTitleScreen();
        UpdateScoreUI();  // 초기 점수 표시
    }
    
    void ShowTitleScreen()
    {
        titleScreenPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void StartGame()
    {
        titleScreenPanel.SetActive(false);
        Time.timeScale = 1f;
        score = 0;  // 게임 시작 시 점수 초기화
        UpdateScoreUI();
    }
    
    // 점수 증가 함수
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }
    
    // UI 업데이트 함수
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}