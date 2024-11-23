using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day11
{
    [ProblemName("Radioisotope Thermoelectric Generators")]
    public class Solution : ISolver
    {
        // Element indices
        internal enum Element
        {
            Promethium = 0,
            Cobalt,
            Curium,
            Ruthenium,
            Plutonium,
            Elerium,
            Dilithium
        }

        // Total number of elements for state representation
        private const int TotalElements = 5; // Adjust to 7 if including Elerium and Dilithium in Part One

        public object PartOne(string input)
        {
            // Initialize state for Part One
            // Floor numbering: 0 = first floor, 1 = second floor, etc.
            // Initial State based on the problem description:
            // Floor 0: Promethium Generator (PG), Promethium Microchip (PM)
            // Floor 1: Cobalt Generator (CG), Curium Generator (CuG), Ruthenium Generator (RG), Plutonium Generator (PG)
            // Floor 2: Cobalt Microchip (CM), Curium Microchip (CuM), Ruthenium Microchip (RM), Plutonium Microchip (PM)
            // Floor 3: Empty

            var initialState = new State(
                0,
                new int[TotalElements],
                new int[TotalElements]
            );

            // Setting initial positions
            // Promethium
            initialState = initialState.SetChip(Element.Promethium, 0)
                .SetGenerator(Element.Promethium, 0);

            // Cobalt
            initialState = initialState.SetGenerator(Element.Cobalt, 1)
                .SetChip(Element.Cobalt, 2);

            // Curium
            initialState = initialState.SetGenerator(Element.Curium, 1)
                .SetChip(Element.Curium, 2);

            // Ruthenium
            initialState = initialState.SetGenerator(Element.Ruthenium, 1)
                .SetChip(Element.Ruthenium, 2);

            // Plutonium
            initialState = initialState.SetGenerator(Element.Plutonium, 1)
                .SetChip(Element.Plutonium, 2);

            return Solve(initialState);
        }

        public object PartTwo(string input)
        {
            // Extend the initial state by adding Elerium and Dilithium
            // All new items start on the first floor (floor 0)

            var initialState = new State(
                0,
                new int[7],
                new int[7]
            );

            // Promethium
            initialState = initialState.SetChip(Element.Promethium, 0)
                .SetGenerator(Element.Promethium, 0);

            // Cobalt
            initialState = initialState.SetGenerator(Element.Cobalt, 1)
                .SetChip(Element.Cobalt, 2);

            // Curium
            initialState = initialState.SetGenerator(Element.Curium, 1)
                .SetChip(Element.Curium, 2);

            // Ruthenium
            initialState = initialState.SetGenerator(Element.Ruthenium, 1)
                .SetChip(Element.Ruthenium, 2);

            // Plutonium
            initialState = initialState.SetGenerator(Element.Plutonium, 1)
                .SetChip(Element.Plutonium, 2);

            // Elerium
            initialState = initialState.SetChip(Element.Elerium, 0)
                .SetGenerator(Element.Elerium, 0);

            // Dilithium
            initialState = initialState.SetChip(Element.Dilithium, 0)
                .SetGenerator(Element.Dilithium, 0);

            return Solve(initialState);
        }

        private static int Solve(State initialState)
        {
            var queue = new Queue<State>();
            var seen = new HashSet<State>();
            queue.Enqueue(initialState);
            seen.Add(initialState.Normalize());

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current.IsFinalState())
                {
                    return current.Steps;
                }

                foreach (var next in current.GetNextStates())
                {
                    var normalized = next.Normalize();

                    if (seen.Add(normalized))
                    {
                        queue.Enqueue(next);
                    }
                }
            }

            return -1; // If no solution is found
        }

        /// <summary>
        /// Represents the state of the building.
        /// </summary>
        internal readonly struct State : IEquatable<State>
        {
            public int Elevator { get; }

            public int[] Chips { get; } // Floor positions for each chip

            public int[] Generators { get; } // Floor positions for each generator

            public int Steps { get; }

            public State(int elevator, int[] chips, int[] generators, int steps = 0)
            {
                Elevator = elevator;
                Chips = (int[])chips.Clone();
                Generators = (int[])generators.Clone();
                Steps = steps;
            }

            /// <summary>
            /// Sets the floor of a specific chip.
            /// </summary>
            public State SetChip(Element element, int floor)
            {
                var newChips = (int[])Chips.Clone();
                newChips[(int)element] = floor;

                return new State(Elevator, newChips, Generators, Steps);
            }

            /// <summary>
            /// Sets the floor of a specific generator.
            /// </summary>
            public State SetGenerator(Element element, int floor)
            {
                var newGenerators = (int[])Generators.Clone();
                newGenerators[(int)element] = floor;

                return new State(Elevator, Chips, newGenerators, Steps);
            }

            /// <summary>
            /// Determines if the current state is the final state (all items on the top floor).
            /// </summary>
            public bool IsFinalState()
            {
                var topFloor = 3; // Floors are 0-indexed: 0 to 3

                foreach (var chipFloor in Chips)
                {
                    if (chipFloor != topFloor)
                    {
                        return false;
                    }
                }

                foreach (var generatorFloor in Generators)
                {
                    if (generatorFloor != topFloor)
                    {
                        return false;
                    }
                }

                return true;
            }

            /// <summary>
            /// Generates all possible next states from the current state.
            /// </summary>
            public IEnumerable<State> GetNextStates()
            {
                var currentFloorItems = GetItemsOnFloor(Elevator);
                var possibleMoves = GetPossibleMoves(currentFloorItems);

                foreach (var move in possibleMoves)
                {
                    foreach (var direction in GetPossibleDirections())
                    {
                        var newFloor = Elevator + direction;

                        if (newFloor < 0 || newFloor > 3)
                        {
                            continue;
                        }

                        var newState = MoveItems(newFloor, move);

                        if (newState.IsValid())
                        {
                            yield return new State(newFloor, newState.Chips, newState.Generators, Steps + 1);
                        }
                    }
                }
            }

            /// <summary>
            /// Determines the possible directions the elevator can move.
            /// Prefer moving up, but allow moving down if necessary.
            /// </summary>
            private IEnumerable<int> GetPossibleDirections()
            {
                if (Elevator == 0)
                {
                    yield return 1; // Can only move up
                }
                else if (Elevator == 3)
                {
                    yield return -1; // Can only move down
                }
                else
                {
                    yield return 1;
                    yield return -1;
                }
            }

            /// <summary>
            /// Moves specified items to the new floor.
            /// </summary>
            private State MoveItems(int newFloor, (Element, bool)[] itemsToMove)
            {
                var newChips = (int[])Chips.Clone();
                var newGenerators = (int[])Generators.Clone();

                foreach (var (element, isGenerator) in itemsToMove)
                {
                    if (isGenerator)
                    {
                        newGenerators[(int)element] = newFloor;
                    }
                    else
                    {
                        newChips[(int)element] = newFloor;
                    }
                }

                return new State(newFloor, newChips, newGenerators, Steps + 1);
            }

            /// <summary>
            /// Retrieves all items on a specific floor.
            /// </summary>
            private (Element, bool)[] GetItemsOnFloor(int floor)
            {
                var items = new List<(Element, bool)>();

                for (var i = 0; i < Chips.Length; i++)
                {
                    if (Chips[i] == floor)
                    {
                        items.Add(((Element)i, false));
                    }

                    if (Generators[i] == floor)
                    {
                        items.Add(((Element)i, true));
                    }
                }

                return items.ToArray();
            }

            /// <summary>
            /// Generates all possible combinations of one or two items to move.
            /// </summary>
            private IEnumerable<(Element, bool)[]> GetPossibleMoves((Element, bool)[] items)
            {
                // Generate all single item moves
                foreach (var item in items)
                {
                    yield return
                    [
                        item
                    ];
                }

                // Generate all two-item moves
                for (var i = 0; i < items.Length; i++)
                {
                    for (var j = i + 1; j < items.Length; j++)
                    {
                        yield return
                        [
                            items[i],
                            items[j]
                        ];
                    }
                }
            }

            /// <summary>
            /// Checks if the state is valid (no microchips are fried).
            /// </summary>
            public bool IsValid()
            {
                for (var floor = 0; floor < 4; floor++)
                {
                    var hasGenerator = false;

                    foreach (var gen in Generators)
                    {
                        if (gen == floor)
                        {
                            hasGenerator = true;

                            break;
                        }
                    }

                    if (!hasGenerator)
                    {
                        continue;
                    }

                    for (var i = 0; i < Chips.Length; i++)
                    {
                        if (Chips[i] == floor && Generators[i] != floor)
                        {
                            return false; // Microchip is fried
                        }
                    }
                }

                return true;
            }

            /// <summary>
            /// Normalizes the state by sorting the pairs of chips and generators.
            /// This reduces the number of unique states by treating equivalent states as identical.
            /// </summary>
            public State Normalize()
            {
                var pairs = new List<(int chip, int generator)>();

                for (var i = 0; i < Chips.Length; i++)
                {
                    pairs.Add((Chips[i], Generators[i]));
                }

                // Sort the pairs to ensure equivalent states are treated the same
                pairs.Sort();

                var normalizedChips = new int[pairs.Count];
                var normalizedGenerators = new int[pairs.Count];

                for (var i = 0; i < pairs.Count; i++)
                {
                    normalizedChips[i] = pairs[i].chip;
                    normalizedGenerators[i] = pairs[i].generator;
                }

                return new State(Elevator, normalizedChips, normalizedGenerators, Steps);
            }

            public bool Equals(State other)
            {
                if (Elevator != other.Elevator)
                {
                    return false;
                }

                for (var i = 0; i < Chips.Length; i++)
                {
                    if (Chips[i] != other.Chips[i])
                    {
                        return false;
                    }

                    if (Generators[i] != other.Generators[i])
                    {
                        return false;
                    }
                }

                return true;
            }

            public override bool Equals(object? obj) => obj is State other && Equals(other);

            public override int GetHashCode()
            {
                var hash = Elevator;

                for (var i = 0; i < Chips.Length; i++)
                {
                    hash = hash * 31 + Chips[i];
                    hash = hash * 31 + Generators[i];
                }

                return hash;
            }
        }
    }
}
