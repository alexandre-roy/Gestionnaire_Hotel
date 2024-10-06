using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using _420_14B_FX_A23_TP1.classes;
using _420_14B_FX_A24_TP1.classes;

namespace _420_14B_FX_A24_TP1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        #region CONSTANTES

        public const string CHEMIN_FICHIER_CHAMBRES = @"C:\data\420-14B-FX\TP1\chambres.csv";

        public const string CHEMIN_FICHIER_RESERVATIONS = @"C:\data\420-14B-FX\TP1\reservations.csv";

        #endregion

        #region ATTRIBUTS

        /// <summary>
        /// Hotel
        /// </summary>
        GestionHotel _gestionHotel;

        #endregion

        #region CONSTRUCTEURS
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion


        #region MÉTHODES
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _gestionHotel = new GestionHotel(CHEMIN_FICHIER_CHAMBRES, CHEMIN_FICHIER_RESERVATIONS);

            AfficherListeChambres();

            AfficherListeReservations();

        }

        private void AfficherListeChambres()
        {
            lstChambres.Items.Clear();

            for (int i = 0; i < _gestionHotel.Chambres.Length; i++)
            {
                lstChambres.Items.Add(_gestionHotel.Chambres[i]);
            }
        }

        private void AfficherListeReservations()
        {
            lstReservations.Items.Clear();

            for (int i = 0; i < _gestionHotel.Reservations.Length; i++)
            {
                lstReservations.Items.Add(_gestionHotel.Reservations[i]);
            }
        }


        private void btnEffacerRecherche_Click(object sender, RoutedEventArgs e)
        {
          lstChambres.Items.Clear();
        }

        private void btnRechercheChambre_Click(object sender, RoutedEventArgs e)
        {
            DateOnly dateArrivee = DateOnly.FromDateTime(dtpDateArrivee.SelectedDate.Value);
            DateOnly dateDepart = DateOnly.FromDateTime(dtpDateDepart.SelectedDate.Value);

            Chambre[] chambresDisponibles = _gestionHotel.RechercherChambresDisponibles(dateArrivee, dateDepart);

            lstChambres.Items.Clear();

            for (int i = 0; i < chambresDisponibles.Length; i++)
            {
                if (chambresDisponibles[i] != null)
                {
                    lstChambres.Items.Add(chambresDisponibles[i]);
                }
            }
        }

        private void lstChambres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Chambre chambreSelectionnee = (Chambre)lstChambres.SelectedItem;
            txtNumero.Text = chambreSelectionnee.Numero.ToString();
            txtType.Text = chambreSelectionnee.Type.ToString();
            txtDateArrivee.Text = dtpDateArrivee.SelectedDate.Value.ToShortDateString();
            txtDateDepart.Text = dtpDateDepart.SelectedDate.Value.ToShortDateString();
            txtPrixParNuit.Text = chambreSelectionnee.PrixParNuit.ToString();

            DateTime dateArrivee = DateTime.Parse(txtDateArrivee.Text);
            DateTime dateDepart = DateTime.Parse(txtDateDepart.Text);
            int nbJours = (dateDepart - dateArrivee).Days;
            decimal sousTotal = (nbJours * decimal.Parse(txtPrixParNuit.Text));
            decimal total = sousTotal + (sousTotal * 0.15M);

            txtTotal.Text = total.ToString();
        }

        private void btnEffacerReservation_Click(object sender, RoutedEventArgs e)
        {

            
        }

        private void btnCreerReservation_Click(object sender, RoutedEventArgs e)
        {
            string nom = txtNom.Text;
            string prenom = txtPrenom.Text;     
            string courriel = txtCourriel.Text;
            string telephone = txtTelephone.Text;
            string adresse = txtAdresse.Text;

            Reservation nouvelleReservation = new Reservation(adresse, (Chambre)lstChambres.SelectedItem, courriel, DateOnly.FromDateTime(dtpDateArrivee.SelectedDate.Value), DateOnly.FromDateTime(dtpDateDepart.SelectedDate.Value), nom, prenom, telephone);

            _gestionHotel.CreerReservation(nouvelleReservation);

            lstReservations.Items.Clear();

            AfficherListeReservations();
        }

        private void btnEffacerRechercheReservation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRechercherReservation_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnSupprimerReservation_Click(object sender, RoutedEventArgs e)
        {
           
        }
        #endregion

        
    }
}