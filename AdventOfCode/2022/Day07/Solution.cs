namespace AdventOfCode.Y2022.Day07;

[ProblemName("No Space Left On Device")]
internal class Solution : ISolver 
{

    public object PartOne(string input) 
    {
        var root = ParseInput(input);

        var foldersToCheck = new List<Folder>
        {
            root
        };

        var sum = 0;
        while (foldersToCheck.Any())
        {
            var check = foldersToCheck.First();
            foldersToCheck.RemoveAt(0);
            var folderSize = check.GetSize();
            if (folderSize <= 100000)
                sum += folderSize;

            foldersToCheck.AddRange(check.GetSubFolders());
        }

        return sum;
    }

    public object PartTwo(string input) 
    {
        var root = ParseInput(input);

        var memoryToFree = root.GetSize() - 70000000 + 30000000;
        
        var foldersToCheck = new List<Folder>
        {
            root
        };
        var min = int.MaxValue;
        while (foldersToCheck.Count > 0)
        {
            var check = foldersToCheck.First();
            foldersToCheck.RemoveAt(0);

            var folderSize = check.GetSize();
            if (folderSize >= memoryToFree)
                min = Math.Min(min, folderSize);

            foldersToCheck.AddRange(check.GetSubFolders());
        }

        return min;
    }

    private Folder ParseInput(string input)
    {
        var commands = input.Split("\n");
        var root = new Folder("root", null);

        var current = root;
        for (var i = 1; i < commands.Length; i++)
        {
            var command = commands[i];

            if (command == "$ cd ..")
                current = current.Parent ?? throw new Exception("Cannot go up from root");
            else if (command.StartsWith("$ cd "))
            {
                var name = command.Split(" ", 3)[2];
                current = current.GetOrCreateSubFolder(name);
            }
            else if (command.StartsWith("dir "))
            {
                var folderName = command.Split(" ", 2)[1];
                current.GetOrCreateSubFolder(folderName);
            }
            else if (char.IsNumber(command[0]))
            {
                var split = command.Split(" ", 2);
                var file = new File(split[1], int.Parse(split[0]));
                current.Add(file);
            }
        }

        return root;
    }

    private interface IHasSize
    {
        int GetSize();

        string GetName();
    }

    private record Folder(string Name, Folder? Parent) : IHasSize
    {
        private readonly List<IHasSize> _children = new();

        public void Add(IHasSize child)
        {
            _children.Add(child);
        }

        public List<Folder> GetSubFolders() => _children.OfType<Folder>().ToList();

        public Folder GetOrCreateSubFolder(string name)
        {
            var folder = _children.OfType<Folder>().FirstOrDefault(e => e.GetName() == name);

            if (folder == null)
            {
                folder = new Folder(name, this);
                _children.Add(folder);
            }

            return folder;
        }

        public int GetSize() => _children.Sum(x => x.GetSize());

        public string GetName()
        {
            return Name;
        }
    }

    private record File(string Name, int Size) : IHasSize
    {
        public int GetSize() => Size;

        public string GetName()
        {
            return Name;
        }
    }
}
