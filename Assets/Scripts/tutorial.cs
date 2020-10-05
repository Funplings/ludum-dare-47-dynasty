using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Sprite[] slides;
    [SerializeField] private TMP_Text pageNumber;

    private int index = 0;
    private int max;
    private Image background;

    void Start()
    {
      background = GetComponent<Image>();
      max = slides.Length;
      background.sprite = slides[index];
      UpdatePage();
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.RightArrow))
      {
        MoveRight();
      }
      if (Input.GetKeyDown(KeyCode.LeftArrow))
      {
        MoveLeft();
      }
    }

    public void MoveRight(){
      AudioManager.instance.Play("Select");
      index++;
      if(index == max){
        GameManager.instance.LoadMainMenu();
        return;
      }
      UpdatePage();
    }

    public void MoveLeft(){
      AudioManager.instance.Play("Select");
      index = Mathf.Max(0, index - 1);
      UpdatePage();
    }

    private void UpdatePage(){
      if(index >= max) return;
      background.sprite = slides[index];
      pageNumber.text = string.Format("{0}/{1}", index + 1, max);
    }

}
