using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AtWork.CustomControls
{
    public class RepeaterView : StackLayout
    {
        #region Bindable Properties

        /// <summary>
        /// Property bound to <see cref="ItemTemplate"/>.
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(propertyName: nameof(ItemTemplate), returnType: typeof(DataTemplate), declaringType: typeof(RepeaterView), defaultValue: default(DataTemplate), propertyChanged: OnItemTemplateChanged);

        public static readonly BindableProperty FooterTemplateProperty = BindableProperty.Create(propertyName: nameof(FooterTemplate), returnType: typeof(DataTemplate), declaringType: typeof(RepeaterView), defaultValue: default(DataTemplate), propertyChanged: OnFooterTemplateChanged);


        /// <summary>
        /// Property bound to <see cref="ItemsSource"/>.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(propertyName: nameof(ItemsSource), returnType: typeof(IEnumerable<object>), declaringType: typeof(RepeaterView), propertyChanged: OnItemsSourceChanged);
        public static readonly BindableProperty ColumnCountProperty = BindableProperty.Create(propertyName: nameof(ColumnCount), returnType: typeof(double), declaringType: typeof(RepeaterView), defaultValue: double.MinValue, propertyChanged: OnColumnCountChanged);
        public static readonly BindableProperty GridWidthUnitTypeProperty = BindableProperty.Create(propertyName: nameof(GridWidthUnitType), returnType: typeof(GridUnitType), declaringType: typeof(RepeaterView), defaultValue: GridUnitType.Star, propertyChanged: OnGridWidthUnitTypeChanged);

        #endregion

        #region Properties 

        /// <summary>
        /// Gets or sets the <see cref="DataTemplate"/>.
        /// </summary>
        public DataTemplate FooterTemplate
        {
            get { return (DataTemplate)GetValue(FooterTemplateProperty); }
            set { SetValue(FooterTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="DataTemplate"/>.
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the collection of view models to bind to the item views.
        /// </summary>
        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public double ColumnCount
        {
            get { return (double)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        public GridUnitType GridWidthUnitType
        {
            get { return (GridUnitType)GetValue(GridWidthUnitTypeProperty); }
            set { SetValue(GridWidthUnitTypeProperty, value); }
        }
        #endregion

        #region Property Changed Callbacks

        /// <summary>
        /// Called when <see cref="ItemTemplate"/> changes.
        /// </summary>
        /// <param name="bindable">The <see cref="BindableObject"/> being changed.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var layout = (RepeaterView)bindable;
            if (newValue == null)
            {
                return;
            }

            layout.PopulateItems();
        }

        /// <summary>
        /// Called when <see cref="ItemTemplate"/> changes.
        /// </summary>
        /// <param name="bindable">The <see cref="BindableObject"/> being changed.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnFooterTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var layout = (RepeaterView)bindable;
            if (newValue == null)
            {
                return;
            }

            layout.PopulateItems();
        }

        /// <summary>
        /// Called when <see cref="ItemsSource"/> is changed.
        /// </summary>
        /// <param name="bindable">The <see cref="BindableObject"/> being changed.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var layout = (RepeaterView)bindable;
            if (newValue == null)
            {
                layout.Children.Clear();
                return;
            }

            layout.PopulateItems();
        }

        private static void OnColumnCountChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var layout = (RepeaterView)bindable;
            if (newValue == null)
            {
                return;
            }

            layout.PopulateItems();
        }

        private static void OnGridWidthUnitTypeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var layout = (RepeaterView)bindable;
            if (newValue == null)
            {
                return;
            }

            layout.PopulateItems();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates and binds the item views based on <see cref="ItemTemplate"/>.
        /// </summary>
        private void PopulateItems()
        {
            try
            {
                var items = ItemsSource;
                if (items == null || ItemTemplate == null)
                {
                    return;
                }

                var children = Children;
                children.Clear();

                if (ColumnCount > 0)
                {
                    var ViewCollection = items.ToList();

                    ColumnDefinitionCollection columnDefinitions = new ColumnDefinitionCollection();

                    for (int i = 0; i < ColumnCount; i++)
                    {
                        columnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridWidthUnitType) });
                    }

                    var grd = new Grid()
                    {
                        ColumnSpacing = this.Spacing,
                        RowSpacing = this.Spacing,
                        Padding = 0,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        ColumnDefinitions = columnDefinitions,
                    };

                    var row = 0;
                    for (int i = 0; i < ViewCollection.Count; i++)
                    {
                        for (int j = 0; j < ColumnCount; j++)
                        {
                            if (i < ViewCollection.Count && ViewCollection[i] != null)
                            {
                                var item = ViewCollection[i];
                                grd.Children.Add(InflateView(item), j, row);

                                if (j != ColumnCount - 1)
                                {
                                    i++;
                                }
                            }
                        }
                        row++;
                    }
                    children.Add(grd);
                }
                else
                {
                    foreach (var item in items)
                    {
                        children.Add(InflateView(item));
                    }
                    /* Ash
                    if (items != null && FooterTemplate != null)
                    {
                        var view = (View)CreateContent(FooterTemplate, null, this);
                        children.Add(view);
                    }
                    */
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// Inflates an item view using the correct <see cref="DataTemplate"/> for the given view model.
        /// </summary>
        /// <param name="viewModel">The view model to bind the item view to.</param>
        /// <returns>The new view with the view model as its binding context.</returns>
        private View InflateView(object viewModel)
        {
            var view = (View)CreateContent(ItemTemplate, viewModel, this);
            view.BindingContext = viewModel;
            return view;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Create UI content from a <see cref="DataTemplate"/> (or optionally a <see cref="DataTemplateSelector"/>).
        /// </summary>
        /// <param name="template">The <see cref="DataTemplate"/>.</param>
        /// <param name="item">The view model object.</param>
        /// <param name="container">The <see cref="BindableObject"/> that will be the parent to the content.</param>
        /// <returns>The content created by the template.</returns>
        public static object CreateContent(DataTemplate template, object item, BindableObject container)
        {
            var selector = template as DataTemplateSelector;
            if (selector != null)
            {
                template = selector.SelectTemplate(item, container);
            }

            return template.CreateContent();
        }

        #endregion
    }
}
