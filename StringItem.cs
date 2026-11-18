namespace Lab3_2_Variant8;

/// <summary>
/// Клас для зберігання та обробки рядка (варіант 8).
/// </summary>
public class StringItem : IComparable<StringItem>
{
    public string Value { get; private set; }
    public int Length => Value.Length;

    public StringItem(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    // Пошук заданого символу. Повертає всі позиції символу в рядку.
    public List<int> FindCharacter(char character)
    {
        var positions = new List<int>();
        for (int i = 0; i < Value.Length; i++)
            if (Value[i] == character)
                positions.Add(i);
        return positions;
    }

    // Зміна порядку символів на протилежний.
    public void ReverseOrder()
    {
        char[] characters = Value.ToCharArray();
        Array.Reverse(characters);
        Value = new string(characters);
    }

    // Додавання нового рядка до існуючого.
    public void Append(string newText)
    {
        Value += newText ?? throw new ArgumentNullException(nameof(newText));
    }

    public void Display()
    {
        Console.WriteLine($"Значення: \"{Value}\", довжина: {Length}");
    }

    public int CompareTo(StringItem? other)
    {
        if (other is null) return 1;
        return string.Compare(Value, other.Value, StringComparison.Ordinal);
    }

    public override string ToString() => $"\"{Value}\" (довжина: {Length})";
}

/// <summary>Окремий компаратор для порівняння рядків за довжиною.</summary>
public class StringItemLengthComparer : IComparer<StringItem>
{
    public int Compare(StringItem? x, StringItem? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (x is null) return -1;
        if (y is null) return 1;

        int result = x.Length.CompareTo(y.Length);
        return result != 0 ? result : x.CompareTo(y);
    }
}
