using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieIo;
using ZombieIo.Input;
//using ZombieIo.Character.Skills;

public class GameManager : MonoBehaviour
{
    private ScoreManager scoreManager;
    [SerializeField] private CharacterFactory characterFactory;
    //[SerializeField] private WindowsService windowsService;
    //[SerializeField] private SkillService skillService;

    [Space, SerializeField]
    private GameData _gameData;

    private bool _isGameActive = false;
    private float _gameTimeSec = 0;
    private float _spawnEnemyTimeSec = 0;

    private float gameSessionTime;
    private float TimeBetweenEnemySpawn;
    public CharacterFactory CharacterFactory => characterFactory;
    //public WindowsService WindowsService =>
    //windowsService;
    public ScoreManager ScoreManager { get; private set; }

    public IInputService InputService { get; private set; }
    //public SkillService SkillService => skillService;
    //public SessionExperienceManager SessionExperienceManager { get; private set; }

    public float GameTime =>
        _gameTimeSec;



    public bool IsGameActive
    {
        get => _isGameActive;
        set => _isGameActive = value;
    }


    private bool isGameActive;
    [SerializeField] private GameData gameData;



    public static GameManager Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        scoreManager = new ScoreManager();
        isGameActive = false;

        characterFactory = FindObjectOfType<CharacterFactory>();
        if (characterFactory == null)
        {
            Debug.LogError("CharacterFactory не найден в сцене!");
        }
        //windowsService.Initialize();
    }

    public void StartGame()
    {
        if (characterFactory == null)
            Debug.LogError("CharacterFactory не найден!");

        Character player = characterFactory.CreateCharacter(CharacterType.DefaultPlayer);

        if (player != null)
        {
            player.transform.position = Vector3.zero;
            player.gameObject.SetActive(true);
            player.Initialize();
            player.HealthComponent.OnCharacterDeath += CharacterDeathHandler;

            gameSessionTime = 0;
            TimeBetweenEnemySpawn = gameData.SpawnEnemyTimeSec;


            isGameActive = true;
        }
        else
        {
            Debug.LogError("Не удалось создать персонажа!");
        }

        scoreManager.StartGame();
    }

    private void Update()
    {
        if (!isGameActive)
            return;

        gameSessionTime += Time.deltaTime;
        TimeBetweenEnemySpawn -= Time.deltaTime;

        if (TimeBetweenEnemySpawn < 0)
        {
            SpawnEnemy();
            TimeBetweenEnemySpawn = gameData.SpawnEnemyTimeSec;
        }

        if (gameSessionTime >= gameData.GameTimeSecondsMax)
        {
            GameVictory();
        }

    }

    private void CharacterDeathHandler(Character deathCharacter)
    {
        switch (deathCharacter.CharacterType) 
        {
            case CharacterType.DefaultPlayer:
                GameOver(); 
                break;

            case CharacterType.DefaultEnemy:
                scoreManager.CharacterDeathHandler(deathCharacter);
                break;

        }

        deathCharacter.gameObject.SetActive(false);
        characterFactory.ReturnToPool(deathCharacter);

        deathCharacter.HealthComponent.OnCharacterDeath -= CharacterDeathHandler;

    }
    private void SpawnEnemy()
    {
        Character enemy = characterFactory.CreateCharacter(CharacterType.DefaultEnemy);
        Vector3 playerPosition = characterFactory.PlayerCharacter.transform.position;
        enemy.transform.position = new Vector3(playerPosition.x + GetOffset(), 0, playerPosition.z + GetOffset());
        enemy.gameObject.SetActive(true);
        enemy.Initialize();
        enemy.HealthComponent.OnCharacterDeath += CharacterDeathHandler;


        float GetOffset()
        {
            bool isPlus = Random.Range(0, 100) % 2 == 0;
            float offset = Random.Range(gameData.MinEnemySpawnDistance, gameData.MaxEnemySpawnDistance);
            return (isPlus) ? offset : (-1 * offset);
        }
    }
    private void GameVictory()
    {
        scoreManager.CompleteMatch();

        Debug.Log("Victiry");
        isGameActive = false;
    }

    private void GameOver()
    {
        scoreManager.CompleteMatch();

        Debug.Log("Defeat");
        isGameActive = false;
    }

}

