using System;
using System.Numerics;

class EllipticCurvePoint
{
    public BigInteger X { get; }
    public BigInteger Y { get; }

    // Конструктор для точок на еліптичній криві
    public EllipticCurvePoint(BigInteger x, BigInteger y)
    {
        X = x;
        Y = y;
    }

    // Перевірка, чи належить точка кривій
    public bool IsOnCurve(BigInteger p)
    {
        // Замініть на ваше рівняння еліптичної кривої
        BigInteger leftSide = (Y * Y) % p;
        BigInteger rightSide = (BigInteger.ModPow(X, 3, p) - X + 3) % p;

        return leftSide == rightSide;
    }

    // Додавання двох точок на еліптичній криві
    public static EllipticCurvePoint Add(EllipticCurvePoint p, EllipticCurvePoint q, BigInteger pValue)
    {
        BigInteger lambda;
        if (p.X == q.X && p.Y == q.Y)
        {
            // Дві однакові точки: подвоєння
            lambda = (3 * p.X * p.X + 1) * BigInteger.ModPow(2 * p.Y, pValue - 2, pValue);
        }
        else
        {
            // Різні точки: звичайне додавання
            BigInteger xDifference = (q.X - p.X + pValue) % pValue;
            BigInteger yDifference = (q.Y - p.Y + pValue) % pValue;

            lambda = (yDifference * BigInteger.ModPow(xDifference, pValue - 2, pValue)) % pValue;
        }

        BigInteger x3 = (lambda * lambda - p.X - q.X + 2 * pValue) % pValue;
        BigInteger y3 = (lambda * (p.X - x3) - p.Y + pValue) % pValue;

        return new EllipticCurvePoint(x3, y3);
    }
}

class Program
{
    static void Main()
    {
        // Задана еліптична крива
        BigInteger p = 127;

        // Задані точки P і Q
        EllipticCurvePoint P = new EllipticCurvePoint(71, 48);
        EllipticCurvePoint Q = new EllipticCurvePoint(75, 6);

        // Перевірка, чи точки належать кривій
        bool isPOnCurve = P.IsOnCurve(p);
        bool isQOnCurve = Q.IsOnCurve(p);

        Console.WriteLine($"Point P is on the curve: {isPOnCurve}");
        Console.WriteLine($"Point Q is on the curve: {isQOnCurve}");

        // Виконання операції додавання P + Q
        EllipticCurvePoint PPlusQ = EllipticCurvePoint.Add(P, Q, p);
        Console.WriteLine($"Result of P + Q: ({PPlusQ.X}, {PPlusQ.Y})");
    }
}
