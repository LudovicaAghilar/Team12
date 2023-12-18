using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldScript : MonoBehaviour
{
    public CharacterManager character;

    public void SaveName() {
        character.name = GetComponent<TMP_InputField>().text;
    }
}
