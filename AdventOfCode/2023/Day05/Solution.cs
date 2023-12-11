namespace AdventOfCode.Y2023.Day05;

[ProblemName("If You Give A Seed A Fertilizer")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var instruction = GetInstruction(input);

        var locations = new List<long>();

        foreach (var seed in instruction.Seeds)
        {
            var val = instruction.Map(seed);

            locations.Add(val);
        }

        return locations.Min();
    }

    public object PartTwo(string input)
    {
        var instruction = GetInstruction(input);

        var seedRange = new SeedRange();

        for (var i = 0; i < instruction.Seeds.Count; i += 2)
        {
            seedRange.AddRange(instruction.Seeds[i], instruction.Seeds[i + 1]);
        }

        var location = 0L;
        var mappers = instruction.Mappers.ToList();
        mappers.Reverse();
        while (true)
        {
            var val = location;
            foreach (var mapper in mappers)
            {
                val = mapper.GetReverseMap(val);
            }

            if (seedRange.IsInRange(val))
                break;

            location++;
        }

        return location;
    }

    private class SeedRange
    {
        private List<(long, long)> Ranges { get; } = new();
        public void AddRange(long start, long length)
        {
            Ranges.Add((start, length));
        }

        public bool IsInRange(long value)
        {
            foreach (var (start, length) in Ranges)
            {
                if (value >= start && start + length > value)
                    return true;
            }
            return false;
        }
    }

    private Instruction GetInstruction(string input)
    {
        var sections = input.Split("\n\n");

        // Seeds
        var seeds = sections[0]
            .Split(" ")[1..]
            .Select(long.Parse)
            .ToList();

        var seedsToSoil = CreateMapper(sections[1]);
        var soilToFertilizer = CreateMapper(sections[2]);
        var fertilizerToWater = CreateMapper(sections[3]);
        var waterToLight = CreateMapper(sections[4]);
        var lightToTemperature = CreateMapper(sections[5]);
        var temperatureToHumidity = CreateMapper(sections[6]);
        var humidityToLocation = CreateMapper(sections[7]);

        return new Instruction(seeds, new List<Mapper> {
            seedsToSoil,
            soilToFertilizer,
            fertilizerToWater,
            waterToLight,
            lightToTemperature,
            temperatureToHumidity,
            humidityToLocation
        });
    }

    private Mapper CreateMapper(string section)
    {
        var lines = section.Split("\n");
        var mapper = new Mapper();

        for (var i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(" ");
            var destinationStart = long.Parse(parts[0]);
            var sourceStart = long.Parse(parts[1]);
            var length = long.Parse(parts[2]);
            mapper.AddMappedRange(new MapConfig(destinationStart, sourceStart, length));
        }

        return mapper;
    }

    private class Instruction
    {
        public Instruction(List<long> seeds,
            List<Mapper> mappers)
        {
            Seeds = seeds;
            Mappers = mappers;
        }

        public List<long> Seeds { get; }

        public List<Mapper> Mappers { get; }


        public long Map(long value)
        {
            foreach (var mapper in Mappers)
            {
                value = mapper.GetMappedValue(value);
            }

            return value;
        }
    }

    private record MapConfig(long DestinationStart, long SourceStart, long Length);

    private class Mapper
    {

        private readonly List<MapConfig> _map = new();

        public void AddMappedRange(MapConfig map)
        {
            _map.Add(map);
        }

        public long GetMappedValue(long value)
        {
            foreach (var map in _map)
            {
                if (value >= map.SourceStart && map.SourceStart + map.Length > value)
                    return value + map.DestinationStart - map.SourceStart;
            }

            return value;
        }

        public long GetReverseMap(long value)
        {
            foreach (var map in _map)
            {
                if (value >= map.DestinationStart && map.DestinationStart + map.Length > value)
                    return value + map.SourceStart - map.DestinationStart;
            }

            return value;
        }
    }
}
