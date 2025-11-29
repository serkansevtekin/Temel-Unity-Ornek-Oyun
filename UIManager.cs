using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text coinText;
    [SerializeField] GameObject PuzzleGamePanel;
    [SerializeField] GameObject GameOverPanel;

    public int puzzleCorrectCount;
    public static UIManager UIinstance;

    public bool PuzzleState = false;
    void Awake()
    {
        if (UIinstance != null && UIinstance != this)
        {
            Destroy(gameObject);
            return;

        }
        UIinstance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        coinText.text = "0";
        CloseGameOverPanel();
    }

    public void CoinTextCollectedCoinCount(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void PuzzleGamePanelOpen()
    {


        PuzzleState = true;
        puzzleCorrectCount = 0;
        PuzzleGamePanel.SetActive(true);
        PuzzleGamePanel.transform.localScale = Vector3.zero;
        PuzzleGamePanel.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack);

    }

    private void ColesPuzzleGamePanel()
    {
        PuzzleState = false;


        // Animasyon ile kapanış
        PuzzleGamePanel.transform
            .DOScale(0, 0.25f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                PuzzleGamePanel.SetActive(false);
            });
    }

    public void OnCorrectMath()
    {
        puzzleCorrectCount++;
        if (puzzleCorrectCount >= 4)
        {
            ColesPuzzleGamePanel();
        }
    }


    public void OpenGameOverPanel()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void CloseGameOverPanel()
    {
        GameOverPanel.SetActive(false);
        Time.timeScale = 1;


    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

       

        //DontDestroy Objeleri sıfırlamak için  
        if (UIManager.UIinstance != null)
        {
            Destroy(UIManager.UIinstance.gameObject);
        }
    }

}
