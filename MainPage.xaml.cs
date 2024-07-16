using ChaudronNoir.Classes;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Nodes;

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

            //this.BindingContext = stats;

            InitializeComponent();

            LoadData();

            grdStats.BindingContext = stats;
            lstObject.ItemsSource = objetsList;
        }
        //-------------------------------------------------------------------

        // Met à jour les variables de classe avec les données contenu sur l'appareil
        private void LoadData()
        {
            // Load data
            string path = FileSystem.Current.AppDataDirectory;
            string fileStats = Path.Combine(path, "stats.lcn");
            string fileObjets = Path.Combine(path, "objets.lcn");

            if (File.Exists(fileStats))
            {
                string rawStats = File.ReadAllText(fileStats);
                Stats? temp = JsonSerializer.Deserialize<Stats>(rawStats);
                if (temp != null)
                {
                    stats = temp;
                    stats.OnPropertyChanged();
                }
                else
                {
                    stats.Reset();
                }
                grdStats.BindingContext = stats;
            }

            if (File.Exists(fileObjets))
            {
                string rawObjets = File.ReadAllText(fileObjets);
                ObservableCollection<Objet>? temp = JsonSerializer.Deserialize<ObservableCollection<Objet>>(rawObjets);
                objetsList.Clear();
                if (temp != null)
                {
                    objetsList = temp;
                    lstObject.ItemsSource = objetsList;
                }
            }

            etrLastChapter.Text = Preferences.Get("LastChapter", "1");
        }
        //-------------------------------------------------------------------

        // Sauvegarde les données sur l'appareil
        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            SaveData();
            await DisplayAlert("Sauvegarde", "Partie sauvegardée", "Ok");
        }
        //-------------------------------------------------------------------

        // Enregistre la progression sur l'appareil
        private void SaveData()
        {
            string path = FileSystem.Current.AppDataDirectory;
            string fileStats = Path.Combine(path, "stats.lcn");
            string fileObjets = Path.Combine(path, "objets.lcn");

            if (File.Exists(fileStats))
            {
                File.Delete(fileStats);
            }
            if (File.Exists(fileObjets))
            {
                File.Delete(fileObjets);
            }

            var serializedStats = JsonSerializer.Serialize(stats);
            File.WriteAllText(fileStats, serializedStats);

            var serializedObjets = JsonSerializer.Serialize(objetsList);
            File.WriteAllText(fileObjets, serializedObjets);

            if (etrLastChapter.Text != string.Empty)
            {
                Preferences.Set("LastChapter", etrLastChapter.Text);
            }
        }
        //-------------------------------------------------------------------

        // Ajoute un objet à la liste des objets
        private void btnAddObjectClick(object sender, EventArgs e)
        {
            if (etrObject.Text != string.Empty)
            {
                Objet obj = new Objet(etrObject.Text);
                objetsList.Add(obj);

                etrObject.Text = string.Empty;
            }
        }
        //-------------------------------------------------------------------

        // Supprime l'objet séléctionné de la liste
        private void btnSuppObjet_Clicked(object sender, EventArgs e)
        {
            Objet? objSel = lstObject.SelectedItem as Objet;
            if (objSel != null)
            {
                objetsList.Remove(objSel);
            }
        }
        //-------------------------------------------------------------------

        // Supprime la sauvegarde et réinitialise l'aventure
        private async void btnReset_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Nouvelle partie", "Vous êtes sur le point de recommencer une partie et défasser votre sauvegarde.\nContinuer ?", "Oui", "Non"))
            {
                stats.Reset();
                objetsList.Clear();

                string path = FileSystem.Current.AppDataDirectory;
                string fileStats = Path.Combine(path, "stats.lcn");
                string fileObjets = Path.Combine(path, "objets.lcn");

                if (File.Exists(fileStats))
                {
                    File.Delete(fileStats);
                }
                if (File.Exists(fileObjets))
                {
                    File.Delete(fileObjets);
                }

                Preferences.Clear();
                etrLastChapter.Text = "1";
            }
        }
        //-------------------------------------------------------------------
    }
}
