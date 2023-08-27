using Controllers.Data;
using Levels;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers
{
    public class UIController : MonoBehaviour
    {
        #region PUBLIC PROPS
        public static UIController Instance { get; private set; }

        #endregion
        
        #region SERIALIZE FIELDS

        [Header("Panels")] 
        [SerializeField] private GameObject gameplayPanel;
        [SerializeField] private GameObject levelStartPanel;
        [SerializeField] private GameObject levelCompletePanel;
        [SerializeField] private GameObject levelFailPanel;
        [Space]
        [SerializeField] private GameObject tutorialArrow;
        [SerializeField] private GameObject nextButton;
        
        [Header("Settings Value")]
        [SerializeField] private int hideTutorialLevelIndex; //ToDo
        [SerializeField] private float levelCompletePanelShowDelayTime;
        [SerializeField] private float levelFailPanelShowDelayTime;
        
        #endregion
        
        #region PRIVATE FIELDS
        
        private void Initializer()
        {
            // Button Behaviors =>
            
            //Level Start
            levelStartPanel.SetActive(true);
            levelStartPanel.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                levelStartPanel.SetActive(false);
                LevelManager.Instance.LevelStart();
            });

            //Level Completed
            
            nextButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
            
            //Level Fail
            
            //TODO
            
        }
        
        private void ShowTutorial()
        {
            if (DataManager.Instance.Level > hideTutorialLevelIndex) return;
            
            tutorialArrow.SetActive(true);

            Invoke(nameof(HideTutorial), 3);
        }
        
        private void HideTutorial()
        {
            tutorialArrow.SetActive(false);
        }
        
        private void ShowLevelFailPanel()
        {
            if (tutorialArrow.activeSelf)
            {
                tutorialArrow.SetActive(false);
            }

            levelFailPanel.SetActive(true);
        }
        
        private void ShowLevelCompletePanel()
        {
            if (tutorialArrow.activeSelf)
            {
                tutorialArrow.SetActive(false);
            }

            nextButton.SetActive(false); //Delay Button Activation

            levelCompletePanel.SetActive(true);

            Invoke(nameof(DelayNextButton), 2f);
        }
        
        private void DelayNextButton()
        {
            nextButton.SetActive(true);
        }
        
        #endregion
        
        #region CUSTOM EVENTS

        private void OnLevelFail(Level levelData)
        {
            Invoke(nameof(ShowLevelFailPanel), levelFailPanelShowDelayTime);
        }

        private void OnLevelStart(Level levelData)
        {
            ShowTutorial();
            gameplayPanel.SetActive(true);
        }

        private void OnLevelComplete(Level levelData)
        {
            Invoke(nameof(ShowLevelCompletePanel), levelCompletePanelShowDelayTime);
        }

        private void OnLevelStageComplete(Level levelData, int stageIndex)
        {
            // TODO
        }

        #endregion
        
        #region UNITY EVENT METHODS

        private void Awake()
        {
            Initializer();

            if (Instance == null) Instance = this;
        }

        private void Start()
        {
            LevelManager.OnLevelStart += OnLevelStart;
            LevelManager.OnLevelComplete += OnLevelComplete;
            LevelManager.OnLevelFail += OnLevelFail;
            LevelManager.OnLevelStageComplete += OnLevelStageComplete;
        }


        private void OnDestroy()
        {
            LevelManager.OnLevelStart -= OnLevelStart;
            LevelManager.OnLevelComplete -= OnLevelComplete;
            LevelManager.OnLevelFail -= OnLevelFail;
            LevelManager.OnLevelStageComplete -= OnLevelStageComplete;
        }

        #endregion
    }
}

