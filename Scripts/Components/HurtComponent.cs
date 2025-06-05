using CarrotCottage.Scripts.Characters.PlayerScripts;
using CarrotCottage.Scripts.Common;
using Godot;
using System.Threading.Tasks;

namespace CarrotCottage.Scripts.Components;

public partial class HurtComponent : Area2D
{
    [Export]
    public PlayerTools Tool { get; set; } = PlayerTools.None;

    [Signal]
    public delegate void HurtEventHandler(int damage);

    public void OnAreaEntered(Area2D area2D)
    {
        if (area2D is HitComponent hitComponent)
        {
            if (Tool == hitComponent.CurrentTool)
            {
                EmitSignal(SignalName.Hurt, hitComponent.Damage);
            }
        }
    }
}
