namespace GU1;

public class LargeNumber {

    readonly byte[] number = new byte[1024];

    #region From methods

    public static LargeNumber FromInt(int value)
    {
        LargeNumber result = new();
        for (int i = 0; i < 1024; i++)
        {
            result.number[i] = 0;
        }
        int j = 0;
        while (value > 0)
        {
            result.number[j] = (byte)(value % 10);
            value /= 10;
            j++;
        }
        return result;
    }

    public static LargeNumber FromString(string value)
    {
        LargeNumber result = new();
        for (int i = 0; i < 1024; i++)
        {
            result.number[i] = 0;
        }
        int j = 0;
        for (int i = value.Length - 1; i >= 0; i--)
        {
            result.number[j] = (byte)(value[i] - '0');
            j++;
        }
        return result;
    }

    #endregion

    #region Constructors

    public LargeNumber() {

        for (int i = 0; i < 1024; i++)
            number[i] = 0;
    }

    public LargeNumber(int value) {

        for (int i = 0; i < 1024; i++)
            number[i] = 0;

        int j = 0;
        while (value > 0) {

            number[j] = (byte)(value % 10);
            value /= 10;
            j++;
        }
    }

    public LargeNumber(string value) {

        for (int i = 0; i < 1024; i++)
            number[i] = 0;

        int j = 0;
        for (int i = value.Length - 1; i >= 0; i--) {

            number[j] = (byte)(value[i] - '0');
            j++;
        }
    }

    #endregion

    #region Add

    public void Add(LargeNumber other) {

        byte carry = 0;
        for (int i = 0; i < 1024; i++) {

            int sum = number[i] + other.number[i] + carry;
            number[i] = (byte)(sum % 10);
            carry = (byte)(sum / 10);
        }
    }

    public static LargeNumber operator +(LargeNumber a, LargeNumber b) {

        LargeNumber result = new();

        for (int i = 0; i < 1024; i++)
            result.number[i] = 0;

        byte carry = 0;
        for (int i = 0; i < 1024; i++) {

            int sum = a.number[i] + b.number[i] + carry;
            result.number[i] = (byte)(sum % 10);
            carry = (byte)(sum / 10);
        }

        return result;
    }
    public static LargeNumber operator +(LargeNumber a, int b) => a + FromInt(b);
    public static LargeNumber operator +(int a, LargeNumber b) => FromInt(a) + b;
    public static LargeNumber operator +(LargeNumber a, string b) => a + FromString(b);
    public static LargeNumber operator +(string a, LargeNumber b) => FromString(a) + b;
    public static LargeNumber operator +(LargeNumber a) => a;
    public static LargeNumber operator ++(LargeNumber a) => a + 1;

    #endregion

    #region Subtract

    public void Subtract(int value) {

        byte borrow = 0;
        for (int i = 0; i < 1024; i++) {

            int difference = number[i] - value - borrow;
            if (difference < 0) {

                difference += 10;
                borrow = 1;
            } else {

                borrow = 0;
            }
            number[i] = (byte)difference;
        }
    }

    public static LargeNumber operator -(LargeNumber a, LargeNumber b)
    {
        LargeNumber result = new();
        for (int i = 0; i < 1024; i++)
        {
            result.number[i] = 0;
        }
        byte borrow = 0;
        for (int i = 0; i < 1024; i++)
        {
            int difference = a.number[i] - b.number[i] - borrow;
            if (difference < 0)
            {
                difference += 10;
                borrow = 1;
            }
            else
            {
                borrow = 0;
            }
            result.number[i] = (byte)difference;
        }
        return result;
    }
    public static LargeNumber operator -(LargeNumber a, int b) => a - FromInt(b);
    public static LargeNumber operator -(int a, LargeNumber b) => FromInt(a) - b;
    public static LargeNumber operator -(LargeNumber a, string b) => a - FromString(b);
    public static LargeNumber operator -(string a, LargeNumber b) => FromString(a) - b;
    public static LargeNumber operator -(LargeNumber a)
    {
        LargeNumber result = new();
        for (int i = 0; i < 1024; i++)
        {
            result.number[i] = 0;
        }
        for (int i = 0; i < 1024; i++)
        {
            result.number[i] = (byte)(9 - a.number[i]);
        }
        result++;
        return result;
    }
    public static LargeNumber operator --(LargeNumber a) => a - 1;

    #endregion

    #region Multiply

    public void Multiply(int factor) {

        byte carry = 0;
        for (int i = 0; i < 1024; i++) {

            int product = number[i] * factor + carry;
            number[i] = (byte)(product % 10);
            carry = (byte)(product / 10);
        }
    }

