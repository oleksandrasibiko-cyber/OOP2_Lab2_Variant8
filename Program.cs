using System.Collections;

namespace Lab3_2_Variant8;

internal class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("ЛАБОРАТОРНА РОБОТА 3.2 — ВАРІАНТ 8");
        Console.WriteLine("Дослідження колекцій та узагальнених колекцій C#\n");

        StringItem[] initialItems =
        {
            new("Hello"),
            new("World"),
            new("CSharp"),
            new("Programming")
        };

        DemonstrateStringMethods();
        DemonstrateArray(initialItems);
        DemonstrateArrayList(initialItems);
        DemonstrateGenericList(initialItems);
        DemonstrateSorting(initialItems);
        DemonstrateBinaryTree(initialItems);

        Console.WriteLine("\nРоботу завершено.");
    }

    private static void DemonstrateStringMethods()
    {
        Console.WriteLine("=== Методи класу StringItem ===");
        var item = new StringItem("Programming");
        item.Display();

        char searched = 'm';
        List<int> positions = item.FindCharacter(searched);
        Console.WriteLine(positions.Count > 0
            ? $"Символ '{searched}' знайдено на позиціях: {string.Join(", ", positions)}"
            : $"Символ '{searched}' не знайдено.");

        item.ReverseOrder();
        Console.WriteLine("Після зміни порядку символів:");
        item.Display();

        item.Append("_CSharp");
        Console.WriteLine("Після додавання нового рядка:");
        item.Display();
    }

    private static void DemonstrateArray(StringItem[] source)
    {
        Console.WriteLine("\n=== Масив StringItem[] ===");
        StringItem[] items = source.ToArray();
        Console.WriteLine("Початкові елементи:");
        DisplayItems(items);

        // Додавання: масив має фіксований розмір, тому створюємо новий.
        items = items.Append(new StringItem("New item")).ToArray();
        Console.WriteLine("Після додавання:");
        DisplayItems(items);

        // Оновлення елемента.
        items[1] = new StringItem("Updated");
        Console.WriteLine("Після оновлення елемента з індексом 1:");
        DisplayItems(items);

        // Пошук.
        int foundIndex = Array.FindIndex(items, x => x.Value.Contains('a'));
        Console.WriteLine(foundIndex >= 0
            ? $"Перший рядок із символом 'a': індекс {foundIndex}, {items[foundIndex]}"
            : "Рядок із символом 'a' не знайдено.");

        // Видалення: створюємо масив без елемента з індексом 2.
        items = items.Where((_, index) => index != 2).ToArray();
        Console.WriteLine("Після видалення елемента з індексом 2:");
        DisplayItems(items);
    }

    private static void DemonstrateArrayList(StringItem[] source)
    {
        Console.WriteLine("\n=== Неузагальнена колекція ArrayList ===");
        ArrayList items = new(source);
        Console.WriteLine("Початкові елементи:");
        DisplayArrayList(items);

        items.Add(new StringItem("New item"));
        Console.WriteLine("Після додавання:");
        DisplayArrayList(items);

        items[1] = new StringItem("Updated");
        Console.WriteLine("Після оновлення елемента з індексом 1:");
        DisplayArrayList(items);

        StringItem? found = items.Cast<StringItem>().FirstOrDefault(x => x.Value.Contains('a'));
        Console.WriteLine(found is not null ? $"Знайдено: {found}" : "Елемент не знайдено.");

        items.RemoveAt(2);
        Console.WriteLine("Після видалення елемента з індексом 2:");
        DisplayArrayList(items);
    }

    private static void DemonstrateGenericList(StringItem[] source)
    {
        Console.WriteLine("\n=== Узагальнена колекція List<StringItem> ===");
        List<StringItem> items = new(source);
        Console.WriteLine("Початкові елементи:");
        DisplayItems(items);

        items.Add(new StringItem("New item"));
        Console.WriteLine("Після додавання:");
        DisplayItems(items);

        items[1] = new StringItem("Updated");
        Console.WriteLine("Після оновлення елемента з індексом 1:");
        DisplayItems(items);

        StringItem? found = items.Find(x => x.Value.Contains('a'));
        Console.WriteLine(found is not null ? $"Знайдено: {found}" : "Елемент не знайдено.");

        items.RemoveAt(2);
        Console.WriteLine("Після видалення елемента з індексом 2:");
        DisplayItems(items);
    }

    private static void DemonstrateSorting(StringItem[] source)
    {
        Console.WriteLine("\n=== Порівняння та сортування ===");
        List<StringItem> items = source.ToList();

        // IComparable<StringItem> реалізовано в самому класі: порівняння за значенням.
        items.Sort();
        Console.WriteLine("Сортування за значенням (IComparable<StringItem>):");
        DisplayItems(items);

        // IComparer<StringItem> задає альтернативне правило порівняння.
        items.Sort(new StringItemLengthComparer());
        Console.WriteLine("Сортування за довжиною (IComparer<StringItem>):");
        DisplayItems(items);
    }

    private static void DemonstrateBinaryTree(StringItem[] source)
    {
        Console.WriteLine("\n=== Узагальнене бінарне дерево ===");
        var tree = new BinaryTree<StringItem>();
        foreach (StringItem item in source)
            tree.Add(item);

        Console.WriteLine("Обхід дерева у зворотному порядку (Postorder: ліве, праве, корінь):");
        foreach (StringItem item in tree)
            Console.WriteLine(item);
    }

    private static void DisplayItems(IEnumerable<StringItem> items)
    {
        foreach (StringItem item in items)
            Console.WriteLine(item);
    }

    private static void DisplayArrayList(ArrayList items)
    {
        foreach (object? item in items)
            Console.WriteLine(item);
    }
}
