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
    public partial class PedidosViewCell : ViewCell
    {
        public static BindableProperty NombreProperty =
                BindableProperty.Create("Nombre", typeof(string),
                    typeof(PedidosViewCell),
                    null,
                    propertyChanged: (bindable, old, n) =>
                    {
                        var cell = bindable as PedidosViewCell;
                        if (cell != null && n != null)
                        {
                            cell.NombreLabel.Text = n.ToString();
                        }
                    });

        public string Nombre
        {
            get { return (string)GetValue(NombreProperty); }
            set { SetValue(NombreProperty, value); }
        }

        public static BindableProperty DescripcionProperty =
            BindableProperty.Create("Descripcion", typeof(string),
                typeof(PedidosViewCell),
                null,
                propertyChanged: (bindable, old, n) =>
                {
                    var cell = bindable as PedidosViewCell;
                    if (cell != null && n != null)
                    {
                        cell.DescripcionLabel.Text = n.ToString();
                    }
                });

        public string Descripcion
        {
            get { return (string)GetValue(DescripcionProperty); }
            set { SetValue(DescripcionProperty, value); }
        }

        public static BindableProperty FechaProperty =
            BindableProperty.Create("Fecha", typeof(DateTime),
                typeof(PedidosViewCell),
                null,
            propertyChanged: (bindable, old, n) =>
            {
                var cell = bindable as PedidosViewCell;
                if (cell != null && n != null)
                {
                    cell.FechaLabel.Text = n.ToString();
                }
            });

        public int Fecha
        {
            get { return (int)GetValue(FechaProperty); }
            set { SetValue(FechaProperty, value); }
        }

        public static BindableProperty DeleteCommandProperty =
            BindableProperty.Create("DeleteCommand", typeof(ICommand),
                typeof(PedidosViewCell),
                null,
                propertyChanged: (bindable, old, n) =>
                {
                    var cell = bindable as PedidosViewCell;
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
                typeof(PedidosViewCell),
                null,
                propertyChanged: (bindable, old, n) =>
                {
                    var cell = bindable as PedidosViewCell;
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


        public PedidosViewCell()
        {
            InitializeComponent();
        }
    }
}