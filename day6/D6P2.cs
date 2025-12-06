using shared;

namespace day6;

public static class D6P2
{
    public static object Part2Answer(this string input)
    {
        var map = input
            .ParseUntrimmedMap();

        var height = map.Grid.Length;
        var maxX = map.Grid[0].Length;

        var operatorRow = height - 1;

        long sum = 0L;

        List<long> operands = [];

        for (int x = maxX - 1; x >= 0; x--)
        {
            string number = "";
            for (int y = 0; y < operatorRow; y++)
            {
                var pos = new Pos(x, y);
                if (map.Contains(pos))
                {
                    var cel = map.Get(pos);
                    if (cel != ' ')
                        number = number + cel;
                }
            }

            if (number.Length == 0)
                continue;
            
            var operand = long.Parse(number.Trim());
            operands.Add(operand);

            var operatorPos = new Pos(x, operatorRow);
            if (!map.Contains(operatorPos))
                continue;

            var operatorOrNot = map.Get(operatorPos);
            if (operatorOrNot == '*')
            {
                var a = operands.Times();
                sum += a;
                operands.Clear();
            }
            else if (operatorOrNot == '+')
            {
                var a = operands.Sum();
                sum += a;
                operands.Clear();
            }
        }

        return sum;
    }
}
