using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Nota")]
//serve per aggiungere "il menu crea" al create
//lo scriptable object è un asset e non un game object!
public class SO_NotaItem : ScriptableObject 
{
    //servono private con properties
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _type;
    [SerializeField] private float _durata;
    [SerializeField] private string _stringDurata;
    [SerializeField] private bool _isBreak;
    [SerializeField] private Nota _prefab;

    public Nota prefab => this._prefab;

    public string stringDurata => this._stringDurata;

    public bool isBreak => this._isBreak;

    public Sprite icon
    {
        get
        {
            return this._icon;
        }
        set
        {
            this._icon = value;
        }
    }

    public string type
    {
        get
        {
            return this._type;
        }
        set
        {
            this._type = value;
        }
    }
    //è un get sintetizzato (property)
    public float durata => this._durata;

}
