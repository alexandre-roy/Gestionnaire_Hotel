using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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


        #region CONSTRUCTEURS

        public GestionHotel(string cheminFichierChambres, string cheminFichierReservations) 
        {
            ChargerChambres(cheminFichierChambres);
            ChargerReservations(cheminFichierReservations);
        }

        #endregion

        #region MÉTHODES

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

        public Chambre[] AjouterChambre(Chambre chambre, Chambre[] vectChambres)
        {
            Chambre[] chambre1 = new Chambre[vectChambres.Length + 1];

            for (int i = 0; i < vectChambres.Length; i++)
            {
                chambre1[i] = vectChambres[i];

                chambre1[vectChambres.Length] = chambre;
            }

            return chambre1;
        }

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


        #endregion
    }
}


    