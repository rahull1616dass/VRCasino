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
    [HideInInspector]
    public bool _isAudioEnabled;
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
    private GameObject _closeButton;
    [SerializeField]
    private Button _exitButton;
    [SerializeField]
    private Button _audioButton;
    [SerializeField]
    private AudioSource gameAudio, gameAudioOfBG;
    [SerializeField]
    private InputActionReference _debugButtonDeal, _debugButtonNewGame, _debugButtonHit;

    private List<ScoreHistoryData> m_lastThreeHistory = new List<ScoreHistoryData>();
    private int m_CurrentChipValueOnBettingHole;

    [SerializeField]
    private Image PanelImage;
    [SerializeField]
    private TextMeshProUGUI m_MessageBoard, HistoryText, m_CurrentBet, m_GameStateText;
    [SerializeField]
    private GameObject m_PlayerObj;
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private AudioClip buttonClip;
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
        OnClickInfo(_closeButton);
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

        _isAudioEnabled = false;
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

        if (_isAudioEnabled)
        {
            gameAudioOfBG.Play();
            _isAudioEnabled = false;
        }
        else
        {
            gameAudioOfBG.Pause();
            _isAudioEnabled = true;
            //_audioImage.sprite = _audioOnSprite;
        }
        ButtonSound();
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
        OnClickInfoClose(_closeButton);
        String text = "";
        switch (gameState)
        {
            case GameState.ComputerWon:
                {
                    text = "Dealer won!";
                    break;
                }
            case GameState.HumanWon:
                {
                    text = "You won!";
                    break;
                }
            case GameState.Draw:
                {
                    text = "The game was a draw!";
                    break;
                }

        }
        m_GameStateText.text = text;
        StartCoroutine(ClearGameStateText(5f));
    }

    public void UpdateHistoryAndResult(GameState gameState, int CurrentBet)
    {
        ShowResult(gameState);
        if (m_lastThreeHistory.Count > 3)
            m_lastThreeHistory.RemoveAt(m_lastThreeHistory.Count - 1);
        m_lastThreeHistory.Add(new ScoreHistoryData(gameState, CurrentBet));
        string TextToShow = "Round History\n\n";
        for(int i=0; i < m_lastThreeHistory.Count; i++)  
        {
            TextToShow =  TextToShow + "You " + (m_lastThreeHistory[i].EGameState == GameState.ComputerWon ? "Lost " : m_lastThreeHistory[i].EGameState == GameState.HumanWon ? "Won " : "Draw ") + ":" + m_lastThreeHistory[i].m_ChipCount + "€\n\n";
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
        PanelImage.enabled = false;
        StartCoroutine(ClearMessageBoard(0f));
        ButtonSound();
    }

    public void OnClickInfo(GameObject button)
    {
        m_MessageBoard.text = InfoText;
        button.SetActive(true);
        PanelImage.enabled = true;
        ButtonSound();
    }
    public void OnBetUpdate(int Value)
    {
        m_CurrentChipValueOnBettingHole = Value;
        m_CurrentBet.text = "Current Bet: " + Value+"€";
    }
    private IEnumerator ClearMessageBoard(float Time)
    {
        yield return new WaitForSeconds(Time);
        m_MessageBoard.text = "";
    }

    private IEnumerator ClearGameStateText(float Time)
    {
        yield return new WaitForSeconds(Time);
        m_GameStateText.text = "";
    }

    public void CardSound()
    {
        if (_isAudioEnabled)
            gameAudio.PlayOneShot(audioClips[UnityEngine.Random.Range(0, audioClips.Count)]);
    }

    public void ButtonSound()
    {
        if (_isAudioEnabled)
            gameAudio.PlayOneShot(buttonClip);
    }
}