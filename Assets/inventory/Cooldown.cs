using Util;

namespace Inventory
{
    public class Cooldown{
        public int CDTime=300;
        public bool onCD;

        public void CD(){
            onCD=true;
            _ = TimeUtil.SetBoolAfterDelay(() => onCD=false, CDTime);     
        }

        public Cooldown(int CDTime=300, bool onCD = false){
            this.CDTime=CDTime;
            this.onCD=onCD;
        }
    }
}