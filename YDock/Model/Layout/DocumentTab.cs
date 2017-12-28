﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using YDock.Interface;

namespace YDock.Model
{
    public class DocumentTab : IModel, ILayoutContainer
    {
        public DocumentTab()
        {
            _children.CollectionChanged += _children_CollectionChanged;
        }

        private void _children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
                foreach (ILayoutElement item in e.OldItems)
                    item.Container = null;
            if (e.NewItems != null)
                foreach (ILayoutElement item in e.NewItems)
                    item.Container = this;
        }

        ObservableCollection<ILayoutElement> _children = new ObservableCollection<ILayoutElement>();
        public ObservableCollection<ILayoutElement> Children
        {
            get { return _children; }
        }

        private IView _view;
        public IView View
        {
            get
            {
                return _view;
            }

            set
            {
                if (_view != value)
                    _view = value;
            }
        }

        public IEnumerable<ILayoutElement> ChildrenSorted
        {
            get
            {
                var listSorted = Children.ToList();
                listSorted.Sort();
                return listSorted;
            }
        }

        IEnumerable<ILayoutElement> ILayoutContainer.Children
        {
            get
            {
                return Children;
            }
        }
    }
}