﻿using System.IO;
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
        /// Gestionnaire d'hotel
        /// </summary>
        GestionHotel _gestionHotel;

        #endregion

        #region CONSTRUCTEUR
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region MÉTHODES
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _gestionHotel = new GestionHotel(CHEMIN_FICHIER_CHAMBRES, CHEMIN_FICHIER_RESERVATIONS);
            txtTotalReservations.Text = $"{_gestionHotel.CalculerMontantTotalReservations():C}";
            txtPrixMoyenReservation.Text = $"{_gestionHotel.CalculerPrixMoyenReservation():C}";
            txtChambrePlusReservee.Text = Convert.ToString(_gestionHotel.ObtenirChambreLaPlusReservee().Numero);           
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

        private string ValiderFormulaire()
        {
            string messageErreur = "";

            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                messageErreur += $"Vous devez inscrire le nom du client.\n";
            }

            if (string.IsNullOrWhiteSpace(txtPrenom.Text))
            {
                messageErreur += $"Vous devez inscrire le prénom du client.\n";
            }

            if (string.IsNullOrWhiteSpace(txtCourriel.Text))
            {
                messageErreur += $"Vous devez inscrire le courriel du client.\n";
            }

            if (!string.IsNullOrWhiteSpace(txtCourriel.Text) && !txtCourriel.Text.Contains(Reservation.COURRIEL_CAR_OBLIGATOIRE))
            {
                messageErreur += $"Le courriel doint contenir un '@'.\n";
            }

            if (string.IsNullOrWhiteSpace(txtTelephone.Text))
            {
                messageErreur += $"Vous devez inscrire le numéro de téléphone du client.\n";
            }

            if (!string.IsNullOrWhiteSpace(txtTelephone.Text) && !txtTelephone.Text.Contains(Reservation.TELEPHONE_CAR_OBLIGATOIRE))
            {
                messageErreur += $"Le numéro de téléphone doit contenir au moins un '-'.\n";
            }

            if (string.IsNullOrWhiteSpace(txtAdresse.Text))
            {
                messageErreur += $"Vous devez inscrire l'adresse du client.";
            }

            return messageErreur;
        }

        #endregion

        #region ACTIONS-FORMULAIRE

        /// <summary>
        /// Efface les champs de recherche de chambres.
        /// </summary>
        private void btnEffacerRecherche_Click(object sender, RoutedEventArgs e)
        {
            dtpDateArrivee.SelectedDate = null;
            dtpDateDepart.SelectedDate = null;
            lstChambres.Items.Clear();
        }

        /// <summary>
        /// Recherche les chambres disponibles pour les dates sélectionnées.
        /// </summary>
        private void btnRechercheChambre_Click(object sender, RoutedEventArgs e)
        {
            if (!dtpDateArrivee.SelectedDate.HasValue || !dtpDateDepart.SelectedDate.HasValue)
            {
                MessageBox.Show("Vous devez sélectionner une date d'arrivée et une date de départ.", "Sélection de dates");
                return;
            }
            if (dtpDateArrivee.SelectedDate.Value >= dtpDateDepart.SelectedDate.Value)
            {
                MessageBox.Show("La date d'arrivée doit être antérieure à la date de départ.", "Sélection de dates");
                return;
            }

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

        /// <summary>
        /// Remplis les informations de la chambre sélectionnée.
        /// </summary>
        private void lstChambres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!dtpDateArrivee.SelectedDate.HasValue || !dtpDateDepart.SelectedDate.HasValue)
            {
                MessageBox.Show("Vous devez sélectionner une date d'arrivée et une date de départ.", "Sélection de dates");
                return;
            }
            if(dtpDateArrivee.SelectedDate.Value >= dtpDateDepart.SelectedDate.Value)
            {
                MessageBox.Show("La date d'arrivée doit être antérieure à la date de départ.", "Sélection de dates");
                return;
            }

            Chambre chambreSelectionnee = (Chambre)lstChambres.SelectedItem;

            if (chambreSelectionnee == null)
            {
                return;
            }

            txtNumero.Text = Convert.ToString(chambreSelectionnee.Numero);
            txtType.Text = Convert.ToString(chambreSelectionnee.Type);
            txtDateArrivee.Text = dtpDateArrivee.SelectedDate.Value.ToShortDateString();
            txtDateDepart.Text = dtpDateDepart.SelectedDate.Value.ToShortDateString();
            txtPrixParNuit.Text = Convert.ToString(chambreSelectionnee.PrixParNuit);

            DateTime dateArrivee = DateTime.Parse(txtDateArrivee.Text);
            DateTime dateDepart = DateTime.Parse(txtDateDepart.Text);
            int nbJours = (dateDepart - dateArrivee).Days;
            decimal sousTotal = (nbJours * decimal.Parse(txtPrixParNuit.Text));
            decimal total = sousTotal + (sousTotal * 0.15M);

            txtTotal.Text = $"{total:C}";
        }

        /// <summary>
        /// Efface les champs de création de réservations.
        /// </summary>
        private void btnEffacerReservation_Click(object sender, RoutedEventArgs e)
        {
            txtNom.Text = null;
            txtPrenom.Text = null;
            txtCourriel.Text = null;
            txtTelephone.Text = null;
            txtAdresse.Text = null;

            txtNumero.Text = null;
            txtType.Text = null;
            txtDateArrivee.Text = null;
            txtDateDepart.Text = null;
            txtPrixParNuit.Text = null;
            txtTotal.Text = null;

            lstChambres.SelectedItem = null;
        }

        /// <summary>
        /// Crée une nouvelle réservation.
        /// </summary>
        private void btnCreerReservation_Click(object sender, RoutedEventArgs e)
        {
            if(lstChambres.SelectedItem == null)
                {
                MessageBox.Show("Vous devez sélectionner une chambre.", "Création d'une réservation");
            }

            else if (ValiderFormulaire() == "")
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

                txtTotalReservations.Text = $"{_gestionHotel.CalculerMontantTotalReservations():C}";
                txtPrixMoyenReservation.Text = $"{_gestionHotel.CalculerPrixMoyenReservation():C}";
                txtChambrePlusReservee.Text = Convert.ToString(_gestionHotel.ObtenirChambreLaPlusReservee().Numero);

                txtNom.Clear();
                txtPrenom.Clear();
                txtCourriel.Clear();
                txtTelephone.Clear();
                txtAdresse.Clear();
                txtNumero.Clear();
                txtType.Clear();
                txtDateArrivee.Clear();
                txtDateDepart.Clear();
                txtPrixParNuit.Clear();
                txtTotal.Clear();

                _gestionHotel.EnregistrerReservation(CHEMIN_FICHIER_RESERVATIONS);

                MessageBox.Show("La réservation a été créée avec succès!", "Création d'une réservation");

                lstChambres.Items.Clear();
            }
            
            else
            {
                MessageBox.Show(ValiderFormulaire(), "Création d'une réservation");
            }
        }

        /// <summary>
        /// Efface les champs de recherche de réservations et la liste des réservations.
        /// </summary>
        private void btnEffacerRechercheReservation_Click(object sender, RoutedEventArgs e)
        {
            lstReservations.Items.Clear();
            txtRechercheCourriel.Clear();
            txtRechercheTelephone.Clear();
        }

        /// <summary>
        /// Recherche des réservations selon les critères de recherche.
        /// </summary>
        private void btnRechercherReservation_Click(object sender, RoutedEventArgs e)
        {
            lstReservations.Items.Clear();

            Reservation[] liste = _gestionHotel.RechercherReservations(txtRechercheCourriel.Text, txtRechercheTelephone.Text);

            for (int i = 0; i < liste.Length; i++)
            {
                lstReservations.Items.Add(liste[i]);
            }
        }

        /// <summary>
        /// Supprime la réservation sélectionnée.
        /// </summary>
        private void btnSupprimerReservation_Click(object sender, RoutedEventArgs e)
        {
            _gestionHotel.SupprimerReservation((Reservation)lstReservations.SelectedItem);
            lstReservations.Items.Clear();

            MessageBox.Show("La réservation a été supprimée avec succès!", "Suppression d'une réservation");

            txtTotalReservations.Text = $"{_gestionHotel.CalculerMontantTotalReservations():C}";
            txtPrixMoyenReservation.Text = $"{_gestionHotel.CalculerPrixMoyenReservation():C}";
            txtChambrePlusReservee.Text = Convert.ToString(_gestionHotel.ObtenirChambreLaPlusReservee().Numero);

            _gestionHotel.EnregistrerReservation(CHEMIN_FICHIER_RESERVATIONS);

        }

        #endregion     
    }
}