using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_windowDispensa : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI durataText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button addButton;
    [SerializeField] private FrullatoreController frullatore;
    [SerializeField] private UI_NotaButton notaButtonPrefab;
    [SerializeField] private Transform contentParent; //del button
    [SerializeField] private Transform spawnPoint;
    private SO_Recipe recipe;
    private UI_NotaButton[] buttons;

    //ho un riferimento al bottone selezionato
    private UI_NotaButton selectedBtn;

    private void Start()
    {
        this.recipe = FindObjectOfType<LevelManager>().ricette[0];

        if (this.frullatore == null)
        {
            this.frullatore = FindObjectOfType<FrullatoreController>();
        }

        //creo i bottoni
        this.buttons = new UI_NotaButton[this.recipe.ingredienti.Length];

        for (int i = 0; i < this.recipe.ingredienti.Length; i++)
        {
            //inizializzo i bottoni e li aggiungo all'array
            buttons[i] = AddBtn(this.recipe.ingredienti[i]);
        }

    }

    private void Update()
    {
         if(Input.GetKeyDown(KeyCode.Space) && this.addButton.interactable)
        {
            this.addButton.onClick.Invoke();
            Debug.Log("cristo");
        }
    }

    //funzione che instanzia il btn in base allo SO passato e aggiunge il btn all'array di bottoni
    public UI_NotaButton AddBtn(SO_NotaItem itemData)
    {
        UI_NotaButton btn = Instantiate(this.notaButtonPrefab, this.contentParent);
        btn.Setup(this, itemData);
        return btn;
    }

    //fzn che servirà ad aggiornare la grafica dei bottoni quando cambio ricetta/ingredienti
    public void UpdateIngredienti(SO_NotaItem[] itemsToAdd)
    {
        for(int i = 0; i< itemsToAdd.Length; i++)
        {
            this.buttons[i].Setup(this, itemsToAdd[i]);
            this.buttons[i].UpdateGraphics();
        }
    }

    public void OnSelectedItem(UI_NotaButton selectedBtn)
    {
        this.selectedBtn = selectedBtn;
        //il bottone può essere cliccato solo se ho premuto sull'obj
        //dovrebbe rimanere disabilitato anche nel caso in cui
        //il contenitore è pieno
        this.addButton.interactable = true;
        UpdateGraphics(selectedBtn.data);
    }
    //l'updateGraphics per il details panel che cambia in base al btn selezionato
    public void UpdateGraphics(SO_NotaItem data)
    {
        this.itemNameText.text = data.type;
        this.itemIcon.sprite = data.icon;
        this.durataText.text = data.stringDurata;
    }

    public void OnAddButtonClicked()
    {
        //this.selectedBtn.button.interactable = false;
        //tolgo il bottone dalla finestra se "lo inserisco"

        this.addButton.interactable = false;
        //GameObject toPop = Instantiate(this.selectedBtn.data.prefab, this.spawnPoint.position, this.spawnPoint.rotation);
        //Nota n = frullatore.GetNota(this.selectedBtn.data);
        frullatore.In(frullatore.GetNota(this.selectedBtn.data), this.selectedBtn.gameObject);
        //per non dover avere un riferimento al frullatore posso usare un action
    }
}
