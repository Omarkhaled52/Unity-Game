using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    // player movement
    private CharacterController controller;
    public float SpeedF;
    private int Lane = 1;
    public float LaneFactor = 3.5f;

    Rigidbody rb;

    

    //public string targetTag;
    [SerializeField] AudioSource Coin;
    [SerializeField] AudioSource Evolution;
    [SerializeField] AudioSource negativeEvolution;
    [SerializeField] AudioSource hitObstacle;
    [SerializeField] AudioSource powerUp;
    [SerializeField] AudioSource invalidAction;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI Red;
    [SerializeField] TextMeshProUGUI Blue;
    [SerializeField] TextMeshProUGUI Green;

    public static int counter = 0;
    int counterBlue = 0;
    int counterRed = 0;
    int counterGreen = 0;
    Boolean isGreen = false;
    Boolean isBlue = false;

    private Renderer playerRenderer;

    private GameObject []obstacles;

    void Start()
    {
        counter = 0;
        controller = GetComponent<CharacterController>();
        rb = this.GetComponent<Rigidbody>();
        playerRenderer = GetComponent<Renderer>();

    }


    // Update is called once per frame
    void Update()
    {

        //MainMenu.setMute();

        //Movement
        transform.position += new Vector3(0, 0, SpeedF) * Time.deltaTime;


        if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
        {
            Lane--;
            if (Lane == -1)
            {
                Lane = 0;
            }
        }
        if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
        {
            Lane++;
            if (Lane == 3)
            {
                Lane = 2;
            }
        }


        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (Lane == 0)
        {
            targetPosition += Vector3.left * LaneFactor;
        }
        else if (Lane == 2)
        {
            targetPosition += Vector3.right * LaneFactor;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 10 * Time.deltaTime);

        if (counterRed == 5 && Input.GetKeyDown("j") && playerRenderer.material.color != Color.red)
        {
            playerRenderer.material.color = Color.red;
            counterRed--;
            Red.text = "Red: " + counterRed;
            Evolution.Play();
        }
        if(counterRed < 5 && Input.GetKeyDown("j") && playerRenderer.material.color != Color.red)
        {
            invalidAction.Play();
        }

        if (counterBlue == 5 && Input.GetKeyDown("k") && playerRenderer.material.color != Color.blue)
        {
            playerRenderer.material.color = Color.blue;
            counterBlue--;
            Blue.text = "Blue: " + counterBlue;
            Evolution.Play();

        }
        if (counterBlue < 5 && Input.GetKeyDown("k") && playerRenderer.material.color != Color.blue)
        {
            invalidAction.Play();
        }
        if (counterGreen == 5 && Input.GetKeyDown("l") && playerRenderer.material.color != Color.green)
        {
            playerRenderer.material.color = Color.green;
            counterGreen--;
            Green.text = "Green: " + counterGreen;
            Evolution.Play();

        }
        if(counterGreen < 5 && Input.GetKeyDown("l") && playerRenderer.material.color != Color.green)
        {
            invalidAction.Play();
        }
        if ((counterRed == 0 && playerRenderer.material.color == Color.red) || (counterBlue == 0 && playerRenderer.material.color == Color.blue) || (counterGreen == 0 && playerRenderer.material.color == Color.green))
        {
            playerRenderer.material.color = Color.white;
            negativeEvolution.Play();
        }

        if (playerRenderer.material.color == Color.red && Input.GetKeyDown("space"))
        {

            if (counterRed >= 1)
            {
                obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
                powerUp.Play();

                foreach (GameObject obstacle in obstacles)
                {
                    Destroy(obstacle);
                }
                counterRed--;
                Red.text = "Red: " + counterRed;
            }

        }
        else if (playerRenderer.material.color == Color.white && Input.GetKeyDown("space"))
        {
            // print("invalid action");
            invalidAction.Play();
        }
        if (playerRenderer.material.color == Color.green && Input.GetKeyDown("space"))
        {
            if (!isGreen)
            {


                counterGreen--;
                isGreen = true;
                Green.text = "Green: " + counterGreen;
                powerUp.Play();
            }


        }
        else if (playerRenderer.material.color == Color.white && Input.GetKeyDown("space"))
        {
            // print("invalid action");
            invalidAction.Play();
        }


        if (playerRenderer.material.color == Color.blue && Input.GetKeyDown("space"))
        {
            if (!isBlue)
            {
                counterBlue--;
                isBlue = true;
                Blue.text = "Blue: " + counterBlue;
                powerUp.Play();

            }
        }
        else if (playerRenderer.material.color == Color.white && Input.GetKeyDown("space"))
        {
           // print("invalid action");
            invalidAction.Play();
        }

    }


        public void OnTriggerEnter(Collider other)
        {
        if (isBlue )
        {
            
            if (other.gameObject.CompareTag("Obstacle") &&playerRenderer.material.color==Color.blue)
            {
                Destroy(other.gameObject);
                hitObstacle.Play();
                isBlue = false;

            }
            if ((other.gameObject.CompareTag("Obstacle") && playerRenderer.material.color == Color.red)||(other.gameObject.CompareTag("Obstacle") && playerRenderer.material.color == Color.green))
            {
                
              
                hitObstacle.Play();
                isBlue = false;
                playerRenderer.material.color = Color.white;
                //
            }
            if (playerRenderer.material.color ==Color.white && other.gameObject.CompareTag("Obstacle"))
            {
                hitObstacle.Play();
                isBlue = false;
                SceneManager.LoadScene("GameOver");


            }



        }
        else if (!isBlue )
        {
            if (playerRenderer.material.color == Color.white)
            {
                if (other.gameObject.CompareTag("Obstacle"))
                {
                    Time.timeScale = 0f;
                    hitObstacle.Play();
                    SceneManager.LoadScene("GameOver");
                }
            }
            if(playerRenderer.material.color == Color.green|| playerRenderer.material.color == Color.red|| playerRenderer.material.color == Color.blue)
            if (other.gameObject.CompareTag("Obstacle"))
            {
                playerRenderer.material.color = Color.white;
                    hitObstacle.Play();
                }
        }

        if (isGreen)
        {
            if (playerRenderer.material.color == Color.red || playerRenderer.material.color == Color.blue)
            {
               
                if (other.gameObject.CompareTag("Blue"))
                {
                    Coin.Play();
                    Destroy(other.gameObject);
                    counter+=2;
                    if (counterBlue < 5)
                    {

                        counterBlue++;
                        Blue.text = "Blue: " + counterBlue;
                        
                    }

                }
                if (other.gameObject.CompareTag("Red"))
                {
                    Coin.Play();
                    Destroy(other.gameObject);
                    counter+=2;
                    if (counterRed < 5)
                    {
                        counterRed++;
                        Red.text = "Red: " + counterRed;

                    }

                }
                isGreen = false;
                
                
                score.text = "Score: " + counter;
            }
            if (playerRenderer.material.color == Color.green)
            {
                if (other.gameObject.CompareTag("Green"))
                {
                    counter += 10;
                    counterGreen += 0;
                    Green.text = "Green: " + counterGreen;
                    Destroy(other.gameObject);
                    Coin.Play();

                }
                if (other.gameObject.CompareTag("Red"))
                {
                    counter += 5;
                    if (counterRed <= 2)
                    {
                        counterRed += 2;
                        if (counterRed >= 3)
                        {
                            counterRed = 5;
                            Red.text = "Red: " + counterRed;
                        }
                    }
                    Destroy(other.gameObject);
                    Coin.Play();

                }
                if (other.gameObject.CompareTag("Blue"))
                {
                    counter += 5;
                    if (counterBlue <= 2)
                    {
                        counterBlue += 2;
                        if (counterBlue >= 3)
                        {
                            counterBlue = 5;
                            Blue.text = "Blue: " + counterBlue;
                        }
                    }
                    Destroy(other.gameObject);
                    Coin.Play();


                }
                isGreen = false;
                score.text = "Score: " + counter;

            }
            if(playerRenderer.material.color == Color.white)
            {
                if (other.gameObject.CompareTag("Green"))
                {
                    counter += 1;
                    counterGreen += 1;
                    Green.text = "Green: " + counterGreen;
                    Destroy(other.gameObject);
                    Coin.Play();

                }
                if (other.gameObject.CompareTag("Red"))
                {
                    counter += 1;
                    if (counterRed <= 2)
                    {
                        counterRed += 1;
                        if (counterRed >= 3)
                        {
                            counterRed +=1;
                            Red.text = "Red: " + counterRed;
                        }
                    }
                    Destroy(other.gameObject);
                    Coin.Play();

                }
                if (other.gameObject.CompareTag("Blue"))
                {
                    counter += 1;
                    counterBlue += 1;
                    Blue.text = "Blue: " + counterBlue;
                        
                    
                    Destroy(other.gameObject);
                    Coin.Play();


                }
                isGreen = false;
                score.text = "Score: " + counter;

            }
        }
        else if (!isGreen)
        {

            if (other.gameObject.CompareTag("Blue"))
            {
                Destroy(other.gameObject);
                Coin.Play();

                if (counterBlue < 5)
                {
                    counterBlue++;
                    Blue.text = "Blue: " + counterBlue;
                }
                if (playerRenderer.material.color == Color.blue)
                {
                    counter += 2;
                }
                else
                {
                    counter++;
                }
                score.text = "Score: " + counter;


            }
            else if (other.gameObject.CompareTag("Red"))
            {


                Destroy(other.gameObject);
                Coin.Play();

                if (counterRed < 5)
                {
                    counterRed++;
                    Red.text = "Red: " + counterRed;
                }
                if (playerRenderer.material.color == Color.red)
                {
                    counter += 2;
                }
                else
                {
                    counter++;
                }
                score.text = "Score: " + counter;
            }
            else if (other.gameObject.CompareTag("Green"))
            {


                Destroy(other.gameObject);
                Coin.Play();

                if (counterGreen < 5)
                {
                    counterGreen++;
                    Green.text = "Green: " + counterGreen;

                }
                if (playerRenderer.material.color == Color.green)
                {
                    counter += 2;
                }
                else
                {
                    counter++;
                }
            }



            score.text = "Score: " + counter;


        }





        }
    
}

