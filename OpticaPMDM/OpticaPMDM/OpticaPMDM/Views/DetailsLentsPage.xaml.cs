﻿using OpticaPMDM.Backend.Entities;
using OpticaPMDM.Backend.Services;
using OpticaPMDM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpticaPMDM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsLentsPage : ContentPage
    {
        bool Flag { get; set; }
        public DetailsLentsPage(Lentillas lentillas)
        {
            InitializeComponent();
            BindingContext = new LentillasVM(lentillas, ItemSaved, LentillasService.Instance);
            Flag = false;
        }
        public DetailsLentsPage()
        {
            InitializeComponent();
            BindingContext = new LentillasVM(null, ItemSaved, LentillasService.Instance);
            Flag = false;
        }

        public DetailsLentsPage(bool Flag)
        {
            InitializeComponent();
            BindingContext = new LentillasVM(null, ItemSaved, LentillasService.Instance);
            this.Flag = Flag;
        }

        public async void ItemSaved()
        {
            if (Flag == true)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await Navigation.PopAsync();
            }
        }
    }
}