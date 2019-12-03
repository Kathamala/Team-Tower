using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "New Character", menuName = "PlayableCharacter")]
public class PlayableCharacter: ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
    public GameObject characterPrefab;
    [NonSerialized] public bool selectable = true;
    [NonSerialized] public Gamepad currentGamepad;
    [NonSerialized] public Vector2 position;

}
