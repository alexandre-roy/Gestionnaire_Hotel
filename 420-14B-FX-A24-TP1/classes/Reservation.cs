using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _420_14B_FX_A24_TP1.classes
{
    /// <summary>
    /// Représente une réservation dans le système de gestion d'un hôtel.
    /// </summary>
    public class Reservation
    {
        #region CONSTANTES

        public const string COURRIEL_CAR_OBLIGATOIRE = "@";
        public const string TELEPHONE_CAR_OBLIGATOIRE = "-";

        #endregion

        #region ATTRIBUTS

        // Champs privés
        private string _adresse;
        private Chambre _chambre;
        private string _courriel;
        private DateOnly _dateArrivee;
        private DateOnly _dateDepart;
        private string _nom;
        private string _prenom;
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
                if (!string.IsNullOrEmpty(value))
                {
                    value = _adresse;
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
                if (!string.IsNullOrEmpty(value) && value.Contains(COURRIEL_CAR_OBLIGATOIRE))
                {
                    value = _courriel;
                }
            }
        }

        /// <summary>
        /// Obtient ou définit la date d'arrivée du client.
        /// </summary>
        public DateOnly DateArrivee
        {
            get { return _dateArrivee; }
            set { value = _dateArrivee; }
        }

        /// <summary>
        /// Obtient ou définit la date de départ du client.
        /// </summary>
        public DateOnly DateDepart
        {
            get { return _dateDepart; }
            set { value = _dateDepart; }
        }

        /// <summary>
        /// Obtient ou définit le nom du client.
        /// </summary>
        public string Nom
        {
            get { return _nom; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    value = _nom;
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
                if (!string.IsNullOrEmpty(value))
                {
                    value = _prenom;
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
                if (!string.IsNullOrEmpty(value) && value.Contains(TELEPHONE_CAR_OBLIGATOIRE))
                {
                    value = _telephone;
                }
            }
        }

        /// <summary>
        /// Obtient ou définit le sous-total.
        /// </summary>
        public decimal SousTotal
        {
            get {  

                int nombreDeNuits = (int)(_dateDepart.ToDateTime(TimeOnly.MinValue) - _dateArrivee.ToDateTime(TimeOnly.MinValue)).TotalDays;

                return nombreDeNuits * _chambre.PrixParNuit; }
        }

        /// <summary>
        /// Obtient ou définit le total.
        /// </summary>
        public decimal Total
        {
            get { return (SousTotal / 15) + SousTotal; }
        }


        #endregion

        #region MÉTHODES
        #endregion

    }
}
