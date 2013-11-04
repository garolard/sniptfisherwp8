using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interactivity;
using Microsoft.Phone.Controls;
using System.Windows.Input;
using System.Windows;

namespace Sniptfisher.Behaviors
{
    public class IncrementalLoadingBehavior : Behavior<LongListSelector>
    {
        private const int itemsBeforeNextLoad = 7;

        public ICommand LoadCommand
        {
            get { return (ICommand)GetValue(LoadCommandProperty); }
            set { SetValue(LoadCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LoadMoreItemsCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadCommandProperty =
            DependencyProperty.Register("LoadCommand", typeof(ICommand), typeof(IncrementalLoadingBehavior), new PropertyMetadata(default(ICommand)));        

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.ItemRealized += OnItemRealized;
        }

        private void OnItemRealized(object sender, ItemRealizationEventArgs e)
        {
            var longListSelector = sender as LongListSelector;
            if (longListSelector == null)
            {
                return;
            }

            var item = e.Container.Content;
            var items = longListSelector.ItemsSource;

            if (items != null && items.Count >= itemsBeforeNextLoad)
            {
                if (e.ItemKind == LongListSelectorItemKind.Item)
                {
                    if (item.Equals(items[items.Count - itemsBeforeNextLoad]))
                    {
                        this.LoadCommand.Execute(null);
                    }
                }
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.ItemRealized -= OnItemRealized;
        }
    }
}
