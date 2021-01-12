using System;
using System.IO;

namespace ParseMyCSV
{
    public class ConsoleTest
    {
        /// This class redirects Console.Out so that it can be tested against. Usage:
        ///
        /// <code>
        /// using val output = new ConsoleOutput();
        /// // Something that prints stuff...
        /// output.GetOutput(); // Returns what was printed
        /// </code>
        ///
        /// Source: https://stackoverflow.com/a/13397075
        public class ConsoleOutput : IDisposable
        {
            private readonly StringWriter _stringWriter;
            private readonly TextWriter _originalOutput;

            public ConsoleOutput()
            {
                _stringWriter = new StringWriter();
                _originalOutput = Console.Out;
                Console.SetOut(_stringWriter);
            }

            public string GetOutput()
            {
                return _stringWriter.ToString();
            }

            public void Dispose()
            {
                Console.SetOut(_originalOutput);
                _stringWriter.Dispose();
            }
        }
        
        /// This class redirects Console.Error so that it can be tested against. Usage:
        ///
        /// <code>
        /// using val output = new ConsoleError();
        /// // Something that prints stuff...
        /// output.GetError(); // Returns what was printed
        /// </code>
        ///
        /// Inspired by: https://stackoverflow.com/a/13397075
        public class ConsoleError : IDisposable
        {
            private readonly StringWriter _stringWriter;
            private readonly TextWriter _originalOutput;

            public ConsoleError()
            {
                _stringWriter = new StringWriter();
                _originalOutput = Console.Error;
                Console.SetError(_stringWriter);
            }

            public string GetError()
            {
                return _stringWriter.ToString();
            }

            public void Dispose()
            {
                Console.SetError(_originalOutput);
                _stringWriter.Dispose();
            }
        }
        
        /// This class hijacks Console.In so that it can be tested against. Usage:
        ///
        /// <code>
        /// using val output = new ConsoleInput("This is my stdin" + Environment.NewLine);
        /// // Something that takes stuff from stdin...
        /// </code>
        /// 
        /// Inspired by: https://stackoverflow.com/a/13397075
        ///
        public class ConsoleInput : IDisposable
        {
            private readonly StringReader _stringReader;
            private readonly TextReader _originalInput;

            public ConsoleInput(string input)
            {
                _stringReader = new StringReader(input);
                _originalInput = Console.In;
                Console.SetIn(_stringReader);
            }

            public void Dispose()
            {
                Console.SetIn(_originalInput);
                _stringReader.Dispose();
            }
        }
    }
}