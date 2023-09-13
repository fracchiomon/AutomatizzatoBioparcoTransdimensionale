using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Legend : MonoBehaviour
{
    [SerializeField] private SO_NotaItem[] itemsToShow;
    [SerializeField] private UI_LegendItem prefabToAdd;
    [SerializeField] private Transform contentParent;

    void Start()
    {
        //per ogni elemento nell'array creo un bottone
        foreach (SO_NotaItem itemData in itemsToShow)
        {
            //UI_NotaButton button = Instantiate(this.notaButtonPrefab, this.contentParent);
            //button.Setup(this, itemData);
            AddItem(itemData);
        }
    }

    public void AddItem(SO_NotaItem itemData)
    {
        UI_LegendItem item = Instantiate(this.prefabToAdd, this.contentParent);
        item.Setup(this, itemData);
    }
}
