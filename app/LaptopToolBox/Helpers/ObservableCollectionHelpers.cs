using System;
using System.Collections.ObjectModel;

namespace LaptopToolBox.Helpers;

public static class ObservableCollectionHelpers
{
    public static void AdaptToSize<T>(ObservableCollection<T> collection, int size, Func<T> generator) where T : class
    {
        while (collection.Count < size)
        {
            collection.Add(generator.Invoke());
        }
        
        while (collection.Count > size)
        {
            collection.RemoveAt(collection.Count - 1);
        }
    }
}