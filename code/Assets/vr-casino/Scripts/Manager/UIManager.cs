using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using System;
using Valve.VR.InteractionSystem;

[System.Serializable]
public class ScoreHistoryData
{
    public GameState EGameState;
    public int m_ChipCount;

    public ScoreHistoryData(GameState egameState, int chipCount)
    {
        EGameState = egameState;
        m_ChipCount = chipCount;
    }
}
public class UIManager : MonoBehaviour
{
    private Image _audioImage;
    private bool _isAudioDisabled;
    //private Sprite _audioOnSprite;

    //[SerializeField]
    //private TextMeshProUGUI _scoreText;
    //[SerializeField]
    //private Button _dealButton;
    //[SerializeField]
    //private Button _hitButton;
    //[SerializeField]
    //private Button _standButton;
    [SerializeField]
    private Button _newGameButton;
    [SerializeField]
    private Button _exitButton;
    [SerializeField]
    private Button _audioButton;
    [SerializeField]
    private AudioSource gameAudio;
    [SerializeField]
    private InputActionReference _debugButtonDeal, _debugButtonNewGame, _debugButtonHit;

    private List<ScoreHistoryData> m_lastThreeHistory = new List<ScoreHistoryData>();
    private int m_CurrentChipValueOnBettingHole;

    [SerializeField]
    private TextMeshProUGUI m_MessageBoard, HistoryText, m_CurrentBet;
    [SerializeField]
    private GameObject m_PlayerObj;
    [TextArea(15, 20)]
    public string InfoText;
    
    /*[SerializeField]
    private Sprite _audioOffSprite;*/

    public delegate void ButtonEvent();
    public event ButtonEvent OnDealButtonEvent = delegate { };
    public event ButtonEvent OnHitButtonEvent = delegate { };
    public event ButtonEvent OnStandButtonEvent = delegate { };
    public event ButtonEvent OnNewGameButtonEvent = delegate { };
    public event ButtonEvent OnExitButtonEvent = delegate { };

    public delegate bool ButtonEventWithBoolean();
    public event ButtonEventWithBoolean OnAudioButtonEvent = delegate() { return false; };

    private void Start()
    {
        //_dealButton.onClick.AddListener(() => OnDealButtonEvent());
        //_hitButton.onClick.AddListener(() => OnHitButtonEvent());
        //_standButton.onClick.AddListener(() => OnStandButtonEvent());
        _newGameButton.onClick.AddListener(() => OnNewGameButtonEvent());
        _exitButton.onClick.AddListener(() => OnExitButtonEvent());

        _audioButton.onClick.AddListener(OnAudioButtonClick);
        _audioImage = _audioButton.GetComponent<Image>();
        _debugButtonDeal.action.performed += DealButton;
        _debugButtonNewGame.action.performed += NewGameButton;
        _debugButtonHit.action.performed += HitGameButton;

        _isAudioDisabled = false;
        gameAudio.Play();
        //_audioOnSprite = _audioImage.sprite;

        //_scoreText =  gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void OnUpdateGameplayButtons(GameAction action)
    {
        //_dealButton.interactable = (action == GameAction.Deal);
        //_hitButton.interactable = (action == GameAction.HitAndStand);
        //_standButton.interactable = (action == GameAction.HitAndStand);
        _newGameButton.interactable = (action == GameAction.NewGame);
    }

    

    //public void UpdateScore(int humanScore, int computerScore)
    //{
    //    _scoreText.text = string.Format("Player: {0}\nComputer: {1}", humanScore, computerScore);
    //}

    private void OnAudioButtonClick()
    {
        //bool _isAudioDisabled = OnAudioButtonEvent();

        if (_isAudioDisabled)
        {
            gameAudio.Play();
            _isAudioDisabled = false;
        }
        else
        {
            gameAudio.Pause();
            _isAudioDisabled = true;
            //_audioImage.sprite = _audioOnSprite;
        }
    }

    public void DealButton(InputAction.CallbackContext context)
    {
        Debug.Log("Caalling");
        OnDealButtonEvent();
    }
    public void DealButton()
    {
        Debug.Log("Caalling");
        if (m_CurrentChipValueOnBettingHole > 0)
            OnDealButtonEvent();
        else
            OnNoCoinsOnDeal("Please Put some coins to deal");

    }

    public void NewGameButton(InputAction.CallbackContext context)
    {
        Debug.Log("NewGame");
        OnNewGameButtonEvent();
    }

    public void HitGameButton(InputAction.CallbackContext context)
    {
        Debug.Log("Hit");
        OnHitButtonEvent();
    }

    public void HitGameButton()
    {
        Debug.Log("Hit");
        OnHitButtonEvent();
    }

    public void StandButton()
    {
        Debug.Log("Stand");
        OnStandButtonEvent();
    }

    public void OnClickNewGame()
    {
        Destroy(m_PlayerObj);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void ShowResult(GameState gameState)
    {
        m_MessageBoard.text = gameState.ToString();
        StartCoroutine(ClearMessageBoard(5f));
    }

    public void UpdateHistoryAndResult(GameState gameState, int CurrentBet)
    {
        ShowResult(gameState);
        if (m_lastThreeHistory.Count > 3)
            m_lastThreeHistory.RemoveAt(m_lastThreeHistory.Count - 1);
        m_lastThreeHistory.Add(new ScoreHistoryData(gameState, CurrentBet));
        string TextToShow = "";
        foreach(var data in m_lastThreeHistory)
        {
            TextToShow = TextToShow + "You " + (data.EGameState == GameState.ComputerWon ? "Lose " : data.EGameState == GameState.HumanWon ? "Won " : "Draw ") + "By the coins worth of " + CurrentBet + "€\n\n";
        }
        HistoryText.text = TextToShow;
    }

    private void OnNoCoinsOnDeal(string Text)
    {
        m_MessageBoard.text = Text;
        StartCoroutine(ClearMessageBoard(5f));
    }

    public void OnClickInfoClose(GameObject button) 
    {
        button.SetActive(false);
        StartCoroutine(ClearMessageBoard(0f));
    }

    public void OnClickInfo(GameObject button)
    {
        m_MessageBoard.text = InfoText;
        button.SetActive(true);
    }
    public void OnBetUpdate(int Value)
    {
        m_CurrentChipValueOnBettingHole = Value;
        m_CurrentBet.text = "Current Bet Value  " + Value+"€";
    }
    private IEnumerator ClearMessageBoard(float Time)
    {
        yield return new WaitForSeconds(Time);
        m_MessageBoard.text = "";
    }
}