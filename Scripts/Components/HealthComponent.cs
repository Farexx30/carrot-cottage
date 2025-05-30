using Godot;

namespace CarrotCottage.Scripts.Components;

public partial class HealthComponent : Node2D
{
    [Export]
    public int MaxHealth { get; set; } = 1;

    private int _currentHealth;

    [Signal]
    public delegate void NoHealthReachedEventHandler();

    public override void _Ready()
    {
        _currentHealth = MaxHealth;
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, MaxHealth);

        if (_currentHealth == 0)
        {
            EmitSignal(SignalName.NoHealthReached);
        }
    }
}
