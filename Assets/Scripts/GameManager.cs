using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//GameManger is a singleton Monobehavior
//This class will become big :) as such in gamejams.... :(
public class GameManager : MonoBehaviour
{
    #region Instance

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                print("Instance of GameObject does not exist!");

            return instance;
        }
    }

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    #endregion

    //Managers
    [Header("Managers")]
    public GuiManager guiManager;

    [Header("Database")]
    public CardDatabase cardDatabase;
    public ShipDatabase shipDatabase;
    public Decks deckDatabase;
    public List<GameObject> shipModelsID;

    //public ShipDatabase ship;


    [Header("General")]
    //[HideInInspector] public Arena arena;
    public List<Player> players = new List<Player>();
    public CameraController cam;
    public GameObject cancelButton;

    [Header("Data")]
    public PlayerData Player1;
    public bool npcPlayer2 = true;
    public PlayerData Player2;

    public int currentPlayer = 0;
    public int selectedCard;
    public UICard UIcard;
    

    

    //Attack Anim Stuff
    private float timer;
    public AudioSource as_shoot;
    public AudioSource as_hit;
    public ParticleSystem particle_shoot;
    public ParticleSystem particle_hitBlack;
    public ParticleSystem particle_hitOrange;

    private List<int> shipsThatGotHit = new List<int>();
    private int shipsThatGotHitCounter;


    public enum Phases
    {
        PlaceShips,
        BeginTurn,
        PlayerAction, //Move & Attack
        EnemyAction,
        AttackAnim,
        HitAnimPrepare,
        HitAnim,
        Wait,
        Victory,
        NULL
    }

    //public GameMode mode;
    public Phases phase = Phases.PlayerAction;
    private Phases pendingPhase;

    private void Start()
    {
        //Translate PlayerData into Player List
        players.Add(new Player(Player1));
        players.Add(new Player(Player2));

        //Player Give cards to player

        for (int i = 0; i < 7; i++)
            players[0].DrawCard();

        guiManager.UpdateCardsInHand(players[0]);

        guiManager.ShowCards(false);
        guiManager.ShowGrid(true);
    }

    private void Update()
    {
        int enemyID = (currentPlayer == 0) ? 1 : 0;
        //Player enemy = players[enemyID];

        switch (phase)
        {
            case Phases.PlaceShips:
                break;

            case Phases.BeginTurn:

                //Flip Player
                currentPlayer = (currentPlayer == 0) ? 1 : 0;

                guiManager.ClearTiles();
                guiManager.ShowCards(true);
                cam.SwitchModes(0);
                players[0].NextShip();
                ToggleShip(players[0].ships[players[0].selectedShip].shipClass);

                //Switch Phase

                //If next turn is ENEMY than hardcode for them to play :)
                if (currentPlayer == 0)
                {
                    cancelButton.gameObject.SetActive(true);
                    guiManager.text_helper.color = Color.yellow;
                    guiManager.text_helper.text = "";
                    phase = Phases.PlayerAction;
                }

                else
                {
                    guiManager.text_helper.color = Color.red;
                    guiManager.text_helper.text = "Enemy's Turn";
                    guiManager.ShowCards(false);
                    guiManager.ShowGrid(false);

                    //Pending
                    timer = 1.3f;
                    pendingPhase = Phases.EnemyAction;
                    phase = Phases.Wait;
                }


                break;

            case Phases.EnemyAction:

                EnemyAction_Logic();
                  
                break;

            case Phases.AttackAnim:
                //Play Fire Explosion Particle
                //Play Fire Explosion Sfx
                //Countdown Timer then switch to HitAnim!

                guiManager.ShowGrid(false);

                as_shoot.Play();
                particle_shoot.Play();

                //Pending
                timer = 1f;
                pendingPhase = Phases.HitAnimPrepare;
                phase = Phases.Wait;

                break;

            case Phases.HitAnimPrepare:
                //Check every anyship and see if it hit anything
                //If so remember ship
                //Play hit anim

                shipsThatGotHit = players[enemyID].AttackArea(guiManager.lastColoredTiles);
                print(shipsThatGotHit.Count + " ships were hit!");

                //Hit one or more ships!
                if (shipsThatGotHit.Count > 0)
                {
                    shipsThatGotHitCounter = 0;
                    cam.SwitchModesFast(CameraController.CamModes.Hit);
                    ToggleShip(players[enemyID].ships[shipsThatGotHit[shipsThatGotHitCounter]].shipClass);

                    //Pending
                    timer = 1.4f;
                    pendingPhase = Phases.HitAnim;
                    phase = Phases.Wait;
                    return;
                }
                //-----IF NO HIT ------
                else
                {
                    //Pending
                    timer = 1f;
                    pendingPhase = Phases.BeginTurn;
                    phase = Phases.Wait;
                }             


                break;

            case Phases.HitAnim:

                //Start with first boat that got hit then repeat
                //Swap Boat model
                //Check if ship is destroyed!

                as_hit.Play();
                particle_hitBlack.Play();
                particle_hitOrange.Play();
               

                //CHANGE IF I WANT HEAL, MOVEMENT, OR OBSTACLES!
                int damage = cardDatabase.cards[selectedCard].power;
                int shipThatGotHit = shipsThatGotHit[shipsThatGotHitCounter];


                players[enemyID].DamageShip(shipThatGotHit, damage);

                if (players[enemyID].ships[shipsThatGotHit[shipsThatGotHitCounter]].health <= 0)
                {
                    players[enemyID].ships.RemoveAt(shipsThatGotHit[shipsThatGotHitCounter]);
                    
                    ClearShips();

                    //CHECK IF PLAYER WON!
                    if (players[enemyID].ships.Count == 0)
                    {
                        print("ALL SHIPS DESTROYED!");


                        //Pending
                        timer = 2.3f;
                        pendingPhase = Phases.Victory;
                        phase = Phases.Wait;

                        return;
                    }                    
                }                


                shipsThatGotHitCounter++;

                //Finished!
                if (shipsThatGotHitCounter >= shipsThatGotHit.Count)
                {
                    shipsThatGotHit.Clear();

                    //Pending
                    timer = 1f;
                    pendingPhase = Phases.BeginTurn;
                    phase = Phases.Wait;
                }

                //Do next boat
                else
                {
                    //ToggleShip(players[enemyID].ships[shipsThatGotHit[shipsThatGotHitCounter]].shipClass);
                    ToggleShip(players[enemyID].ships[shipsThatGotHit[shipsThatGotHitCounter]].shipClass);

                    //Pending
                    timer = 1.4f;
                    pendingPhase = Phases.HitAnim;
                    phase = Phases.Wait;
                }


                break;

            case Phases.Wait:

                timer -= Time.deltaTime;

                if (timer <= 0)
                    phase = pendingPhase;

                break;

            case Phases.Victory:
                 phase = Phases.Victory;
                 guiManager.ShowCards(false);
                 guiManager.ShowGrid(false);

                cam.SwitchModes(0);
                ToggleShip(players[0].ships[players[0].selectedShip].shipClass);

                guiManager.text_Victory.text = "PLAYER " + (currentPlayer + 1).ToString() + " WINS!";
                 guiManager.text_Victory.gameObject.SetActive(true);

                phase = Phases.NULL;

                break;

            default:
                break;
        }
    }

    //Attack, move, etc from tile click
    public void PerformGridAction() //ON CLICK TILE!
    {
        //Perform action after selecting grid
        //Use card effect here!

        switch (phase)
        {
            case Phases.PlaceShips:
                //Get next ship to place from player
                //If all ships are placed then move to next phase

                var lastCoordinate = guiManager.lastCoordinate;

                if (guiManager.legal) //If allowed to place ship there!
                {
                    //Placeship also checks if it went through all the ships!
                    if (players[0].PlaceShip(players[0].selectedShip, guiManager.lastColoredTiles) == true)
                    {
                        players[0].selectedShip = 0;

                        //IF True than all ships have been placed! SWITCH PHASE
                        guiManager.ShowCards(true);
                        guiManager.ShowGrid(false);

                        phase = Phases.PlayerAction;
                        guiManager.ClearTiles();

                        cancelButton.SetActive(true);

                        //HARD CODE AI HERE TO PLACE THEIR SHIPS...
                        EnemyPlaceShips();

                        //Show appropriate SHIP
                        ToggleShip(players[0].ships[players[0].selectedShip].shipClass);

                        guiManager.text_helper.text = "";
                        
                        return;
                    }

                    guiManager.ClearTiles();

                    guiManager.lastPlayer = players[0];
                    guiManager.ShowPlayerShips(players[0], Color.green);
                    
                }

                break;

            //ON CARD CLICK
            case Phases.PlayerAction: 
                
                //Clear then we update later
                RemoveCardFromHand(UIcard);

                //Add Hitmarker from attack from last time
                players[0].AddPlayerAttackMarker(guiManager.lastColoredTiles);

                players[0].DrawCard();

                guiManager.UpdateCardsInHand(players[0]);

                //SwitchCameraMode(0);
                guiManager.ClearTiles();
                guiManager.ShowGrid(false);

                //PENDING

                timer = 0.25f;
                pendingPhase = Phases.AttackAnim;
                phase = Phases.Wait;

                break;

            default:
                break;
        }


    }

    public Player GetCurrentPlayer()
    {
        return players[currentPlayer];
    }

    public void RemoveCardFromHand(UICard card)
    {
        //guiManager.cardsInHand.Remove(card);

        //Find matching cardID in player hand and remove it
        //Update all Cards in hand again

        for (int i = 0; i < players[0].cardsInHand.Count; i++)
        {
            if (players[0].cardsInHand[i] == card.cardID)
            {
                players[0].cardsInHand.RemoveAt(i);
                break;
            }            
        }        
    }

    public void SelectCard(int cardID)
    {
        selectedCard = cardID;
        guiManager.ShowGrid(true);
        guiManager.ShowCards(false);
    }

    public void SwitchCameraMode(int mode)
    {
        cam.SwitchModes(CameraController.CamModes.Ship);

        if (mode == 1)
            cam.SwitchModes(CameraController.CamModes.Attack);
        else if (mode == 2)
            cam.SwitchModes(CameraController.CamModes.Hit);
    }

    private void ToggleShip(int shipID)
    {
        ClearShips();

        shipModelsID[shipID].gameObject.SetActive(true);
    }
    private void ToggleShip(Ship.ShipClass shipClass)
    {
        ClearShips();

        switch (shipClass)
        {
            case Ship.ShipClass.Submarine: //3
                shipModelsID[3].gameObject.SetActive(true);
                break;

            case Ship.ShipClass.Cruiser: //2
                shipModelsID[2].gameObject.SetActive(true);
                break;

            case Ship.ShipClass.Battleship: //1
                shipModelsID[1].gameObject.SetActive(true);
                break;

            case Ship.ShipClass.Destroyer: //4
                shipModelsID[4].gameObject.SetActive(true);
                break;

            case Ship.ShipClass.Carrier: //0
                shipModelsID[0].gameObject.SetActive(true);
                break;

            default:
                break;
        }
    }

    private void ClearShips()
    {
        for (int i = 0; i < shipModelsID.Count; i++)        
            shipModelsID[i].gameObject.SetActive(false);        
    }


    //-------------------- HARDCODED ENEMY AI DOWN HERE ;) -----------------------------------

    private void EnemyPlaceShips()
    {
        //Choose random coordinate and random rotation
            //Check if legal
                //If Yes then place next ship
                //If No try again

        Player player2 = players[1];

        //Go through each ship
        for (int i = 0; i < player2.ships.Count; i++)
        {
            Ship ship = player2.ships[i];
            int rotateMode = Random.Range(0, 3);
            int xPos = Random.Range(0, 19);
            int yPos = Random.Range(0, 19);

            //For each tile of selected ship!
            for (int j = 0; j < ship.size.Count; j++)
            {
                Vector2Int shipPiece = ship.size[j];

                //Rotations
                var tempArea = shipPiece;
                if (rotateMode == 1)
                {
                    shipPiece.x = tempArea.y;
                    shipPiece.y = -tempArea.x;
                }
                else if (rotateMode == 2)
                {
                    shipPiece.x = -tempArea.x;
                    shipPiece.y = -tempArea.y;
                }
                else if (rotateMode == 3)
                {
                    shipPiece.x = -tempArea.y;
                    shipPiece.y = tempArea.x;
                }
                //-----------------------------

                //When considering the coordinate along with the aoe
                Vector2Int coordinate = new Vector2Int(xPos, yPos);
                Vector2Int takenSpaceOffset = coordinate + shipPiece;



                if (CheckIfColliding(player2, takenSpaceOffset) == true)
                {
                    i--;
                    ship.hitboxLocations.Clear();
                    break;
                }

                else
                {
                    ship.hitboxLocations.Add(takenSpaceOffset);
                }
      
            }
        }
    }

    public bool CheckIfColliding(Player player, Vector2Int partOfShip)
    {
        for (int j = 0; j < player.ships.Count; j++)
        {
            for (int k = 0; k < player.ships[j].hitboxLocations.Count; k++)
            {
                //If colliding with an exisiting ship structure or out of bounds NOT LEGAL
                if (player.ships[j].hitboxLocations[k] == partOfShip ||
                    partOfShip.x < 0 ||
                    partOfShip.y < 0 ||
                    partOfShip.y >= 20 ||
                    partOfShip.x >= 20)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void EnemyAction_Logic()
    {
        cam.SwitchModes(CameraController.CamModes.Attack);
        guiManager.lastColoredTiles.Clear();

        guiManager.ShowGrid(true);
        guiManager.ShowPlayerShips(players[0], Color.green);

        cancelButton.gameObject.SetActive(false);

        //Get random card from deck
        int cardIndex = Random.Range(0, players[1].cardsInDeck.Count - 1);
        selectedCard = players[1].cardsInDeck[cardIndex];


        int rotateMode = Random.Range(0, 3);
        int xPos = Random.Range(0, 19);
        int yPos = Random.Range(0, 19);


        //players[1].AddPlayerAttackMarker(guiManager.lastColoredTiles);

        Card card = cardDatabase.cards[selectedCard];
  

        Vector2Int coordinate = new Vector2Int(xPos, yPos);
        guiManager.ActionColors(coordinate, rotateMode); //Updates lastColoredTiles to be used to attack here later!


        //PENDING
        timer = 1.35f;
        pendingPhase = Phases.AttackAnim;
        phase = Phases.Wait;
        
    }

    private void AttackAnim_Logic()
    {

    }


}



