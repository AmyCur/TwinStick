namespace GameDebug
{
    public static class CombatDebug
    {
        public static bool shouldDrawHurtBoxes=true;
        public const int HURTBOXLINGERTIME=300; // In MS

        public static bool shouldDrawEnemyAttackRays=true;
        public static bool enemiesShouldBroadcastState=false;

        public static bool logGrounded=false;

        public static bool forgetClickedItemAfterTime=true;
        public static float timeToForgetClickedItem = 8f;

        public static bool drawEnemySpawnSpheres=true;
    }
}