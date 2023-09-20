
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_MenuButton : MonoBehaviour
{
    public void ToMainMenu()
    {
        this.GetComponentInChildren<Button>().interactable = false;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SongManager.Instance.StopSong();
        }
            
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
            

        GiocoManager.Instance.ToMainMenu();
    }

}
