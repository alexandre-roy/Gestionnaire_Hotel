namespace _420_14B_FX_A24_TP1.classes
{
    /// <summary>
    /// Représente une réservation dans le système de gestion d'un hôtel.
    /// </summary>
    public class Reservation
    {
        #region CONSTANTES

        /// <summary>
        /// Caractère obligatoire pour le courriel.
        /// </summary>
        public const string COURRIEL_CAR_OBLIGATOIRE = "@";

        /// <summary>
        /// Caractère obligatoire pour le téléphone.
        /// </summary>
        public const string TELEPHONE_CAR_OBLIGATOIRE = "-";

        #endregion

        #region ATTRIBUTS

        /// <summary>
        /// L'adresse du client.
        /// </summary>
        private string _adresse;

        /// <summary>
        /// La chambre associé a la réservation.
        /// </summary>
        private Chambre _chambre;

        /// <summary>
        /// L'adresse courriel du client.
        /// </summary>
        private string _courriel;

        /// <summary>
        /// La date d'arrivée du séjour du client.
        /// </summary>
        private DateOnly _dateArrivee;

        /// <summary>
        /// La date de départ du séjour du client.
        /// </summary>
        private DateOnly _dateDepart;

        /// <summary>
        /// Le nom de famille du client.
        /// </summary>
        private string _nom;

        /// <summary>
        /// Le prénom du client.
        /// </summary>
        private string _prenom;

        /// <summary>
        /// Le numéro de téléphone du client.
        /// </summary>
        private string _telephone;

        #endregion

        #region PROPRIÉTÉS

        /// <summary>
        /// Obtient ou définit l'adresse du client.
        /// </summary>
        public string Adresse
        {
            get { return _adresse; }
            set {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _adresse = value;
                }
            }                                 
        }

        /// <summary>
        /// Obtient ou définit l'adresse courriel du client.
        /// </summary>
        public string Courriel
        {
            get { return _courriel; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && ContiensCaractere(value, COURRIEL_CAR_OBLIGATOIRE))
                {
                    _courriel = value;
                }
            }
        }

        /// <summary>
        /// Obtient ou définit la date d'arrivée du client.
        /// </summary>
        public DateOnly DateArrivee
        {
            get { return _dateArrivee; }
            set { _dateArrivee = value; }
        }

        /// <summary>
        /// Obtient ou définit la date de départ du client.
        /// </summary>
        public DateOnly DateDepart
        {
            get { return _dateDepart; }
            set { _dateDepart = value; }
        }

        /// <summary>
        /// Obtient ou définit le nom du client.
        /// </summary>
        public string Nom
        {
            get { return _nom; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _nom = value;
                }
            }
        }

        /// <summary>
        /// Obtient ou définit le prénom du client.
        /// </summary>
        public string Prenom
        {
            get { return _prenom; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _prenom = value;
                }
            }
        }

        /// <summary>
        /// Obtient ou définit le numéro de téléphone du client.
        /// </summary>
        public string Telephone
        {
            get { return _telephone; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && ContiensCaractere(value, TELEPHONE_CAR_OBLIGATOIRE))
                {
                    _telephone = value;
                }
            }
        }

        /// <summary>
        /// Obtient ou définit le sous-total.
        /// </summary>     
        public decimal SousTotal
        {
            get { return CalculerSousTotal(); }
        }

        /// <summary>
        /// Obtient ou définit le total.
        /// </summary>
        public decimal Total
        {
            get { return CalculerTotal();  }
        }

        /// <summary>
        /// Obtient ou définit la chambre.
        /// </summary>
        public Chambre Chambre
        {
            get { return _chambre; }
            set { _chambre = value; }
        }

        #endregion

        #region CONSTRUCTEUR

        /// <summary>
        /// Inistialise une nouvelle instance de la classe Reservation.
        /// </summary>
        public Reservation(string adresse, Chambre chambre, string courriel, DateOnly dateArrivee, DateOnly dateDepart, string nom, string prenom, string telephone)
        {
            Adresse = adresse;
            Chambre = chambre;
            Courriel = courriel;
            DateArrivee = dateArrivee;
            DateDepart = dateDepart;
            Nom = nom;
            Prenom = prenom;
            Telephone = telephone;
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet de calculer le montant du sous-total de la réservation.
        /// </summary>
        /// <returns>
        /// Une décimal représentant le montant du sous-total de la réservation.
        /// </returns>
        private decimal CalculerSousTotal()
        {
            int nombreDeNuits = (int)(DateDepart.ToDateTime(TimeOnly.MinValue) - DateArrivee.ToDateTime(TimeOnly.MinValue)).TotalDays;
            return nombreDeNuits * Chambre.PrixParNuit;
        }


        /// <summary>
        /// Permet de calculer le montant total de la réservation incluant les taxes.
        /// </summary>
        /// <returns>
        /// Une décimal représentant le montant du total de la réservation.
        /// </returns>
        private decimal CalculerTotal()
        {
            decimal taxes = 0.15M;
            return (SousTotal * taxes) + SousTotal;
        }

        /// <summary>
        /// S'assure qu'une chaine de caractères ne contient pas un certain caractère.
        /// </summary>
        /// <returns>
        /// Un bool qui indique si le caractère est présent ou pas.
        /// </returns>
        public static bool ContiensCaractere(string chaine, string caractere)
        {
            for (int i = 0; i < chaine.Length; i++)
            {
                if (chaine[i] == char.Parse(caractere))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Une chaîne de caractères représentant l'objet Reservation.
        /// </summary>
        /// <returns>
        /// Une chaines de caractères représentant les informations de la réservation et du client.
        /// </returns>
        public override string ToString()
        {
            string nomPrenomPadRight = $"{Nom}, {Prenom}".PadRight(21, ' ');
            string chambrePadRight = $"{Chambre.Numero}".PadRight(10, ' ');
            string arriveePadRight = $"{DateArrivee}".PadRight(15, ' ');
            string departPadRight = $"{DateDepart}".PadRight(16, ' ');
            string telephonePadRight = $"{Telephone}".PadRight(20, ' ');
            string courrielPadRight = $"{Courriel}".PadRight(38, ' ');

            return $"{nomPrenomPadRight}{chambrePadRight}{arriveePadRight}{departPadRight}{telephonePadRight}{courrielPadRight}{Total:C}";
        }
     
        #endregion
    }
}
