using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DynastyListElement : MonoBehaviour
{
    [SerializeField] TMP_Text number;
    [SerializeField] TMP_Text name;
    [SerializeField] TMP_Text timeline;

    public void SetInfo(int number, string name, int yearStart, int yearEnd)
    {
        this.number.text  = "Dynasty " + number.ToString();
        this.name.text = string.Format("The <i>{0}</i> Dynasty", name);
        this.timeline.text = string.Format("Year {0} - Year {1}", yearStart.ToString("D4"), yearEnd.ToString("D4"));
    }
}
