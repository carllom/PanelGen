using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace PanelGen.Cli
{
    public class Type1Font
    {
        private readonly string _path;
        public Type1Font(string fontFile)
        {
            _path = fontFile;
        }

        public void Parse()
        {
            using (var fs = File.OpenText(_path))
            {
                while (!fs.EndOfStream)
                {
                    var line = fs.ReadLine();

                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Comments
                    if (line.StartsWith("%"))
                    {
                        System.Diagnostics.Debug.Write($"Comment: {line}");
                        continue;
                    }

                    if (line.StartsWith("/"))
                    {
                        // Definition
                    }
                    else
                    {
                        // Start or end of a definition
                        var tokens = line.Split(' ');
                    }
                }
            }
        }

        ushort _r;
        private const ushort C1 = 52845;
        private const ushort C2 = 22719;

        private byte Decrypt(byte cipher)
        {
            var plain = (byte)(cipher ^ (_r>>8));
            _r = (byte)((cipher + _r) * C1 + C2);
            return plain;
        }
    }

    public class PostScriptEngine
    {
        private readonly Stack<object> _oper = new Stack<object>();
        private readonly Stack<Dictionary<string, object>> _dict = new Stack<Dictionary<string, object>>();
        private Stack<object> _exec = new Stack<object>();

        public PostScriptEngine()
        {
            
        }

        public void PushToken(string token)
        {
            switch (token.ToLower())
            {
                case "dict": // Create dictionary  dict(size)
                    var size = int.Parse(_oper.Pop().ToString());
                    _oper.Push(new Dictionary<string, object>());
                    break;
                case "begin": // Start dictionary (push to _dict stack)
                    _dict.Push((Dictionary<string,object>)_oper.Pop());
                    break;
                case "end": // End dictionary (pop from _dict stack)
                    var dict = _dict.Pop();
                    break;
                case "def": // Dict entry definition def(value,key);
                    var value = _oper.Pop();
                    var key = _oper.Pop().ToString();
                    _dict.Peek()[key] = value;
                    //TODO
                    break;
                case "readonly": // Skip this token
                    break;
            }
        }
    }
}
