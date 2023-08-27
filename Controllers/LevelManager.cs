using Controllers.Data;
using Levels;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Controllers
{
    public class LevelManager : MonoBehaviour
    {
        #region DELEGATE

        public delegate void LevelLoadHandler(Level levelData);

        public delegate void LevelStartHandler(Level levelData);

        public delegate void LevelStageCompleteHandler(Level levelData, int stageIndex = 0);

        public delegate void LevelCompleteHandler(Level levelData);

        public delegate void LevelFailHandler(Level levelData);

        #endregion

        #region EVENTS

        public static LevelLoadHandler OnLevelLoad;

        public static LevelStartHandler OnLevelStart;

        public static LevelStageCompleteHandler OnLevelStageComplete;

        public static LevelCompleteHandler OnLevelComplete;

        public static LevelFailHandler OnLevelFail;

        #endregion

        #region PUBLIC FIELDS / PROPS
        
        public static LevelManager Instance { get; private set; }

        #endregion

        #region SERIALIZE PRIVATE FIELDS

        [SerializeField] private LevelSource levelSource;
        [SerializeField] private GameObject levelSpawnPoint;
        [SerializeField] private int loopLevelsStartIndex = 1;
        [SerializeField] private bool loopLevelGetRandom = true; 
        
        [Header("Debugging")]
        [SerializeField] private bool debugMode; //Prevent Instantiating Levels
        [SerializeField] private GameObject debugLevel;

        #endregion

        #region PRIVATE FIELDS

        private GameObject _activeLevel;

        #endregion

        #region PRIVATE METHODS

        private void CheckRepeatLevelIndex()
        {
            if (loopLevelsStartIndex < levelSource.levelData.Length) return;
            loopLevelsStartIndex = 0;
        }

        private GameObject GetLevel()
        {
            GameObject Level = new GameObject();
            
            if (DataManager.Instance.LevelIndex >= levelSource.levelData.Length)
            {
                if (loopLevelGetRandom)
                {
                    DataManager.Instance.LevelIndex = Random.Range(loopLevelsStartIndex, levelSource.levelData.Length - 1);
                }
                else
                {
                    DataManager.Instance.LevelIndex = loopLevelsStartIndex;
                }
            }
            
            Level = levelSource.levelData[DataManager.Instance.LevelIndex];
            
            return Level;
        }

        #endregion

        #region PUBLIC METHODS
        public void LevelLoad()
        {
            _activeLevel = Instantiate(GetLevel(), levelSpawnPoint.transform, false);
            OnLevelLoad?.Invoke(_activeLevel.GetComponent<Level>());
        }
        
        public void LevelStart()
        {
            OnLevelStart?.Invoke(_activeLevel.GetComponent<Level>());
        }
        
        public void LevelStageComplete(int stageIndex = 0)
        {
            OnLevelStageComplete?.Invoke(_activeLevel.GetComponent<Level>(), stageIndex);
        }
        
        public void LevelComplete()
        {
            DataManager.Instance.Level++; //Next Level Number

            DataManager.Instance.LevelIndex++;//Next Level Index
            
            OnLevelComplete?.Invoke(_activeLevel.GetComponent<Level>());
        }
        
        public void LevelFail()
        {
            OnLevelFail?.Invoke(_activeLevel.GetComponent<Level>());
        }
        
        #endregion

        #region UNITY EVENT METHODS

        private void Awake()
        {
            CheckRepeatLevelIndex();
            Instance = this;
        }

        private void Start()
        { 
            
#if !UNITY_EDITOR
            debugMode=false;
#endif

            if (!debugMode)
            {
                LevelLoad();
            }
            else
            {
                _activeLevel = debugLevel;
            }
        } 

        #endregion
    }
}