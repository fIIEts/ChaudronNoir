using ChaudronNoir.Classes;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.JavaScript;

namespace ChaudronNoir
{
    public partial class MainPage : ContentPage
    {
        Stats stats;
        ObservableCollection<Objet> objetsList;

        public MainPage()
        {
            stats = new Stats();
            objetsList = new ObservableCollection<Objet>();

            Objet lance = new Objet("Lance");
            objetsList.Add(lance);

            Objet fourche = new Objet("Fourche");
            objetsList.Add(fourche);

            Objet kitEscalade = new Objet("Kit d'escalade");
            objetsList.Add(kitEscalade);

            this.BindingContext = stats;

            InitializeComponent();

            lstObject.ItemsSource = objetsList;
        }

        private void btnAddObjectClick(object sender, EventArgs e)
        {
            Objet obj = new Objet(etrObject.Text);
            objetsList.Add(obj);

            etrObject.Text = string.Empty;
        }

        private void btnSuppObjet_Clicked(object sender, EventArgs e)
        {
            Objet? objSel = lstObject.SelectedItem as Objet;
            if (objSel != null)
            {
                objetsList.Remove(objSel);
            }
        }

    }
}
