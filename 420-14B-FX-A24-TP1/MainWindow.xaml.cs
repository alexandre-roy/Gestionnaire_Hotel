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

        }

        private void AfficherListeChambres()
        {
            lstChambres.Items.Clear();

            for (int i = 0; i < _gestionHotel.Chambres.Length; i++)
            {
                lstChambres.Items.Add(_gestionHotel.Chambres[i]);
            }
        }


        private void btnEffacerRecherche_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void btnRechercheChambre_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lstChambres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnEffacerReservation_Click(object sender, RoutedEventArgs e)
        {

            
        }

        private void btnCreerReservation_Click(object sender, RoutedEventArgs e)
        {


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