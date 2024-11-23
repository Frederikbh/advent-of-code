using AdventOfCode.Lib;

namespace AdventOfCode._2015.Day22;

[ProblemName("Wizard Simulator 20XX")]
public class Solution : ISolver
{
    // Define the Spell class to hold spell properties
    private class Spell
    {
        public int Cost { get; }

        public int Damage { get; }

        public int Heal { get; }

        public int EffectTimer { get; }

        public string? EffectName { get; }

        public Spell(
            int cost,
            int damage = 0,
            int heal = 0,
            int effectTimer = 0,
            string? effectName = null)
        {
            Cost = cost;
            Damage = damage;
            Heal = heal;
            EffectTimer = effectTimer;
            EffectName = effectName;
        }
    }

    // Define the State struct to represent the game state
    private struct State : IEquatable<State>
    {
        public int PlayerHp;
        public int PlayerMana;
        public int BossHp;
        public int ShieldTimer;
        public int PoisonTimer;
        public int RechargeTimer;
        public int TotalManaSpent;

        public bool Equals(State other) =>
            PlayerHp == other.PlayerHp &&
            PlayerMana == other.PlayerMana &&
            BossHp == other.BossHp &&
            ShieldTimer == other.ShieldTimer &&
            PoisonTimer == other.PoisonTimer &&
            RechargeTimer == other.RechargeTimer;

        public override bool Equals(object? obj) => obj is State other && Equals(other);

        public override int GetHashCode() =>
            HashCode.Combine(PlayerHp, PlayerMana, BossHp, ShieldTimer, PoisonTimer, RechargeTimer);
    }

    // Define all available spells
    private readonly List<Spell> _spells =
    [
        new(53, 4),
        new(73, 2, 2),
        new(113, effectTimer: 6, effectName: "Shield"),
        new(173, effectTimer: 6, effectName: "Poison"),
        new(229, effectTimer: 5, effectName: "Recharge")
    ];

    public object PartOne(string input)
    {
        // Parse the input to get boss stats
        var bossStats = ParseInput(input);

        return Solve(bossStats);
    }

    public object PartTwo(string input)
    {
        // Parse the input to get boss stats
        var bossStats = ParseInput(input);

        return Solve(bossStats, 1);
    }

    private int Solve((int HitPoints, int Damage) bossStats, int damageTakenEachTurn = 0)
    {
        var bossHp = bossStats.HitPoints;
        var bossDamage = bossStats.Damage;

        // Initialize the priority queue with the initial state
        var initialState = new State
        {
            PlayerHp = 50,
            PlayerMana = 500,
            BossHp = bossHp,
            ShieldTimer = 0,
            PoisonTimer = 0,
            RechargeTimer = 0,
            TotalManaSpent = 0
        };

        // Priority queue ordered by TotalManaSpent
        var queue = new PriorityQueue<State, int>();
        queue.Enqueue(initialState, initialState.TotalManaSpent);

        // Dictionary to keep track of the least mana spent to reach a state
        var visited = new Dictionary<State, int>();

        var minimalManaToWin = int.MaxValue;

        while (queue.Count > 0)
        {
            var currentState = queue.Dequeue();

            currentState.PlayerHp -= damageTakenEachTurn;

            // If we've already found a better path to win, skip
            if (currentState.TotalManaSpent >= minimalManaToWin)
            {
                continue;
            }

            // Check if this state has been visited with less or equal mana
            if (visited.TryGetValue(currentState, out var recordedMana))
            {
                if (currentState.TotalManaSpent >= recordedMana)
                {
                    continue;
                }
            }

            // Record the mana spent for this state
            visited[currentState] = currentState.TotalManaSpent;

            // Player's turn: Apply effects
            var playerTurnState = ApplyEffects(currentState);

            // Check if boss is dead after effects
            if (playerTurnState.BossHp <= 0)
            {
                if (playerTurnState.TotalManaSpent < minimalManaToWin)
                {
                    minimalManaToWin = playerTurnState.TotalManaSpent;
                }

                continue;
            }

            // Try casting each spell
            foreach (var spell in _spells)
            {
                // Check if player has enough mana to cast the spell
                if (playerTurnState.PlayerMana < spell.Cost)
                {
                    continue;
                }

                // If the spell starts an effect that's already active, skip
                if (spell.EffectName == "Shield" && playerTurnState.ShieldTimer > 0)
                {
                    continue;
                }

                if (spell.EffectName == "Poison" && playerTurnState.PoisonTimer > 0)
                {
                    continue;
                }

                if (spell.EffectName == "Recharge" && playerTurnState.RechargeTimer > 0)
                {
                    continue;
                }

                // Clone the state for the new spell cast
                var newState = playerTurnState;
                newState.PlayerMana -= spell.Cost;
                newState.TotalManaSpent += spell.Cost;

                // Apply immediate effects
                newState.BossHp -= spell.Damage;
                newState.PlayerHp += spell.Heal;

                switch (spell.EffectName)
                {
                    // Start new effects if applicable
                    case "Shield":
                        newState.ShieldTimer = spell.EffectTimer;

                        break;
                    case "Poison":
                        newState.PoisonTimer = spell.EffectTimer;

                        break;
                    case "Recharge":
                        newState.RechargeTimer = spell.EffectTimer;

                        break;
                }

                // Check if boss is dead after spell
                if (newState.BossHp <= 0)
                {
                    if (newState.TotalManaSpent < minimalManaToWin)
                    {
                        minimalManaToWin = newState.TotalManaSpent;
                    }

                    continue;
                }

                // Boss's turn: Apply effects
                var bossTurnState = ApplyEffects(newState);

                // Check if boss is dead after effects
                if (bossTurnState.BossHp <= 0)
                {
                    if (bossTurnState.TotalManaSpent < minimalManaToWin)
                    {
                        minimalManaToWin = bossTurnState.TotalManaSpent;
                    }

                    continue;
                }

                // Boss attacks
                var playerArmor = bossTurnState.ShieldTimer > 0
                    ? 7
                    : 0;
                var damageDealt = Math.Max(1, bossDamage - playerArmor);
                bossTurnState.PlayerHp -= damageDealt;

                // Check if player is dead
                if (bossTurnState.PlayerHp <= 0)
                {
                    continue; // Player lost, skip this state
                }

                // Enqueue the new state
                queue.Enqueue(bossTurnState, bossTurnState.TotalManaSpent);
            }
        }

        return minimalManaToWin;
    }

    // Method to apply active effects at the start of each turn
    private static State ApplyEffects(State state)
    {
        var newState = state;

        // Apply Shield effect
        if (newState.ShieldTimer > 0)
        {
            // Shield effect grants armor, handled during attack
            newState.ShieldTimer--;
        }

        // Apply Poison effect
        if (newState.PoisonTimer > 0)
        {
            newState.BossHp -= 3;
            newState.PoisonTimer--;
        }

        // Apply Recharge effect
        if (newState.RechargeTimer > 0)
        {
            newState.PlayerMana += 101;
            newState.RechargeTimer--;
        }

        return newState;
    }

    // Method to parse the input and extract boss stats
    private static (int HitPoints, int Damage) ParseInput(string input)
    {
        var hitPoints = 0;
        var damage = 0;

        var lines = input.Split(
            [
                '\n',
                '\r'
            ],
            StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            if (line.StartsWith("Hit Points:"))
            {
                hitPoints = int.Parse(
                    line.Split(':')[1]
                        .Trim());
            }
            else if (line.StartsWith("Damage:"))
            {
                damage = int.Parse(
                    line.Split(':')[1]
                        .Trim());
            }
        }

        return (hitPoints, damage);
    }
}
