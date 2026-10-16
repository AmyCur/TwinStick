using System;
using Player;
using TMPro;
using UnityEngine.UI;

namespace UI{
    [Serializable]
    public class Bar{
        public Image bar;
        public TMP_Text barText;
        public float maxValue=100f;

        public void UpdateBar(float value){
            if(bar!=null) bar.fillAmount = value/maxValue;
            if(barText!=null) barText.text=value.ToString();
        }

        public Bar(float maxValue){
            this.maxValue=maxValue;
        }
    }
}