    public static LargeNumber operator *(LargeNumber a, LargeNumber b) {

        LargeNumber result = new();

        for (int i = 0; i < 1024; i++) {

            LargeNumber partial = new();

            byte carry = 0;
            for (int j = 0; j < 1024; j++) {

                int product = a.number[i] * b.number[j] + carry;
                partial.number[j] = (byte)(product % 10);
                carry = (byte)(product / 10);
            }

            for (int j = 0; j < 1024 - i; j++)
                partial.Multiply(10);

            result.Add(partial);
        }

        return result;
    }
    public static LargeNumber operator *(LargeNumber a, int b) {

        LargeNumber result = new();

        for (int i = 0; i < 1024; i++)
            result.number[i] = 0;

        byte carry = 0;
        for (int i = 0; i < 1024; i++) {

            int product = a.number[i] * b + carry;
            result.number[i] = (byte)(product % 10);
            carry = (byte)(product / 10);
        }

        return result;
    }
    public static LargeNumber operator *(int a, LargeNumber b) => b * a;
    public static LargeNumber operator *(LargeNumber a, string b) => a * FromString(b);
    public static LargeNumber operator *(string a, LargeNumber b) => FromString(a) * b;

    #endregion

    #region Divide

    public void Divide(int divisor) {

        byte carry = 0;
        for (int i = 1023; i >= 0; i--) {

            int dividend = number[i] + carry * 10;
            number[i] = (byte)(dividend / divisor);
            carry = (byte)(dividend % divisor);
        }
    }

    public static LargeNumber operator /(LargeNumber a, LargeNumber b) {

        LargeNumber result = new();

        LargeNumber remainder = new();

        for (int i = 0; i < 1024; i++)
            remainder.number[i] = a.number[i];

        for (int i = 1023; i >= 0; i--) {

            while (remainder >= b) {

                remainder -= b;
                result.number[i]++;
            }
            if (i > 0) {

                remainder.Multiply(10);
                remainder += (LargeNumber)a.number[i - 1];
            }
        }

        return result;
    }
    public static LargeNumber operator /(LargeNumber a, int b) {

        LargeNumber result = new();

        for (int i = 0; i < 1024; i++)
            result.number[i] = 0;

        byte carry = 0;
        for (int i = 1023; i >= 0; i--) {

            int dividend = a.number[i] + carry * 10;
            result.number[i] = (byte)(dividend / b);
            carry = (byte)(dividend % b);
        }

        return result;
    }
    public static LargeNumber operator /(int a, LargeNumber b) => FromInt(a) / b;
    public static LargeNumber operator /(LargeNumber a, string b) => a / FromString(b);
    public static LargeNumber operator /(string a, LargeNumber b) => FromString(a) / b;

    #endregion

    #region Modulus

    public static LargeNumber operator %(LargeNumber a, LargeNumber b) {

        LargeNumber remainder = new();

        for (int i = 0; i < 1024; i++)
            remainder.number[i] = a.number[i];

        for (int i = 1023; i >= 0; i--) {
            while (remainder >= b)
                remainder -= b;

            if (i > 0) {

                remainder.Multiply(10);
                remainder += (LargeNumber)a.number[i - 1];
            }
        }

        return remainder;
    }
    public static LargeNumber operator %(LargeNumber a, int b) => a % FromInt(b);
    public static LargeNumber operator %(int a, LargeNumber b) => FromInt(a) % b;
    public static LargeNumber operator %(LargeNumber a, string b) => a % FromString(b);
    public static LargeNumber operator %(string a, LargeNumber b) => FromString(a) % b;

    #endregion

    #region Bitwise AND

    public static LargeNumber operator &(LargeNumber a, LargeNumber b) {

        LargeNumber result = new();

        for (int i = 0; i < 1024; i++)
            result.number[i] = (byte)(a.number[i] & b.number[i]);

        return result;
    }
    public static LargeNumber operator &(LargeNumber a, int b) => a & FromInt(b);
    public static LargeNumber operator &(int a, LargeNumber b) => FromInt(a) & b;
    public static LargeNumber operator &(LargeNumber a, string b) => a & FromString(b);
    public static LargeNumber operator &(string a, LargeNumber b) => FromString(a) & b;

    #endregion

    #region Bitwise OR

    public static LargeNumber operator |(LargeNumber a, LargeNumber b) {

        LargeNumber result = new();

        for (int i = 0; i < 1024; i++)
            result.number[i] = (byte)(a.number[i] | b.number[i]);

        return result;
    }
    public static LargeNumber operator |(LargeNumber a, int b) => a | FromInt(b);
    public static LargeNumber operator |(int a, LargeNumber b) => FromInt(a) | b;
    public static LargeNumber operator |(LargeNumber a, string b) => a | FromString(b);
    public static LargeNumber operator |(string a, LargeNumber b) => FromString(a) | b;

    #endregion

    #region Bitwise XOR

