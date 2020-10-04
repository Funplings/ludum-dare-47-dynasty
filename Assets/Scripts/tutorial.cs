using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public Sprite slide1;
    public Sprite slide2;
    public Sprite slide3;
    public Sprite[] slides;
    public int index = 0;
    private int size = 3;
    // Start is called before the first frame update

    private SpriteRenderer spriteRenderer;
    void Start()
    {
      spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
      Debug.Log(spriteRenderer);

      /*
      spriteRenderer.sprite = slide1;
      spriteRenderer.sprite = slide2;
      spriteRenderer.sprite = slide3;
      */
      slides = new Sprite[size];
      slides[0] = slide1;
      slides[1] = slide2;
      slides[2] = slide3;
      spriteRenderer.sprite = slides[index];
      Debug.Log(index);
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.RightArrow))
      {
        index++;
        Debug.Log(index);
        if (index == size)
        {
          index = 0;
        }
      }
      if (Input.GetKeyDown(KeyCode.LeftArrow))
      {
        index--;
        if (index == -1)
        {
          index = size - 1;
        }
      }
      if (Input.GetMouseButtonDown(0))
      {
        GameManager.instance.LoadGame();
      }
      spriteRenderer.sprite = slides[index];
      //Debug.Log(index);
    }
}
