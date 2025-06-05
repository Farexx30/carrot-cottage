using Godot;
using System;

namespace CarrotCottage.Scripts.Common;

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

public enum GrowthStates
{
    Seed = -1,
    Sprout,
    Vegetative,
    Reproductive,
    Mature,
    Harvestable,
    // Dead
}
