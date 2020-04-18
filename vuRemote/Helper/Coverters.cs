using System;

namespace vuRemote
{
    public class Converters
    {
        public Converters() { }
        public static int StrToIntDef(string InputNumber, int _default)
        {
            //convert Text to Int
            int Number = _default;
            if (Int32.TryParse(InputNumber, out Number))
                return Number;
            else
                // Konvertierungsfehler, dann Default zurückliefern
                return _default;
        }

        public static bool NumberIsEven(int Number)
        {
            return (Number%2) == 0;
        }

        public static bool NumberIsEven(string Number)
        {
            return (StrToIntDef(Number, -1) % 2) == 0;
        }

    }
}
