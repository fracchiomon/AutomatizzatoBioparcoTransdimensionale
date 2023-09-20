using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "Recipes")]
public class SO_Recipe : ScriptableObject
{
    [SerializeField] private string _textRecipe;
    [SerializeField] private float _durataRecipe;
    [SerializeField] private int _restCounter;
    [SerializeField] private int _noteCounter;
    [SerializeField] private SO_NotaItem[] _ingredienti;

    public string textRecipe => this._textRecipe;
    public float durataRecipe => this._durataRecipe;
    public int restCounter => this._restCounter;
    public int noteCounter => this._noteCounter;
    public SO_NotaItem[] ingredienti => this._ingredienti;
}
