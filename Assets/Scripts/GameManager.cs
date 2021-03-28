using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton Instance
    public static GameManager instance {
        get {
            if (managerInstance == null)
            {
                GameManager manager = FindObjectOfType<GameManager>();
                if (manager == null)
                {
                    Debug.LogError("No instance of GameManager found in scene");
                }
                managerInstance = manager;
            }
            return managerInstance;
        }
    }

    private static GameManager managerInstance; // for singleton

    // Calculates the number of enemies to spawn for the given wave
    private static int CalculateNumberOfEnemies(int waveNumber)
    {
        return 10 + (waveNumber * 5);
    }

    public enum GameState
    {
        STARTING,
        WAVE,
        BREAK,
        VICTORY,
        LOSS
    }

    // Public instance fields
    public int maxWaveNumber = 5;
    public float startDelay = 5f;
    public float interWaveDelay = 10f;

    // Instance fields
    private InGameUI ui;
    private EndGameScreen endGameScreen;
    private PauseMenu pauseMenu;
    private GameState state;
    private int waveNumber;
    private float timerRemaining;
    private int totalEnemies;
    private int enemiesRemainingToSpawn;
    private int enemiesRemaining;

    // Gets whether the game has ended
    public bool IsEnd {
        get => this.state == GameState.VICTORY
            || this.state == GameState.LOSS;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Obtain a reference to the UI
        this.ui = FindObjectOfType<InGameUI>();
        if (this.ui == null)
        {
            Debug.LogError("No instance of InGameUI found in scene");
        }

        // Obtain a reference to the end game screen
        this.endGameScreen = FindObjectOfType<EndGameScreen>();
        if (this.ui == null)
        {
            Debug.LogError("No instance of EndGameScreen found in scene");
        }

        // Obtain a reference to the pause menu
        this.pauseMenu = FindObjectOfType<PauseMenu>();
        if (this.ui == null)
        {
            Debug.LogError("No instance of PauseMenu found in scene");
        }

        // Set the game to starting and wait 5 seconds before spawning enemies
        this.state = GameState.STARTING;
        this.waveNumber = 0;
        this.timerRemaining = 5.0f;
        this.UpdateUI();
    }

    // Update is called every frame
    void Update()
    {
        switch (this.state)
        {
            case GameState.STARTING:
            case GameState.BREAK:
                this.timerRemaining -= Time.deltaTime;
                if (this.timerRemaining <= 0f)
                {
                    // Start the next wave
                    this.NextWave();
                }
                break;
            case GameState.WAVE:
                // Check for the wave being over
                if (this.enemiesRemaining <= 0)
                {
                    this.AfterWave();
                }
                break;
            case GameState.VICTORY:
            case GameState.LOSS:
                // Do nothing
                break;
        }
    }

    // Starts the next wave
    void NextWave()
    {
        this.state = GameState.WAVE;
        this.waveNumber += 1;
        this.totalEnemies = CalculateNumberOfEnemies(this.waveNumber);
        this.enemiesRemainingToSpawn = this.totalEnemies;
        this.enemiesRemaining = this.totalEnemies;
        this.UpdateUI();
    }

    // Starts the inter-wave delay,
    // or moves to victory if the wave number is the max
    void AfterWave()
    {
        if (this.waveNumber >= this.maxWaveNumber)
        {
            this.WinGame();
        }
        else
        {
            this.state = GameState.BREAK;
            this.timerRemaining = this.interWaveDelay;
            this.UpdateUI();
        }
    }

    // Updates the in-game UI to reflect the current objective
    void UpdateUI()
    {
        switch (this.state)
        {
            case GameState.STARTING:
                this.ui.SetObjective("Zombies approaching", "Prepare for incoming waves", "");
                break;
            case GameState.WAVE:
                string primaryObjective = this.waveNumber == this.maxWaveNumber
                    ? "Final wave"
                    : string.Format("Wave {0} / {1}", this.waveNumber, this.maxWaveNumber);
                string tertiaryObjective = string.Format("{0} Remaining", this.enemiesRemaining);
                this.ui.SetObjective(primaryObjective, "Eliminate all zombies", tertiaryObjective);
                break;
            case GameState.BREAK:
                this.ui.SetObjective("Brief respite", "Prepare for the next wave", "");
                break;
        }
    }

    // Called each time the spawner attempts to spawn an enemy
    public bool AttemptEnemySpawn()
    {
        if (this.state == GameState.WAVE && this.enemiesRemainingToSpawn > 0)
        {
            this.enemiesRemainingToSpawn--;
            return true;
        }

        return false;
    }

    // Called every time an enemy is killed
    public void OnEnemyDeath()
    {
        if (this.state == GameState.WAVE)
        {
            this.enemiesRemaining--;
            this.UpdateUI();
        }
    }

    // Sets the game state to victory and shows the end game screen
    void WinGame()
    {
        this.state = GameState.VICTORY;
        this.pauseMenu.Disable();
        this.endGameScreen.ShowVictory();
    }

    // Sets the game state to loss and shows the end game screen
    public void LoseGame()
    {
        this.state = GameState.LOSS;
        this.pauseMenu.Disable();
        this.endGameScreen.ShowLoss();
    }
}
