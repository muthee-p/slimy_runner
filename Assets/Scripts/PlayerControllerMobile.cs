using System.Collections;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerMobile : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel, startPanel, pauseMenu, transitionPanel, creditsPanel, finishedPanel;
    [SerializeField] TextMeshProUGUI distanceText, gameOverDistance, finishedDistancetxt, totalDistanceTxt, takesTxt, instructionsTxt;
    [SerializeField] GameObject dust, fall, heart;
    [SerializeField] GameObject bettle, vulture, dog, dino,bettleChild, vultureChild;
    [SerializeField] Button startPositionBtn, checkPoint1Btn, checkPoint2Btn, checkPoint3Btn;
    [SerializeField] GameObject[] checkpointImages;
    [SerializeField] GameObject fireworks;
    [SerializeField] GameObject[] waters;
    [SerializeField] Sprite flat, original, fullHeart, brokenHeart;
    public float speed;
    public float jumpHeight;
    public AudioSource movement, sheep, drop, squish, honk, slidingSound, squeak,woohoo;
    private float moveInput;
    public CameraShake cameraShake;
    private Rigidbody _rb;
    public float acceleration;
    public float accelerationTime;
    public bool isGrounded, isJumping;
    public float checkRadius;
    public LayerMask Ground;
    public int extraJumps;
    public float jumpTime;
    public float jumpSpeed;
    public int extraJumpsValue;
    public float jumpBufferTime = 0.1f;
    public float addedSpeed;
    private float jumpBufferCounter;
    private float jumpTimeCounter;
    private bool isGameStarted = false;
    float distanceTraveled;
    public float slopeAdjustmentSpeed = 5f;
    private Rigidbody bettleRb, vultureRb, dogRb, dinoRb;
    private Vector3 bettlePos, vulturePos, dogPos, dinoPos, bettleChildPos, vultureChildPos;
    private Vector3 startPosition , checkPoint1, checkPoint2, checkPoint3, currentCheckPoint;
    private float acc, accTime, speedInternal;
    bool gameOver = false;
    bool restoringLives=false;
    bool gamePaused = false;
    bool speedAdjusted = false;
    int Takes;
    int hitCount=0;
    public int noOfLives;
        int lives;
    float totalDistance;
    private const string CheckpointKey = "Checkpoint";
    private const string TotalDistanceKey = "TotalDistance";
    private const string TakesKey = "Takes";
    private const string SpeedKey = "Speed";

    void Start()
    {
       
        if (Application.isMobilePlatform)
        {
            instructionsTxt.text = "Tap Start, Space key to Jump & avoid obstacles";
        }
        else
        {
            instructionsTxt.text = "Tap Start & Jump to avoid obstacles";
        }
        _rb = GetComponent<Rigidbody>();
        extraJumps = extraJumpsValue;
        acc = acceleration;
        accTime = accelerationTime;
        speedInternal=8;
        lives = noOfLives;

        _rb.isKinematic = true;
        bettleRb = bettle.GetComponent<Rigidbody>();
        vultureRb = vulture.GetComponent<Rigidbody>();
        dogRb = dog.GetComponent<Rigidbody>();
        dinoRb = dog.GetComponent<Rigidbody>();
        bettlePos= bettle.GetComponent<Transform>().position;
        vulturePos = vulture.GetComponent<Transform>().position;
        dogPos = dog.GetComponent<Transform>().position;
        dinoPos = dino.GetComponent<Transform>().position;
        bettleChildPos = bettleChild.GetComponent<Transform>().position;
        vultureChildPos = vultureChild.GetComponent<Transform>().position;
        startPosition = new Vector3(31f, 944f, 31f);
        checkPoint1 = new Vector3(800f, 763f, 31f);
        checkPoint2 = new Vector3(1600f,614f, 31f);
        checkPoint3 = new Vector3(2400f,351f,31f);
        gameOver = false;

        LoadGame();
    }

    void OnApplicationQuit()
    {
        SaveGame(); 
    }
    void FixedUpdate()
    {
        if (isGameStarted)
        { 
            float playerX = _rb.position.x;
            isGrounded = Physics.Raycast(transform.position, Vector3.down, checkRadius, Ground);
            Debug.DrawRay(transform.position, Vector3.down * checkRadius, isGrounded ? Color.green : Color.red);

            moveInput = 1;
            _rb.velocity = new Vector3(Mathf.MoveTowards(_rb.velocity.x, moveInput * speed, acceleration * accelerationTime), _rb.velocity.y, _rb.velocity.z);
            
            AdjustRotationToSlope();

            if (playerX >= 800 && playerX <= 805) { speedAdjusted = false; woohoo.Play(); }
            if (playerX >= 1600 && playerX <= 1605){ speedAdjusted = false; woohoo.Play();}
            if (playerX >= 2400 && playerX <= 2405){ speedAdjusted = false; woohoo.Play();}

            if ( playerX < 800){
                currentCheckPoint = startPosition;
             }

            if(playerX>= 800 && playerX<1600)
            {
                checkPoint1Btn.interactable = true;
                currentCheckPoint = checkPoint1;
                if (!speedAdjusted)
                {
                    CheckPointSpeeds();
                }
            }
            if(playerX >=1600 && playerX < 2400)
            {
                checkPoint2Btn.interactable = true;
                checkPoint1Btn.interactable = true;
                currentCheckPoint = checkPoint2;
                if (!speedAdjusted)
                {
                    CheckPointSpeeds();
                }
            }
            if (playerX >= 2400)
            {
                checkPoint2Btn.interactable = true;
                checkPoint1Btn.interactable = true;
                checkPoint3Btn.interactable = true;
                currentCheckPoint = checkPoint3;
                if (!speedAdjusted)
                {
                    CheckPointSpeeds();
                }
            }
            if(playerX == 3100)
            {
                speed /= addedSpeed;
            }
            distanceTraveled = Vector3.Distance(currentCheckPoint, transform.position);
            distanceText.text = distanceTraveled.ToString("F2") + "m";
        }
    }

    void Update()
    {
        if (isGameStarted)
        {
            if (isGrounded)
            {
                extraJumps = extraJumpsValue;
                fall.SetActive(true); 
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
            {
                jumpBufferCounter = jumpBufferTime; 
            }

            if (jumpBufferCounter > 0)
            {
                jumpBufferCounter -= Time.deltaTime;

                if (isGrounded)
                {
                    _rb.velocity = new Vector3(_rb.velocity.x, jumpHeight * jumpSpeed, _rb.velocity.z);
                    extraJumps--;
                    isJumping = true;
                    jumpTimeCounter = jumpTime;
                    jumpBufferCounter = 0; 
                }
            }

            if ((Input.GetKey(KeyCode.Space) || Input.touchCount > 0) && isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    _rb.velocity = new Vector3(_rb.velocity.x, jumpHeight * jumpSpeed, _rb.velocity.z);
                    movement.Stop();
                    dust.SetActive(true);
                    fall.SetActive(false);
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                    dust.SetActive(false);
                }
            }

            if (Input.GetKeyUp(KeyCode.Space) || Input.touchCount == 0)
            {
                isJumping = false;
                dust.SetActive(false);
            }

            _rb.velocity += new Vector3(Vector2.up.x, Vector2.up.y, 0) * Physics.gravity.y * (jumpSpeed - 1) * Time.deltaTime;

            if (lives <= 3)
            {
                Image img = heart.GetComponent<Image>();
                if (lives == 3)
                {
                    img.sprite = fullHeart;
                }
                if (lives == 2)
                {
                    heart.SetActive(true);
                    img.sprite = brokenHeart;
                }
                if (lives == 1)
                {
                    heart.SetActive(false);
                }

                if (restoringLives == false)
                {
                    restoringLives = true;
                    Invoke("LivesReset", 40f);
                }
                else
                {

                    hitCount++;
                }
                if (lives == 0)
                {
                    gameOver = true;
                    movement.Stop();

                    transitionPanel.SetActive(true);
                    Invoke("CloseTransition", 1.5f);
                    Invoke("GameOver", 1.6f);
                    _rb.isKinematic = true;
                }
            }
            else return;
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (gameOver==false)
        {
            if (collision.gameObject.CompareTag("obstacle"))
            {
                GameObject obstacle = collision.gameObject;
                
                if (lives > 0)
                {
                    lives--;
                    speed --;
                    obstacle.GetComponent<Collider>().enabled = false;
                    if (obstacle != null && obstacle.name == "water") { Invoke("EnableWaterCollision", 3f); }
                    else { StartCoroutine(ResetCollider(obstacle, 1.5f)); }
                }
              
                if (obstacle.name == "sheep")
                {
                    sheep.Play();
                }
                if (obstacle.name == "water")
                {
                    drop.Play();
                }
                if (obstacle.name == "truck")
                {
                    honk.Play();
                }

                squish.Play();
                cameraShake.ShakeCamera();
                Invoke("StopShake", 1);

            }
            if (collision.gameObject.CompareTag("enemy"))
            {
                GameObject enemy = collision.gameObject;
                cameraShake.ShakeCamera();
                Invoke("StopShake", 1);
                gameOver = true;
                movement.Stop();
                squeak.Play();

                lives = 0;
                heart.SetActive(false);

                transitionPanel.SetActive(true);
                Invoke("CloseTransition", 1.5f);
                Invoke("GameOver", 1.6f);
                _rb.isKinematic = true;
                if(enemy==dino|| enemy == dog)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = flat;
                    gameObject.GetComponent<Animator>().enabled = false;
                }
            }
                
            if (collision.gameObject.CompareTag("platform"))
            {
                woohoo.Play();
                speed += 4f;
            }
            if (collision.gameObject.CompareTag("terrain"))
            {
                
                fall.SetActive(true);
                movement.Play();

            }
            if (collision.gameObject.CompareTag("wall"))
            {
                cameraShake.ShakeCamera();
                Invoke("StopShake", 1);
                gameOver = true;
               
                Invoke("FinishedGame", 2f);
            }
        }
       
    }
    
    IEnumerator ResetCollider(GameObject obstacle,float delay)
    {
        yield return new WaitForSeconds(delay);
        obstacle.GetComponent<Collider>().enabled=true;
        speed += 1;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
           
            slidingSound.Stop();
            movement.Play();
            Invoke("ResetSpeed", 1.5f);
        }
       
    }
    void ResetSpeed() { speed -= 4f; }

    void LivesReset()
    {
        if (!gamePaused && lives < 3)
        {
            lives++;
           
            if (hitCount > 0 )
            {
                hitCount--;
                Invoke("LivesReset", 40f);
            }
            else if(hitCount == 0)
            {
                restoringLives = false;
            }
        }
       
    }
   
    void EnableWaterCollision()
    {
        foreach (var water in waters)
        {
            water.GetComponent<Collider>().enabled = true;
        }
          
    }
    private void AdjustRotationToSlope()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, checkRadius, Ground))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            Vector3 eulerRotation = targetRotation.eulerAngles;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, eulerRotation.z), slopeAdjustmentSpeed * Time.deltaTime);

        }
    }

    void StopShake()
    {
        cameraShake.StopShake();
    }

    public void StartGame()
    {
        restoringLives = false;
        lives = noOfLives;
        heart.SetActive(true);
        heart.GetComponent<Image>().sprite = fullHeart;
        EnableWaterCollision();
        transitionPanel.SetActive(false);
        isGameStarted = true;
        _rb.isKinematic = false;
        currentCheckPoint = startPosition;
        startPanel.SetActive(false);
        movement.Play();
        ShowImages();
        Takes++;
        SaveGame();
        squeak.Stop();
        gameObject.GetComponent<SpriteRenderer>().sprite = original;
        gameObject.GetComponent<Animator>().enabled = true;
        CheckPointSpeeds();
    }
    public void RestartGame()
    {
        restoringLives = false;
        squeak.Stop();
        lives = noOfLives;
        heart.SetActive(true);
        heart.GetComponent<Image>().sprite = fullHeart;
        
        pauseMenu.SetActive(false);
        transitionPanel.SetActive(true);
        Invoke("CloseTransition", 1.5f);
        ShowImages();
        finishedPanel.SetActive(false);
        isGameStarted = true;
        gameObject.transform.position = currentCheckPoint;
        CheckPointSpeeds();
        gameOverPanel.SetActive(false);
        _rb.isKinematic = false;
        movement.Play();

        bettle.transform.position = bettlePos;
        vulture.transform.position = vulturePos;
        dog.transform.position = dogPos;
        dino.transform.position = dinoPos;
        bettleChild.transform.position = bettleChildPos;
        bettleChild.GetComponent<FlyingEnemy>().caught = false;
       
        vultureChild.transform.position = vultureChildPos;
        vultureChild.GetComponent<FlyingEnemy>().caught = false;
        bettleRb.isKinematic = true;
        vultureRb.isKinematic = true;
        dogRb.isKinematic = true;
        dinoRb.isKinematic = true;

        acceleration = acc;
        accelerationTime = accTime;
        fireworks.SetActive(false);
        Takes++;
        gameOver = false;

        SaveGame();
        gameObject.GetComponent<SpriteRenderer>().sprite = original;
        gameObject.GetComponent<Animator>().enabled = true;
    }

    private void GameOver()
    {
        restoringLives = false;
        totalDistance += distanceTraveled;
        gameOverDistance.text = "Distance Travelled = " + distanceTraveled.ToString("N2") + "m";
        isGameStarted = false;
        gameOverPanel.SetActive(true);
        bettle.GetComponent<FollowAndAttack>().playerSpotted = false;
        vulture.GetComponent<FollowAndAttack>().playerSpotted= false;
        dino.GetComponent<FollowAndAttack>().playerSpotted = false;
        dog.GetComponent<FollowAndAttack>().playerSpotted=false;
        startPanel.SetActive(false);
        EnableWaterCollision();

        movement.Stop();
        HideImages();
        SaveGame();
    }
    public void PauseGame()
    {
        gamePaused = true;
       pauseMenu.SetActive(true);
        _rb.isKinematic = true;
        bettleRb.isKinematic =true;
        vultureRb.isKinematic =true;
        dogRb.isKinematic =true;
        dinoRb.isKinematic =true;
        _rb.velocity = Vector3.zero;
        HideImages() ;
        movement.Stop();
    }

    public void ResumeGame()
    {
        gamePaused = false;
        pauseMenu.SetActive(false);
        _rb.isKinematic = false;
        bettleRb.isKinematic = false;
        vultureRb.isKinematic = false;
        dogRb.isKinematic = false;
        dinoRb.isKinematic = false;
        ShowImages();
        movement.Play();
    }
    void CloseTransition()
    {
        transitionPanel.SetActive(false);
       
    }
    public void RestartFromCheckPointOne()
    {
        currentCheckPoint= checkPoint1;
        RestartGame();
        
    }
    public void RestartFromCheckPointTwo()
    {
        currentCheckPoint = checkPoint2;
        RestartGame();

    }
    public void RestartFromCheckPointThree()
    {
        currentCheckPoint = checkPoint3;
        RestartGame();

    }
    public void RestartFromStart()
    {
        currentCheckPoint = startPosition;
       
        RestartGame();

    }

    void CheckPointSpeeds()
    {
        if (currentCheckPoint == checkPoint1)
        {
            speed = 8 + 1.5f;
        }
        if (currentCheckPoint == checkPoint2)
        {
            speed = 8 + 2.5f;
        }
        if (currentCheckPoint == checkPoint3)
        {
            speed = 8 + 3f;
        }
        if (currentCheckPoint == startPosition)
        {
            speed = 8;
        }
        speedAdjusted = true;
    }

    public void OpenHome()
    {
        restoringLives = false;
        squeak.Stop();
        lives = noOfLives;
        heart.SetActive(true);
        heart.GetComponent<Image>().sprite = fullHeart;

        finishedPanel.SetActive(false);
        isGameStarted = false;

        movement.Stop();
        fireworks.SetActive(false);

        SaveGame();
        gameObject.GetComponent<SpriteRenderer>().sprite = original;
        gameObject.GetComponent<Animator>().enabled = true;
        transitionPanel.SetActive(true);
        Invoke("CloseTransition", 1.5f);
        HideImages();
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameObject.transform.position = currentCheckPoint;
        _rb.isKinematic = true;

        bettle.transform.position = bettlePos;
        vulture.transform.position = vulturePos;
        dog.transform.position = dogPos;
        dino.transform.position = dinoPos;
        bettleChild.transform.position = bettleChildPos;
        bettleChild.GetComponent<FlyingEnemy>().caught = false;
        vultureChild.transform.position = vultureChildPos;
        vultureChild.GetComponent<FlyingEnemy>().caught = false;
        bettleRb.isKinematic = true;
        vultureRb.isKinematic = true;
        dogRb.isKinematic = true;
        dinoRb.isKinematic = true;

        acceleration = acc;
        accelerationTime = accTime; 
        gameOver = false;
        CheckPointSpeeds();
    }

    void FinishedGame()
    {
        finishedPanel.SetActive(true);
        currentCheckPoint = startPosition;
        fireworks.SetActive(true);
        totalDistance += distanceTraveled;
        finishedDistancetxt.text = "Distance Travelled = " + distanceTraveled.ToString("N2") + "m";
        totalDistanceTxt.text = "Total Distance Travelled = " + totalDistance.ToString("N2") + "m";
        takesTxt.text = "Total Takes = " + Takes;
        movement.Stop();
        SaveGame();
    }

    #region SaveGame

    private void SaveGame()
    {
        int checkpointIndex = GetCheckpointIndex();
        PlayerPrefs.SetInt(CheckpointKey, checkpointIndex);
        PlayerPrefs.SetFloat(TotalDistanceKey, totalDistance);
        PlayerPrefs.SetInt(TakesKey, Takes);
        PlayerPrefs.SetFloat(SpeedKey, speed);
        PlayerPrefs.Save();
    }

    private void LoadGame()
    {
        if (PlayerPrefs.HasKey(CheckpointKey))
        {
            int checkpointIndex = PlayerPrefs.GetInt(CheckpointKey);
            currentCheckPoint = GetCheckpointPosition(checkpointIndex);
        }

        if (PlayerPrefs.HasKey(TotalDistanceKey))
        {
            totalDistance = PlayerPrefs.GetFloat(TotalDistanceKey);
        }

        if (PlayerPrefs.HasKey(TakesKey))
        {
            Takes = PlayerPrefs.GetInt(TakesKey);
        }
        if (PlayerPrefs.HasKey(SpeedKey))
        {
            speed = PlayerPrefs.GetFloat(SpeedKey);
        }
    }

    private int GetCheckpointIndex()
    {
        if (currentCheckPoint == checkPoint1) return 1;
        if (currentCheckPoint == checkPoint2) return 2;
        if (currentCheckPoint == checkPoint3) return 3;
        return 0; // Default to startPosition
    }

    private Vector3 GetCheckpointPosition(int index)
    {
        switch (index)
        {
            case 1: return checkPoint1;
            case 2: return checkPoint2;
            case 3: return checkPoint3;
            default: return startPosition;
        }
    }
    #endregion

    #region buttons
    public void OpenCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);  
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    void HideImages()
    {
        foreach (var item in checkpointImages)
        {
            item.SetActive(false);
        }
    }
    void ShowImages()
    {
        foreach (var item in checkpointImages)
        {
            item.SetActive(true);
        }
    }

    #endregion
}
