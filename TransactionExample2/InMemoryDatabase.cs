using System;
using System.Collections.Generic;
using System.Linq;

public class InMemoryDatabase<T> where T : class
{
    private List<T> _data = new List<T>();

    /// <summary>
    /// Вставляет несколько записей в базу данных.
    /// </summary>
    /// <param name="items">Коллекция элементов, которые необходимо добавить.</param>
    public void BulkInsert(IEnumerable<T> items)
    {
        _data.AddRange(items);
        Console.WriteLine($"{items.Count()} записей добавлено.");
    }

    /// <summary>
    /// Обновляет несколько записей в базе данных.
    /// </summary>
    /// <param name="updatedItems">Коллекция элементов, которые необходимо обновить.</param>
    /// <param name="matchPredicate">Функция, которая определяет, какие элементы совпадают для обновления.</param>
    public void BulkUpdate(IEnumerable<T> updatedItems, Func<T, T, bool> matchPredicate)
    {
        foreach (var updatedItem in updatedItems)
        {
            var existingItem = _data.FirstOrDefault(item => matchPredicate(item, updatedItem));
            if (existingItem != null)
            {
                _data.Remove(existingItem);
                _data.Add(updatedItem);
            }
        }
        Console.WriteLine($"{updatedItems.Count()} записей обновлено.");
    }

    /// <summary>
    /// Удаляет несколько записей из базы данных.
    /// </summary>
    /// <param name="itemsToDelete">Коллекция элементов, которые необходимо удалить.</param>
    /// <param name="matchPredicate">Функция, которая определяет, какие элементы совпадают для удаления.</param>
    public void BulkDelete(IEnumerable<T> itemsToDelete, Func<T, T, bool> matchPredicate)
    {
        foreach (var item in itemsToDelete)
        {
            var existingItem = _data.FirstOrDefault(dbItem => matchPredicate(dbItem, item));
            if (existingItem != null)
            {
                _data.Remove(existingItem);
            }
        }
        Console.WriteLine($"{itemsToDelete.Count()} записей удалено.");
    }

    /// <summary>
    /// Выводит текущие данные базы данных в консоль.
    /// </summary>
    public void PrintData()
    {
        Console.WriteLine("Текущие данные:");
        foreach (var item in _data)
        {
            Console.WriteLine(item);
        }
    }
}
