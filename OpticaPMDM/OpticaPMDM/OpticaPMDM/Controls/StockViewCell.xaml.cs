using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpticaPMDM.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockViewCell : ViewCell
    {

        public static BindableProperty MarcaProperty =
            BindableProperty.Create("Marca", typeof(string),
                typeof(StockViewCell),
                null,
                propertyChanged: (bindable, old, n) =>
                {
                    var cell = bindable as StockViewCell;
                    if (cell != null && n != null)
                    {
                        cell.MarcaLabel.Text = n.ToString();
                    }
                });

        public string Marca
        {
            get { return (string)GetValue(MarcaProperty); }
            set { SetValue(MarcaProperty, value); }
        }

        public static BindableProperty ModeloProperty =
            BindableProperty.Create("Modelo", typeof(string),
                typeof(StockViewCell),
                null,
                propertyChanged: (bindable, old, n) =>
                {
                    var cell = bindable as StockViewCell;
                    if (cell != null && n != null)
                    {
                        cell.ModeloLabel.Text = n.ToString();
                    }
                });

        public string Modelo
        {
            get { return (string)GetValue(ModeloProperty); }
            set { SetValue(ModeloProperty, value); }
        }

        public static BindableProperty DiasRestantesProperty =
            BindableProperty.Create("DiasRestantes", typeof(int),
                typeof(StockViewCell),
                null,
            propertyChanged: (bindable, old, n) =>
            {
                var cell = bindable as StockViewCell;
                if (cell != null && n != null)
                {
                    cell.DiasRestantesLabel.Text = n.ToString();
                }
            });

        public int DiasRestantes
        {
            get { return (int)GetValue(DiasRestantesProperty); }
            set { SetValue(DiasRestantesProperty, value); }
        }

        public static BindableProperty ParRestantesProperty =
            BindableProperty.Create("ParRestantes", typeof(int),
                typeof(StockViewCell),
                null,
            propertyChanged: (bindable, old, n) =>
            {
                var cell = bindable as StockViewCell;
                if (cell != null && n != null)
                {
                    cell.ParRestantesLabel.Text = n.ToString();
                }
            });

        public int ParRestantes
        {
            get { return (int)GetValue(ParRestantesProperty); }
            set { SetValue(ParRestantesProperty, value); }
        }


        public static BindableProperty DeleteCommandProperty =
            BindableProperty.Create("DeleteCommand", typeof(ICommand),
                typeof(StockViewCell),
                null,
                propertyChanged: (bindable, old, n) =>
                {
                    var cell = bindable as StockViewCell;
                    if (cell != null && n != null)
                    {
                        cell.deletionMenu.Command = n as ICommand;
                    }
                });

        public ICommand DeleteCommand
        {
            get { return (ICommand)GetValue(DeleteCommandProperty); }
            set { SetValue(DeleteCommandProperty, value); }
        }

        public static BindableProperty DeleteCommandParameterProperty =
            BindableProperty.Create("DeleteCommandParameter", typeof(object),
                typeof(StockViewCell),
                null,
                propertyChanged: (bindable, old, n) =>
                {
                    var cell = bindable as StockViewCell;
                    if (cell != null && n != null)
                    {
                        cell.deletionMenu.CommandParameter = n;
                    }
                });

        public object DeleteCommandParameter
        {
            get { return (object)GetValue(DeleteCommandParameterProperty); }
            set { SetValue(DeleteCommandParameterProperty, value); }
        }


        public StockViewCell()
        {
            InitializeComponent();
        }
    }
}