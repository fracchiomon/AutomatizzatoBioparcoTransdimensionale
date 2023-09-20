
using UnityEngine;

public class Nota : MonoBehaviour
{
    private float _durata;
    private string _nome;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private SO_NotaItem _notaSO;

    public SO_NotaItem notaSO => this._notaSO;

    public float durata
    {
        get
        {
            return this._durata;
        }
        set
        {
            this._durata = value;
        }
    }

    public string nome
    {
        get
        {
            return this._nome;
        }
        set
        {
            this._nome = value;
        }
    }

    public GameObject prefab
    {
        get
        {
            return this._prefab;
        }
        set
        {
            this._prefab = value;
        }
    }

    private void Awake()
    {
        this._durata = this._notaSO.durata;
        this._nome = this._notaSO.type;
    }
}
