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

        public const string TillingFront = "TillingFront";
        public const string TillingBack = "TillingBack";
        public const string TillingLeft = "TillingLeft";
        public const string TillingRight = "TillingRight";

        public const string WateringFront = "WateringFront";
        public const string WateringBack = "WateringBack";
        public const string WateringLeft = "WateringLeft";
        public const string WateringRight = "WateringRight";
    }

    public static class States
    {
        public const string Idle = "Idle";
        public const string Walk = "Walk";
        public const string Chopping = "Chopping";
        public const string Tilling = "Tilling";
        public const string Watering = "Watering";
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
