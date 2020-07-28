using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    PiUIManager piUi;
    private bool menuOpened;
    private PiUI normalMenu;
    // Use this for initialization
    void Start()
    {
        //Get menu for easy not repetitive getting of the menu when setting joystick input
        normalMenu = piUi.GetPiUIOf("Normal Menu");
    }

    private void update_ui_inventory(bool enter)
    {
        Messenger<bool>.Broadcast(GameEvent.UI_INVENTORY, enter);
        if (enter)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = enter;
    }

    // Update is called once per frame
    void Update()
    {
        //Bool function that returns true if on a menu
        // if (piUi.OverAMenu( ))
        //     Debug.Log("You are over a menu");
        // else
        //     Debug.Log("You are not over a menu");
        //Just open the normal Menu if A is pressed
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     piUi.ChangeMenuState("Normal Menu", new Vector2(Screen.width / 2f, Screen.height / 2f));
        // }
        // //Update the menu and add the Testfunction to the button action if s or Fire1 axis is pressed
        // if (Input.GetKeyDown(KeyCode.S) || Input.GetButtonDown("Fire1"))
        // {
        //     //Ensure menu isnt currently open on update just for a cleaner look
        //     if (!piUi.PiOpened("Normal Menu"))
        //     {
        //         int i = 0;
        //         //Iterate through the piData on normal menu
        //         foreach (PiUI.PiData data in normalMenu.piData)
        //         {
        //             //Changes slice label
        //             data.sliceLabel = "Test" + i.ToString( );
        //             //Creates a new unity event and adds the testfunction to it
        //             data.onSlicePressed = new UnityEngine.Events.UnityEvent( );
        //             data.onSlicePressed.AddListener(TestFunction);
        //             i++;
        //         }
        //         //Since PiUI.sliceCount or PiUI.equalSlices didnt change just calling update
        //         piUi.UpdatePiMenu("Normal Menu");
        //     }
        //     //Open or close the menu depending on it's current state at the center of the screne
        //     piUi.ChangeMenuState("Normal Menu", new Vector2(Screen.width / 2f, Screen.height / 2f));
        // }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Ensure menu isnt currently open on regenerate so it doesnt spasm
            if (!piUi.PiOpened("Normal Menu"))
            {
                update_ui_inventory(true);
                //Make all angles equal 
                normalMenu.equalSlices = true;
                normalMenu.iconDistance = 0f;
                //Changes the piDataLength and adds new piData
                normalMenu.piData = new PiUI.PiData[6];
                for(int j = 0; j < 6; j++)
                {
                    normalMenu.piData[j] = new PiUI.PiData( );
                }
                //Turns of the syncing of colors
                normalMenu.syncColors = false;
                // distance de l'icon du centre
                normalMenu.iconDistance = 1.1f;
                //Changes open/Close animations
                normalMenu.openTransition = PiUI.TransitionType.SlideUp;
                normalMenu.closeTransition = PiUI.TransitionType.SlideDown;

                int arme1 = 1;
                int arme2 = 2;
                int health = 0;
                int energy = 5;
                int key = 3;
                int orel = 4;

                Sprite imageHealth = Resources.Load<Sprite>("Icons/health");
                Sprite imageOre = Resources.Load<Sprite>("Icons/ore");
                Sprite imageEnergy = Resources.Load<Sprite>("Icons/energy");
                Sprite imageKey = Resources.Load<Sprite>("Icons/key");
                Sprite imageHammer = Resources.Load<Sprite>("Icons/Hammer");

                int i = 0;
                foreach (PiUI.PiData data in normalMenu.piData)
                {
                    //Turning off the interactability of a slice
                    data.onSlicePressed = new UnityEngine.Events.UnityEvent( );
                    data.onHoverEnter = new UnityEngine.Events.UnityEvent( );
                    data.onHoverExit = new UnityEngine.Events.UnityEvent( );
                    data.hoverFunctions = true;

                    //Set new highlight/non highlight colors
                    data.nonHighlightedColor = new Color(1 - i/10f, 0, 0, 1);
                    data.highlightedColor = new Color(0, 0, 1 - i/10f, 1);
                    data.disabledColor = Color.grey;
                    data.iconSize = 90;
                    data.isInteractable = true;

                    if(i == arme1 || i == arme2)
                    {
                        data.isInteractable = false;
                        data.sliceLabel = "arme " + i;
                        data.icon = imageHammer;

                        // data.onSlicePressed = new UnityEngine.Events.UnityEvent( );
                        // data.onSlicePressed.AddListener(TestFunction);
                    }
                    else if (i == health) {
                        data.nonHighlightedColor = Color.red;
                        data.highlightedColor = Color.blue;
                        data.sliceLabel =  "health ( " + Managers.Inventory.GetItemCount("health") + " )";
                        data.icon = imageHealth;

                        data.onSlicePressed.AddListener(OnHoverSlicePressedHealth);
                        data.onHoverEnter.AddListener(OnHoverEnterHealth);
                        data.onHoverExit.AddListener(OnHoverExitHealth);
                    }
                    else if (i == energy) {
                        data.nonHighlightedColor = Color.yellow;
                        data.highlightedColor = Color.blue;
                        data.sliceLabel = "energy ( " + Managers.Inventory.GetItemCount("energy") + " )";
                        data.icon = imageEnergy;

                        data.onSlicePressed.AddListener(OnHoverSlicePressedEnergy);
                        data.onHoverEnter.AddListener(OnHoverEnterEnergy);
                        data.onHoverExit.AddListener(OnHoverExitEnergy);
                    }
                    else if (i == key) {
                        data.nonHighlightedColor = Color.cyan;
                        data.highlightedColor = Color.blue;
                        data.sliceLabel = "key ( " + Managers.Inventory.GetItemCount("key") + " )";
                        data.icon = imageKey;

                        data.onSlicePressed.AddListener(OnHoverSlicePressedOrel);
                        data.onHoverEnter.AddListener(OnHoverEnterOrel);
                        data.onHoverExit.AddListener(OnHoverExitOrel);
                    }
                    else if (i == orel) {
                        data.nonHighlightedColor = Color.white;
                        data.highlightedColor = Color.blue;
                        data.sliceLabel = "orel ( " + Managers.Inventory.GetItemCount("orel") + " )";
                        data.icon = imageOre;

                        data.onSlicePressed.AddListener(OnHoverSlicePressedKey);
                        data.onHoverEnter.AddListener(OnHoverEnterKey);
                        data.onHoverExit.AddListener(OnHoverExitKey);
                    }
                    
                    //Changes slice label
                    //data.sliceLabel = "Test" + i.ToString( );
                    //Creates a new unity event and adds the testfunction to it
                    
                    i += 1;
                    //Enables hoverFunctions
                    
                    //Creates a new unity event to adds on hovers function
                    // data.onHoverEnter = new UnityEngine.Events.UnityEvent( );
                    // data.onHoverEnter.AddListener(OnHoverEnter);
                    // data.onHoverExit = new UnityEngine.Events.UnityEvent( );
                    // data.onHoverExit.AddListener(OnHoverExit);
                }
                piUi.RegeneratePiMenu("Normal Menu");
            }
            else {
                update_ui_inventory(false);
            }
            piUi.ChangeMenuState("Normal Menu", new Vector2(Screen.width / 2f, Screen.height / 2f));
        }

        //Set joystick input on the normal menu which the piPieces check
        normalMenu.joystickInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //Set the bool to detect if the controller button has been pressed
        normalMenu.joystickButton = Input.GetButtonDown("Fire1");
        //If the button isnt pressed check if has been released
        if (!normalMenu.joystickButton)
        {
            normalMenu.joystickButton = Input.GetButtonUp("Fire1");
        }
    }
    //Test function that writes to the console and also closes the menu
    public void TestFunction()
    {
        //Closes the menu
        piUi.ChangeMenuState("Normal Menu");
        Debug.Log("You Clicked me!");
        update_ui_inventory(false);
    }

    // ------------- health --------------------

    public void OnHoverSlicePressedHealth()
    {
        Messenger.Broadcast(GameEvent.HEALTH_USE);
        update_ui_inventory(false);
        piUi.ChangeMenuState("Normal Menu");
    }

    public void OnHoverEnterHealth()
    {
        Messenger.Broadcast(GameEvent.HEALTH_ENTER);
    }
    public void OnHoverExitHealth()
    {
       Messenger.Broadcast(GameEvent.HEALTH_EXIT);
    }

    // ------------- energy --------------------

    public void OnHoverSlicePressedEnergy()
    {
        Messenger.Broadcast(GameEvent.ENERGY_USE);
        update_ui_inventory(false);
        piUi.ChangeMenuState("Normal Menu");
    }

    public void OnHoverEnterEnergy()
    {
        Messenger.Broadcast(GameEvent.ENERGY_ENTER);
    }
    public void OnHoverExitEnergy()
    {
       Messenger.Broadcast(GameEvent.ENERGY_EXIT);
    }

    // ------------- orel --------------------

    public void OnHoverSlicePressedOrel()
    {
        Messenger.Broadcast(GameEvent.OREL_USE);
        update_ui_inventory(false);
        piUi.ChangeMenuState("Normal Menu");
    }

    public void OnHoverEnterOrel()
    {
        Messenger.Broadcast(GameEvent.OREL_ENTER);
    }
    public void OnHoverExitOrel()
    {
       Messenger.Broadcast(GameEvent.OREL_EXIT);
    }

    // ------------- key --------------------

    public void OnHoverSlicePressedKey()
    {
        Messenger.Broadcast(GameEvent.KEY_USE);
        update_ui_inventory(false);
        piUi.ChangeMenuState("Normal Menu");
    }

    public void OnHoverEnterKey()
    {
        Messenger.Broadcast(GameEvent.KEY_ENTER);
    }
    public void OnHoverExitKey()
    {
       Messenger.Broadcast(GameEvent.KEY_EXIT);
    }

    // public void OnHoverEnterX()
    // {
    //     Debug.Log("XXXXXXXXXXXX");
    //     // Debug.Log(normalMenu.onSlicePressed);
    // }

    // public void OnHoverEnter()
    // {
    //     Debug.Log("Hey get off of me!");
    //     // Debug.Log(normalMenu.onSlicePressed);
    // }
    // public void OnHoverExit()
    // {
    //     Debug.Log("That's right and dont come back!");
    // }
}
