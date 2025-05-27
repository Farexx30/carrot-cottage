namespace CarrotCottage.Scripts.Characters.PlayerScripts;

public static class PlayerConstants
{
    public static class Animations
    {
        public const string IdleFront = "IdleFront";
        public const string IdleBack = "IdleBack";
        public const string IdleLeft = "IdleLeft";
        public const string IdleRight = "IdleRight";

        public const string WalkFront = "WalkFront";
        public const string WalkBack = "WalkBack";
        public const string WalkLeft = "WalkLeft";
        public const string WalkRight = "WalkRight";

        public const string ChoppingFront = "ChoppingFront";
        public const string ChoppingBack = "ChoppingBack";
        public const string ChoppingLeft = "ChoppingLeft";
        public const string ChoppingRight = "ChoppingRight";
    }

    public static class States
    {
        public const string Idle = "Idle";
        public const string Walk = "Walk";
        public const string Chopping = "Chopping";
    }
}

public enum PlayerTools
{
    None,
    Axe,
    Hoe,
    WateringCan,
    CornSeeds,
    CarrotSeeds,
    CauliflowerSeeds,
    TomatoSeeds,
    EggplantSeeds,
    LettuceSeeds,
    WheatSeeds,
    PumpkinSeeds,
    ParsleySeeds,
    RadishSeeds,
    CucumberSeeds
}
