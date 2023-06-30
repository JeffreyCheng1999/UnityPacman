using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }

    

    public GameObject GameItems;
    public bool IsGameStarted;
    public GameObject[] Lifes;
    public Text scoreText;
    
    public int highScore;
    public Text HighScoreTextHomeScreen;
    public Text HighScoreTextWinScreen;
    public Text HighScoreTextGamePlayScreen;
    public Text HighScoreTextRestartScreen;
    bool canResetTimer = false;
    public bool isGameWon;
    public bool isGamePaused;
    private void Awake()
    {
        instance = this;   
    }
    private void Start()
    {   
        highScore = PlayerPrefs.GetInt("HighScore",0);
        HighScoreTextGamePlayScreen.text = "HighScore : " + highScore;
        HighScoreTextHomeScreen.text = "HighScore : " + highScore;
        HighScoreTextWinScreen.text = "HighScore : " + highScore;
        HighScoreTextRestartScreen.text = "HighScore : " + highScore;
    }
    private void Update()
    {
        if (IsGameStarted) {
            GameItems.SetActive(true);
            if (this.lives <= 0 &&  IsGameStarted)
            {
                IsGameStarted = false;
                UIManager.instance.EnableGameOverScreen();
                SoundManager.instance.BackGroundVolume(1);
                SoundManager.instance.BackGroundSound(true);
            }

        }
    }
    //Set up a new game
    public void NewGame()
    {
        SetScore(0);
        SetLives(3);
        canResetTimer = true;
        NewRound();
    }

    //Reset all objects in every new round
    private void NewRound()
    {
        foreach (Transform pellets in this.pellets)
        {
            pellets.gameObject.SetActive(true);
        }
        ResetState();
    }
    private void ResetState()
    {
        ResetGhostMultiplier();

        //reset ghosts and pacman
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
            if (canResetTimer) {
                this.ghosts[i].movement.timer = 0;
                this.ghosts[i].movement.speed = 7;
                this.ghosts[i].movement.RevertColors();
            }
        }
        canResetTimer = false;
        this.pacman.ResetState();
        SoundManager.instance.RespawnVolume(1);
        SoundManager.instance.RespawnSound(true);
    }


    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
            this.ghosts[i].gameObject.SetActive(false);

        this.pacman.gameObject.SetActive(false);
    }


    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = "Score : " + score;
        if (score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            HighScoreTextGamePlayScreen.text = "HighScore : " + highScore;
            HighScoreTextHomeScreen.text = "HighScore : " + highScore;
            HighScoreTextWinScreen.text = "HighScore : " + highScore;   
            HighScoreTextRestartScreen.text = "HighScore : " + highScore;  
        }
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        if (this.lives == 3)
        {
            Lifes[2].SetActive(true);
            Lifes[1].SetActive(true);
            Lifes[0].SetActive(true);
        }else if (this.lives ==2){
            Lifes[2].SetActive(false);
            Lifes[1].SetActive(true);
            Lifes[0].SetActive(true);
        }
        else if (this.lives == 1)
        {
            Lifes[2].SetActive(false);
            Lifes[1].SetActive(false);
            Lifes[0].SetActive(true);
        }
        else if (this.lives == 0)
        {
            Lifes[2].SetActive(false);
            Lifes[1].SetActive(false);
            Lifes[0].SetActive(false);
        }
    }


    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + points);
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);

        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            //Sleep 3 second and allow other function to work in that period
            Invoke(nameof(ResetState), 3.0f);
            SoundManager.instance.CaughtVolume(1);
            SoundManager.instance.CaughtSound(true);
        }
        else
        {
            GameOver();
            SoundManager.instance.CaughtVolume(1);
            SoundManager.instance.CaughtSound(true);
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SoundManager.instance.DotCollectionVolume(1);
        SoundManager.instance.DotCollectionSound(true);

        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets())
        {
            //this.pacman.gameObject.SetActive(false);
            UIManager.instance.EnableWinScreen();
            SoundManager.instance.BackGroundVolume(1);
            SoundManager.instance.BackGroundSound(true);
            /*Invoke(nameof(NewRound), 3.0f);*/
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
        SoundManager.instance.FrightenModeVolume(1);
        SoundManager.instance.FrightenModeSound(true);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellets in this.pellets)
        {
            if (pellets.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }
}
