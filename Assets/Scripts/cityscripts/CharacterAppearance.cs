// -----------------------------------------------------------------------------------------
// using classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// -----------------------------------------------------------------------------------------
// player movement class
public class CharacterAppearance : MonoBehaviour
{
    // static public members
    public static CharacterAppearance instance;

    // -----------------------------------------------------------------------------------------
    // public members
    public Transform tf;
    public Vector2 movement;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    // The name of the sprite sheet to use
    public string SpriteSheetName;

    // -----------------------------------------------------------------------------------------
    // private members
    private Vector2 previousPosition;

    // The name of the currently loaded sprite sheet
    private string LoadedSpriteSheetName;

    // The dictionary containing all the sliced up sprites in the sprite sheet
    private Dictionary<string, Sprite> spriteSheet;

    // -----------------------------------------------------------------------------------------
    // awake method to initialisation
    void Awake()
    {
        instance = this;
        previousPosition = tf.position;
        //velocity = rb.velocity;
        this.LoadSpriteSheet();
        animator.SetFloat("speed", 0);
        animator.SetInteger("orientation", 4);
    }
    // -----------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {

    }
    // -----------------------------------------------------------------------------------------
    // fixed update methode
    void FixedUpdate()
    {
        movement.x = tf.position.x - previousPosition.x;
        movement.y = tf.position.y - previousPosition.y;

        previousPosition = tf.position;

        animationUpdate();
    }

    // Runs after the animation has done its work
    private void LateUpdate()
    {
        // Check if the sprite sheet name has changed (possibly manually in the inspector)
        if (this.LoadedSpriteSheetName != this.SpriteSheetName)
        {
            // Load the new sprite sheet
            this.LoadSpriteSheet();
        }

        // Swap out the sprite to be rendered by its name
        // Important: The name of the sprite must be the same!
        Debug.Log(spriteRenderer.sprite);
        Debug.Log(spriteSheet);
        this.spriteRenderer.sprite = this.spriteSheet[this.spriteRenderer.sprite.name];
    }

    // -----------------------------------------------------------------------------------------
    // Set the animation parameters
    public void animationUpdate()
    {
        animator.SetFloat("speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
        if (movement.x > 0)
            animator.SetInteger("orientation", 6);
        if (movement.x < 0)
            animator.SetInteger("orientation", 2);
        if (movement.y > 0)
            animator.SetInteger("orientation", 0);
        if (movement.y < 0)
            animator.SetInteger("orientation", 4);
    }
    // -----------------------------------------------------------------------------------------
    // Loads the sprites from a sprite sheet
    // -----------------------------------------------------------------------------------------
    // Loads the sprites from a sprite sheet based on player's choice
    private void LoadSpriteSheet()
    {
        // Load the sprites from a sprite sheet file (png).
        // Note: The file specified must exist in a folder named Resources
        string spritesheetfolder = "";

        // Get the player's selected option
        int selectedOption = PlayerPrefs.GetInt("selectedOption");

        string spritesheetfilepath;

        // Choose the sprite sheet based on the selected option
        Debug.Log(selectedOption);
        if (selectedOption == 0)
        {
            spritesheetfilepath = spritesheetfolder + "chara_02/spritesheet";
        }
        else if (selectedOption == 1)
        {
            spritesheetfilepath = spritesheetfolder + "chara_01/spritesheet";
        }
        else
        {
            spritesheetfilepath = spritesheetfolder + "chara_03/spritesheet";
        }

        // Load the sprites from the chosen sprite sheet
        Sprite[] sprites = Resources.LoadAll<Sprite>(spritesheetfilepath);

        // Check if any sprites were loaded
        if (sprites.Length == 0)
        {
            Debug.LogWarning("No sprites found in the specified sprite sheet: " + spritesheetfilepath);
            return;
        }

        // Create a dictionary to store the sprites by name
        this.spriteSheet = sprites.ToDictionary(x => x.name, x => x);

        // Remember the name of the loaded sprite sheet
        this.LoadedSpriteSheetName = spritesheetfilepath;
    }

}