    public static LargeNumber operator ^(LargeNumber a, LargeNumber b) {

        LargeNumber result = new();

        for (int i = 0; i < 1024; i++)
            result.number[i] = (byte)(a.number[i] ^ b.number[i]);

        return result;
    }
    public static LargeNumber operator ^(LargeNumber a, int b) => a ^ FromInt(b);
    public static LargeNumber operator ^(int a, LargeNumber b) => FromInt(a) ^ b;
    public static LargeNumber operator ^(LargeNumber a, string b) => a ^ FromString(b);
    public static LargeNumber operator ^(string a, LargeNumber b) => FromString(a) ^ b;

    #endregion

    #region Bit shift

    public static LargeNumber operator <<(LargeNumber a, int b) {

        LargeNumber result = new();

        for (int i = 0; i < 1024 - b; i++)
            result.number[i + b] = a.number[i];

        return result;
    }

    public static LargeNumber operator >>(LargeNumber a, int b) {

        LargeNumber result = new();

        for (int i = b; i < 1024; i++)
            result.number[i - b] = a.number[i];

        return result;
    }

    #endregion

    #region Bitwise complement

    public static LargeNumber operator ~(LargeNumber a) {

        LargeNumber result = new();

        for (int i = 0; i < 1024; i++)
            result.number[i] = (byte)~a.number[i];

        return result;
    }

    #endregion

    #region Comparison operators

    public static bool operator >(LargeNumber a, LargeNumber b) {

        for (int i = 1023; i >= 0; i--) {

            if (a.number[i] > b.number[i])
                return true;

            if (a.number[i] < b.number[i])
                return false;
        }

        return false;
    }

    public static bool operator <(LargeNumber a, LargeNumber b) {

        for (int i = 1023; i >= 0; i--) {

            if (a.number[i] < b.number[i])
                return true;
            if (a.number[i] > b.number[i])
                return false;
        }

        return false;
    }

    public static bool operator >=(LargeNumber a, LargeNumber b) {

        for (int i = 1023; i >= 0; i--) {

            if (a.number[i] > b.number[i])
                return true;
            if (a.number[i] < b.number[i])
                return false;
        }

        return true;
    }

    public static bool operator <=(LargeNumber a, LargeNumber b) {

        for (int i = 1023; i >= 0; i--) {

            if (a.number[i] < b.number[i])
                return true;
            if (a.number[i] > b.number[i])
                return false;
        }

        return true;
    }

    public static bool operator ==(LargeNumber a, LargeNumber b) {

        for (int i = 1023; i >= 0; i--)
            if (a.number[i] != b.number[i])
                return false;

        return true;
    }

    public static bool operator !=(LargeNumber a, LargeNumber b) {

        for (int i = 1023; i >= 0; i--)
            if (a.number[i] != b.number[i])
                return true;

        return false;
    }

    #endregion

    public override string ToString() {

        int i = 1023;
        while (i > 0 && number[i] == 0)
            i--;

        char[] result = new char[i + 1];

        for (int j = 0; j <= i; j++)
            result[j] = (char)('0' + number[i - j]);

        return new string(result);
    }

    public int ToInt() {

        int result = 0;
        int factor = 1;

        for (int i = 0; i < 1024; i++) {

            result += number[i] * factor;
            factor *= 10;
        }

        return result;
    }

    #region implicit operators

    public static implicit operator LargeNumber(int value) => FromInt(value);
    public static implicit operator LargeNumber(string value) => FromString(value);
    public static implicit operator int(LargeNumber value) => value.ToInt();
    public static implicit operator string(LargeNumber value) => value.ToString();
    public static implicit operator double(LargeNumber value) => value.ToInt();
    public static implicit operator float(LargeNumber value) => value.ToInt();
    public static implicit operator LargeNumber(double value) => FromInt((int)value);
    public static implicit operator LargeNumber(float value) => FromInt((int)value);
    public static implicit operator LargeNumber(byte value) => FromInt(value);
    public static implicit operator LargeNumber(char value) => FromInt(value);
    public static implicit operator LargeNumber(short value) => FromInt(value);
    public static implicit operator LargeNumber(long value) => FromInt((int)value);
    public static implicit operator LargeNumber(uint value) => FromInt((int)value);
    public static implicit operator LargeNumber(ulong value) => FromInt((int)value);
    public static implicit operator LargeNumber(ushort value) => FromInt(value);
    public static implicit operator LargeNumber(sbyte value) => FromInt(value);
    public static implicit operator LargeNumber(decimal value) => FromInt((int)value);
    public static implicit operator LargeNumber(bool value) => FromInt(value ? 1 : 0);

    #endregion

    public override bool Equals(object obj) {

        if (obj == null || GetType() != obj.GetType())
            return false;

        LargeNumber other = (LargeNumber)obj;

        for (int i = 0; i < 1024; i++)
            if (number[i] != other.number[i])
                return false;

        return true;
    }

    public override int GetHashCode() {

        int hash = 0;

        for (int i = 0; i < 1024; i++)
            hash ^= number[i].GetHashCode();

        return hash;
    }
}
