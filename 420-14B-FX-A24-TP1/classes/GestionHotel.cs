﻿using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using _420_14B_FX_A23_TP1.classes;
using _420_14B_FX_A24_TP1.enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _420_14B_FX_A24_TP1.classes
{
    public class GestionHotel
    {
        #region ATTRIBUTS

        /// <summary>
        /// Vecteur de chambres.
        /// </summary>
        private Chambre[] _chambres;
        private Reservation[] _reservations;

        #endregion

        #region PROPRIÉTÉS

        /// <summary>
        /// Obtient la liste des chambres.
        /// </summary>
        public Chambre[] Chambres
        {
            get { return _chambres; }
            set { _chambres = value; }
        }

        /// <summary>
        /// Obtient la liste des reservations.
        /// </summary>
        public Reservation[] Reservations
        {
            get { return _reservations; }
            set { _reservations = value; }
        }

        #endregion

        #region CONSTRUCTEUR
        /// <summary>
        /// Constructeur de GestionHotel.
        /// </summary>
        public GestionHotel(string cheminFichierChambres, string cheminFichierReservations)
        {
            ChargerChambres(cheminFichierChambres);
            ChargerReservations(cheminFichierReservations);
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet de charger les chambres à partir du chemin d'accès du fichier passé en paramètre. 
        /// </summary>
        private void ChargerChambres(string cheminFichierChambres)
        {
            string[] vectLignes = Utilitaire.ChargerDonnees(cheminFichierChambres);

            Chambres = new Chambre[vectLignes.Length - 1];

            for (int i = 1; i < vectLignes.Length; i++)
            {
                string[] detailsChambre = vectLignes[i].Split(';');

                ushort numero = ushort.Parse(detailsChambre[0]);
                decimal prix = decimal.Parse(detailsChambre[2]);
                TypeChambre type = (TypeChambre)Enum.Parse(typeof(TypeChambre), detailsChambre[1]);

                Chambre Chambre = new Chambre(numero, prix, type);

                Chambres[i - 1] = Chambre;
            }
        }

        /// <summary>
        ///  Permet d’obtenir une chambre dans la liste des chambres à partir de son numéro.
        ///  </summary>
        public Chambre ObtenirChambre(ushort numero)
        {
            for (int i = 0; i < Chambres.Length; i++)
            {
                if (Chambres[i].Numero == numero)
                {
                    return Chambres[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Permet de charger les réservations à partir du chemin d'accès du fichier passé en paramètre.
        /// </summary>
        private void ChargerReservations(string cheminFichierReservations)
        {
            string[] vectLignes = Utilitaire.ChargerDonnees(cheminFichierReservations);

            Reservations = new Reservation[vectLignes.Length - 1];

            for (int i = 1; i < vectLignes.Length; i++)
            {
                string[] detailsReservation = vectLignes[i].Split(';');

                ushort numChambre = ushort.Parse(detailsReservation[0]);
                DateOnly dateArrivee = DateOnly.Parse(detailsReservation[1]);
                DateOnly dateDepart = DateOnly.Parse(detailsReservation[2]);
                string nom = detailsReservation[3];
                string prenom = detailsReservation[4];
                string telephone = detailsReservation[5];
                string courriel = detailsReservation[6];
                string adresse = detailsReservation[7];

                Chambre chambre = ObtenirChambre(numChambre);

                Reservation Reservation = new Reservation(adresse, chambre, courriel, dateArrivee, dateDepart, nom, prenom, telephone);

                Reservations[i - 1] = Reservation;
            }
        }

        /// <summary>
        /// Permet d’obtenir les chambres disponibles (i.e. qui n’ont pas de réservation) selon la date d’arrivée et la date de départ.
        /// </summary>
        public Chambre[] RechercherChambresDisponibles(DateOnly dateArrivee, DateOnly dateDepart)
        {
            Chambre[] chambresDispo = new Chambre[Chambres.Length];

            bool dispo = true;

            for (int i = 0; i < Chambres.Length; i++)
            {
                for (int j = 0; j < Reservations.Length; j++)
                {
                    if (Chambres[i].Numero == Reservations[j].Chambre.Numero)
                    {
                        if (dateArrivee < Reservations[j].DateDepart && dateDepart > Reservations[j].DateArrivee)
                        {
                            dispo = false;
                        }
                        else
                        {
                            dispo = true;
                        }
                    }
                    if (dispo)
                    {
                        chambresDispo[i] = Chambres[i];
                    }
                }

            }
            return chambresDispo;
        }

        /// <summary>
        /// Permet d’ajouter une chambre à un vecteur de chambres reçu en paramètre.
        /// </summary>
        private Chambre[] AjouterChambre(Chambre chambre, Chambre[] vectChambres)
        {
            Chambre[] chambre1 = new Chambre[vectChambres.Length + 1];

            for (int i = 0; i < vectChambres.Length; i++)
            {
                chambre1[i] = vectChambres[i];

                chambre1[vectChambres.Length] = chambre;
            }

            return chambre1;
        }

        /// <summary>
        /// Permet d’ajouter la réservation au vecteur de réservations reçu en paramètre.
        /// </summary>
        private Reservation[] AjouterReservation(Reservation reservation, Reservation[] reservations)
        {
            Reservation[] reservation1 = new Reservation[reservations.Length + 1];

            for (int i = 0; i < reservations.Length; i++)
            {
                reservation1[i] = reservations[i];

                reservation1[reservations.Length] = reservation;
            }

            return reservation1;
        }

        /// <summary>
        ///  Permet d’ajouter une nouvelle réservation aux réservations existantes.
        /// </summary>
        public void CreerReservation(Reservation reservation)
        {
            Reservations = AjouterReservation(reservation, Reservations);
        }

        /// <summary>
        /// Recherche des réservations par courriel ou téléphone, et retourne les réservations correspondantes.
        /// </summary>
        public Reservation[] RechercherReservations(string courriel, string telephone)
        {
            Reservation[] listeFiltre = new Reservation[Reservations.Length];

            if (courriel == "" && telephone == "")
            {
                for (int i = 0; i < Reservations.Length; i++)
                {
                    listeFiltre[i] = Reservations[i];
                }
            }
            else if (telephone != "" || courriel != "")
            {
                for (int i = 0; i < Reservations.Length; i++)
                {
                    if (Reservations[i].Courriel == courriel || Reservations[i].Telephone == telephone)
                    {
                        listeFiltre[i] = Reservations[i];
                    }
                }
            }

            return listeFiltre;
        }

        /// <summary>
        ///  Supprime la réservation reçue en paramètres des réservations existantes.
        /// </summary>
        public void SupprimerReservation(Reservation reservation)
        {
            Reservation[] nouvellesReservations = new Reservation[Reservations.Length - 1];
            int j = 0;

            for (int i = 0; i < Reservations.Length; i++)
            {
                if (Reservations[i] != reservation)
                {
                    nouvellesReservations[j] = Reservations[i];
                    j++;
                }
            }
            Reservations = nouvellesReservations;
        }

        /// <summary>
        /// Permet d’obtenir le montant total de toutes les réservations.
        /// </summary>
        public decimal CalculerMontantTotalReservations()
        {
            decimal montantTotal = 0;

            for (int i = 0; i < Reservations.Length; i++)
            {
                montantTotal += Reservations[i].Total;
            }

            return montantTotal;
        }
        /// <summary>
        /// Permet d’obtenir le prix moyen d’une réservation.
        /// </summary>
        public decimal CalculerPrixMoyenReservation()
        {
            decimal montantTotal = CalculerMontantTotalReservations();
            decimal prixMoyen = 0;

            prixMoyen = montantTotal / Reservations.Length;

            return prixMoyen;
        }

        /// <summary>
        /// Permet d’obtenir la chambre ayant eu le plus de réservations.
        public Chambre ObtenirChambreLaPlusReservee()
        {
            int[] nbReservations = new int[Chambres.Length];
            Chambre chambrePopulaire = null;

            for (int i = 0; i < Reservations.Length; i++)
            {
                for (int j = 0; j < Chambres.Length; j++)
                {
                    if (Reservations[i].Chambre.Numero == Chambres[j].Numero)
                    {
                        nbReservations[j]++;
                    }
                }
            }

            for (int i = 0; i < nbReservations.Length; i++)
            {
                if (nbReservations[i] == nbReservations.Max())
                {
                    chambrePopulaire = Chambres[i];
                }
            }

            return chambrePopulaire;
        }

        /// <summary>
        /// Permet l’enregistrement des réservations en format CSV 
        /// </summary>
        public void EnregistrerReservation(string cheminFichier)
        {
            string donnees = null;

            donnees += "NumeroChambre;DateArrivee;DateDepart;Nom;Prenom;Telephone;Courriel;Adresse\n";

            for (int i = 0; i < Reservations.Length; i++)
            {
                donnees += $"{Reservations[i].Chambre.Numero};";
                donnees += $"{Reservations[i].DateArrivee};";
                donnees += $"{Reservations[i].DateDepart};";
                donnees += $"{Reservations[i].Nom};";
                donnees += $"{Reservations[i].Prenom};";
                donnees += $"{Reservations[i].Telephone};";
                donnees += $"{Reservations[i].Courriel};";
                donnees += $"{Reservations[i].Adresse}\n";
            }

            Utilitaire.EnregistrerDonnees(cheminFichier, donnees);
        }

        #endregion
    }
}
    