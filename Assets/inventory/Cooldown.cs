using Util;

namespace Inventory{
    public class Cooldown{
        public bool onCD;
        public int CDTime;

        public void CD(){
            onCD=true;
            _ = TimeUtil.SetBoolAfterDelay(() => onCD=false, CDTime);
        }

        public Cooldown(bool onCD=false, int CDTime=1000){
            this.onCD=onCD;
            this.CDTime=CDTime;
        }
    }
}