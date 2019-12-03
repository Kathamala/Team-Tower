using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{
    public PlayableCharacter[] characters;
    public GameObject characterUIprefab;
    // Start is called before the first frame update
    void Start()
    {
        foreach (PlayableCharacter character in characters)
        {
            SpawnCharacterCell(character);
        }
    }

    private void SpawnCharacterCell(PlayableCharacter character)
    {
        GameObject characterUI = Instantiate(characterUIprefab, transform);
        characterUI.name = character.characterName;
        Image image = characterUI.transform.Find("Image").GetComponent<Image>();
        image.sprite = character.characterSprite;
        TextMeshProUGUI text = characterUI.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        text.text = character.characterName;
    }
}
