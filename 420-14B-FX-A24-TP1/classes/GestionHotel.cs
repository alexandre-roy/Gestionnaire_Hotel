using _420_14B_FX_A23_TP1.classes;
using _420_14B_FX_A24_TP1.enums;

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
        /// <param name="cheminFichierChambres"> Le chemin d'accès du fichier de chambres. </param>
        /// <param name="cheminFichierReservations"> Le chemin d'accès du fichier de réservations. </param>
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
        /// <param name="cheminFichierChambres"> Le chemin d'accès du fichier. </param>
        public void ChargerChambres(string cheminFichierChambres)
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
        ///  <param name="numero"> Un numéro de chambre. </param>
        /// <returns> Une chambre. </returns> 
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
        /// <param name="cheminFichierReservations"> Le chemin d'accès du fichier. </param>
        public void ChargerReservations(string cheminFichierReservations)
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
        /// <param name="dateArrivee"> Une date d'arrivée. </param>
        /// <param name="dateDepart"> Une date de départ. </param>
        /// <returns> Un vecteur des chambres disponibles. </returns>
        public Chambre[] RechercherChambresDisponibles(DateOnly dateArrivee, DateOnly dateDepart)
        {
            Chambre[] chambresDispo = new Chambre[1];

            for (int i = 0; i < Chambres.Length; i++)
            {
                for (int j = 0; j < Reservations.Length; j++)
                {
                    if (Chambres[i].Numero == Reservations[j].Chambre.Numero)
                    {
                        if (!(dateArrivee < Reservations[j].DateDepart && dateDepart > Reservations[j].DateArrivee))
                        {
                            chambresDispo = AjouterChambre(Chambres[i], chambresDispo);
                        }
                    }
                }
            }

            Chambre[] chambresDispo2 = new Chambre[chambresDispo.Length - 1];

            for (int i = 0; i < chambresDispo.Length - 1; i++)
            {
                chambresDispo2[i] = chambresDispo[i + 1];
            }

            return chambresDispo2;
        }

        /// <summary>
        /// Permet d’ajouter une chambre à un vecteur de chambres reçu en paramètre.
        /// </summary>
        /// <param name="chambre"> Une chambre. </param>
        /// <param name="vectChambres"> Un vecteur de chambres. </param>
        /// <returns> Un vecteur de chambres avec une chambre ajouté a la fin. </returns>
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
        /// <param name="reservation"> Une réservation. </param>
        /// <param name="reservations"> Un vecteur de réservations. </param>
        /// <returns> Un vecteur de réservations avec une réservation ajouté à la fin. </returns>
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
        /// <param name="reservation"> Une réservation. </param>
        public void CreerReservation(Reservation reservation)
        {
            Reservations = AjouterReservation(reservation, Reservations);
        }

        /// <summary>
        /// Recherche des réservations par courriel ou téléphone, et retourne les réservations correspondantes.
        /// </summary>
        /// <param name="courriel"> Adresse courriel. </param>
        /// <param name="telephone"> Numéro de téléphone. </param>
        /// <returns> Un vecteur de réservations qui correspondent avec les paramètres. </returns>
        public Reservation[] RechercherReservations(string courriel, string telephone)
        {
            Reservation[] listeFiltre = new Reservation[1];

            if (courriel == "" && telephone == "")
            {
                return Reservations;
            }

            else if (telephone != "" || courriel != "")
            {
                for (int i = 0; i < Reservations.Length; i++)
                {
                    if (Reservations[i].Courriel == courriel || Reservations[i].Telephone == telephone)
                    {               
                        listeFiltre = AjouterReservation(Reservations[i], listeFiltre);
                    }
                }
            }

            Reservation[] listeFiltre2 = new Reservation[listeFiltre.Length - 1];

            for (int i = 0; i < listeFiltre.Length - 1; i++)
            {
                listeFiltre2[i] = listeFiltre[i + 1];
            }

            return listeFiltre2;

        }

        /// <summary>
        ///  Supprime la réservation reçue en paramètres des réservations existantes.
        /// </summary>
        /// <param name="reservation"> Une réservation. </param>
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
        /// <returns> Un decimal qui représente le montant total des réservations. </returns>
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
        /// <returns> Un decimal qui représente le prix moyen des réservations. </returns>
        public decimal CalculerPrixMoyenReservation()
        {
            decimal montantTotal = CalculerMontantTotalReservations();
            decimal prixMoyen = 0;

            prixMoyen = montantTotal / Reservations.Length;

            return prixMoyen;
        }

        /// <summary>
        /// Permet d’obtenir la chambre ayant eu le plus de réservations.
        /// </summary>
        /// <returns> La chambre avec le plus de réservations. </returns>
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
        /// <param name="cheminFichier"> Chemin d'accès au fichier. </param>
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
    