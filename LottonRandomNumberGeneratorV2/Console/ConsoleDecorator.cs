public class ConsoleDecorator
{
    public void WriteLine(string line, WriteLineSeparator writeLineSeparator = WriteLineSeparator.None)
    {
        if (!string.IsNullOrWhiteSpace(line))
        {
            if (writeLineSeparator == WriteLineSeparator.Before || writeLineSeparator == WriteLineSeparator.Both)
            {
                Console.WriteLine("---");
            }

            Console.WriteLine(line);

            if (writeLineSeparator == WriteLineSeparator.After || writeLineSeparator == WriteLineSeparator.Both)
            {
                Console.WriteLine("---");
            }
        }
    }

    public void WriteLineMultiple<T>(IEnumerable<T> lines, Func<T, string> value, bool isIndexIncluded = true, WriteLineSeparator writeLineSeparator = WriteLineSeparator.None)
    {
        if (writeLineSeparator == WriteLineSeparator.Before || writeLineSeparator == WriteLineSeparator.Both)
        {
            Console.WriteLine("---");
        }

        int i = 0;
        foreach (var line in lines)
        {
            i++;
            this.WriteLine(isIndexIncluded ? $"{i}. " + value.Invoke(line) : value.Invoke(line));
        }

        if (writeLineSeparator == WriteLineSeparator.After || writeLineSeparator == WriteLineSeparator.Both)
        {
            Console.WriteLine("---");
        }
    }

    public void Write(string line)
    {
        if (!string.IsNullOrWhiteSpace(line))
        {
            Console.Write(line);
        }
    }

    public string? ReadLine()
    {
        return Console.ReadLine();
    }

    public ConsoleKeyInfo ReadKey()
    {
        return Console.ReadKey();
    }
}