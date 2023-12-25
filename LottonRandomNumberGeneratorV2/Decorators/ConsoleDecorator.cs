﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottonRandomNumberGeneratorV2.Helpers
{
    public class ConsoleDecorator
    {
        public void WriteLine(string line, WriteLineSeparator writeLineSeparator = WriteLineSeparator.None)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                if(writeLineSeparator == WriteLineSeparator.Before || writeLineSeparator == WriteLineSeparator.Both)
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
    }
}
