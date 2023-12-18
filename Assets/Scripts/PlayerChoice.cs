using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoice : MonoBehaviour
{
    public CharacterDatabase characterDB;
    //public Text nameText;
    public SpriteRenderer artworkSprite;

    private int selectedOption = 0;

    void Start()
    {
       if(!PlayerPrefs.HasKey("selectedOption"))
       {
            selectedOption = 0;
       }

       else
       {
            Load();
       }

       UpdateCharacter(selectedOption);
    }
    private void UpdateCharacter(int selectedOption)
    {
         Character character = characterDB.GetCharacter(selectedOption);
         artworkSprite.sprite = character.characterSprite;
         //nameText.text = character.characterName;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

}
