using System.Collections;
using Combat;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Inventory
{
    [System.Serializable]
    public class Cooldown{
        public float CDTime;
        public bool onCD;
        [HideInInspector] public Image cdBar;
        Coroutine cdRoutine;

        IEnumerator HandleCD(){
            
            onCD=true;
            
            for(int i = 0; i < 100; i++)
            {
                cdBar.fillAmount = (float)i/100;
                yield return new WaitForSeconds(CDTime/100/WeaponsGlobals.attackSpeed);
            }
            
            cdBar.fillAmount = 0;

            onCD=false;
        }


        public void CD(){
            cdRoutine = PlayerController.Instance.StartCoroutine(HandleCD());
        }

        public Cooldown(Image cdBar, float CDTime=3f, bool onCD = false){
            this.CDTime=CDTime;
            this.onCD=onCD;
            this.cdBar=cdBar;
        }
    }
}