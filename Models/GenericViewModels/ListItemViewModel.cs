using System;
using System.Collections.Generic;

namespace eventy.Models.GenericViewModels
{
    public class ListItemViewModel : List<ItemViewModel>
    {
        // Automatically creates the id
        public void Add(string value)
        {
            var id = base.Count;
            base.Add(new ItemViewModel()
            {
                Id = id,
                Value = value
            });
        }
    }
}